using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Canvas optionsCanvas;
    public Canvas orderSlipCanvas;
    public Image orderSlipImage;
    public Sprite spaghettiImage;
    public Sprite hotdogImage;
    public Sprite drinkImage;
    private GameNetworkManager networkManager;

    private void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<GameNetworkManager>();
    }

    public void ShowOptions()
    {
        optionsCanvas.enabled = true;
    }

    public void HideOptions()
    {
        optionsCanvas.enabled = false;
    }

    public void ShowOrderSlip(Order orderType)
    {
        switch(orderType)
        {
            case Order.DRINK:
                orderSlipImage.sprite = drinkImage;
                break;

            case Order.HOTDOG:
                orderSlipImage.sprite = hotdogImage;
                break;

            case Order.SPAGHETTI:
                orderSlipImage.sprite = spaghettiImage;
                break;
        }

        orderSlipCanvas.enabled = true;
    }

    public void HideOrderSlip()
    {
        orderSlipCanvas.enabled = false;
    }

    public void Recalibrate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            Player player = players[i].GetComponent<Player>();
            if (player.isLocalPlayer)
            {
                player.reCalibrate();
                break;
            }
        }
    }

    public void Disconnect()
    {
        networkManager.StopHost();
    }
}
