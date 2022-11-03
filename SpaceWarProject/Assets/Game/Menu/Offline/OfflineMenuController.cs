using System.Collections;
using System.Collections.Generic;
using System.Net;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{
    private NetworkManager manager;
    [SerializeField] private TMP_InputField IPInputField;
    [SerializeField] private TMP_InputField Port;
    [SerializeField] private TMP_InputField NamePlayer;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }
    
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
