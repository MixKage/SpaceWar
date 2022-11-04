using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using kcp2k;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{
    private NetworkManager _manager;
    [SerializeField] private TMP_InputField ipInputField;
    [SerializeField] private TMP_InputField port;
    [SerializeField] private TMP_InputField namePlayer;
    [SerializeField] private TMP_Text texNotification;
    [SerializeField] private GameObject errorNotification;
    private KcpTransport _kcpTransport;
    void Awake()
    {
        _manager = GetComponent<NetworkManager>();
        _kcpTransport = GetComponent<KcpTransport>();
    }
    
    public void CreateHost()
    {
        try
        {
            _manager.networkAddress = ipInputField.text;
            _kcpTransport.Port = ushort.Parse(port.text);
            _manager.StartHost();
        }
        catch (Exception e)
        {
            texNotification.text = e.Message;
            errorNotification.SetActive(true);
        }
    }
    
    public void ConnectToIpAddress()
    {
        try
        {
            _manager.networkAddress = ipInputField.text;
            _manager.StartClient();
        }
        catch (Exception e)
        {
            texNotification.text = e.Message;
            errorNotification.SetActive(true);
        }
    }

    public void CreateServerByIp()
    {
        try
        {
            _manager.networkAddress = ipInputField.text;
            _kcpTransport.Port = ushort.Parse(port.text);
            _manager.StartServer();
        }
        catch (Exception e)
        {
            texNotification.text = e.Message;
            errorNotification.SetActive(true);
        }
    }
}
