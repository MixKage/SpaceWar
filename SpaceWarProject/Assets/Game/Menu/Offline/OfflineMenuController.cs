using System.Collections;
using System.Collections.Generic;
using System.Net;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{
    private NetworkManager manager;
    public InputField IPInputField;
    public GameObject HostConnect;

    public void CreateHost()
    {
        manager.StartHost();
    }

    public void ConnectToIpAddress()
    {
        manager.networkAddress = IPInputField.text;
        manager.StartClient();
    }

    public void CreateServerByIp()
    {
        manager.networkAddress = IPInputField.text;
        manager.StartServer();
    }
}
