using System.Collections;
using System.Collections.Generic;

//to find out the local ip
using System.Net;
using System.Net.Sockets;

using System.IO; //to use Stream
using System.Runtime.Serialization.Formatters.Binary; //to use BinaryFormatter

using UnityEngine;
using UnityEngine.Networking;
//Reference links
//http://www.robotmonkeybrain.com/good-enough-guide-to-unitys-unet-transport-layer-llapi/
//https://docs.unity3d.com/Manual/UNetUsingTransport.html
//http://answers.unity3d.com/questions/1102152/trouble-with-networktransport-llapi-clientserver-i.html

public class NetworkTransportExample : MonoBehaviour {
    

    int m_channelReliable = -1,
        m_channelUnreliable = -1,
        m_socketId =-1;
    int m_connectionId = -1;
    const int m_socketPort = 1234;
    HostTopology m_topology;
    
    bool m_isServer = false;
    bool m_isClientWaitingToBeConnected = false;
    string hprLocalIPAddress()
    {
        //return "127.0.0.1";
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        Debug.Log("Local IP " + localIP);
        return localIP;
    }
    void serverInit()
    {
        //open socket
        m_socketId = NetworkTransport.AddHost(m_topology, m_socketPort);
        Debug.Log("serverInit SocketID " + m_socketId);
    }
    void clientInit()
    {
        //open socket
        m_socketId = NetworkTransport.AddHost(m_topology, 0);
        Debug.Log("clientInit SocketID " + m_socketId);

        byte error;
        m_connectionId = NetworkTransport.Connect(m_socketId, hprLocalIPAddress(), m_socketPort, 0, out error);
        Debug.Log((NetworkError)(error));
        Debug.Log("Trying to connect to server. ConnectionId: " + m_connectionId);

        m_isClientWaitingToBeConnected = true;

        //int port;
        //ulong network;
        //ushort detnode;
        //NetworkTransport.GetConnectionInfo(m_socketId, m_connectionId,out  port,out  network, out detnode, out error);
        //Debug.Log("DOUBLE CHECK " +(NetworkError)(error));


    }
    public void sendSocketMessage()
    {
        byte error;
        int a;
        ulong b;
        ushort c;
        NetworkTransport.GetConnectionInfo(m_socketId, m_connectionId, out a, out b, out c, out error);
        Debug.Log((NetworkError)(error));
        Debug.Log("GetConnectionInfo " + a + " , " + b + " , " + c);




        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, "HelloServer");

        int bufferSize = 1024;

        NetworkTransport.Send(m_socketId, m_connectionId, m_channelReliable, buffer, bufferSize, out error);
        Debug.Log((NetworkError)(error));
        Debug.Log("sendSocketMessager " + m_connectionId + " , " + m_channelReliable);
    }
    //We need to always be checking for new messages coming in 
    void UpdateListen()
    {
        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recData)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event received");
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                string message = formatter.Deserialize(stream) as string;
                Debug.Log("incoming message event received: " + message);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }
    }
    void example1()
    {
        if (!Input.GetKeyDown(KeyCode.A)) return;
        
        if (m_isServer) return;
        Debug.Log("A Initiating server");
        serverInit();
        m_isServer = true;
        Debug.Log("Channel reliable " + m_channelReliable);
        Debug.Log("Channel unreliable " + m_channelUnreliable);
        Debug.Log("HostTopology " + m_topology);
        Debug.Log("hostId " + m_socketId);
        
    }
    void example2()
    {
        if (!Input.GetKeyDown(KeyCode.B)) return;
       
        //if (m_isServer) return;
        Debug.Log("B client connect");
        clientInit();
        
    }
    void example3()
    {
        if (!Input.GetKeyDown(KeyCode.C))
        {
            return;
        }

        //if (m_isServer) return;
        Debug.Log("C Sending Message");
        sendSocketMessage();

    }
    private void Start()
    {
        //initialize transport layer for testing
        //initializing the transport layer using default argumetns 
        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();
        m_channelReliable = config.AddChannel(QosType.Reliable);
        m_channelUnreliable = config.AddChannel(QosType.Unreliable);
        m_topology = new HostTopology(config, 10);
    }
    private void Update()
    {
        if (m_isClientWaitingToBeConnected)
        {
            int recConnectionId;
            int recChannelId;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            byte error;
            var recNetworkEvent = NetworkTransport.ReceiveFromHost(m_socketId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    
                    Debug.Log("Here you are truly connected " + recConnectionId + " , " + recChannelId );
                    sendSocketMessage();
                    m_isClientWaitingToBeConnected = false;
                    break;
                case NetworkEventType.BroadcastEvent:
                    Debug.Log("BroadcastEvent");
                    break;
                case NetworkEventType.DataEvent:
                    Debug.Log("DataEvent");
                    break;
                case NetworkEventType.DisconnectEvent:
                    Debug.Log("DisconnectEvent");
                    break;
                case NetworkEventType.Nothing:
                    Debug.Log("Nothing");
                    break;

            }
            return;
        }

            example1();
        example2();
        example3();


        if (m_isServer)
        {
            UpdateListen();
        }
    }
}
