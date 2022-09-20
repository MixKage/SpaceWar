using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.SnapshotInterpolationDemo;
using Mirror.SimpleWeb;
using Telepathy;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singletone
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get { return _instance; }
    }
    #endregion

    //Буфер
    private string _playerName;

    [SerializeField] private NetworkManager _netManager;

    public void Awake()
    {
        _instance = this;
    }

    public void SetPlayerName(string _name)
    {
        _playerName = _name;
    }

    public void SpawnPlayer()
    {
        if (string.IsNullOrWhiteSpace(_playerName))
        {
            Debug.Log("No name has been set");
            return;
        }
        
        // if (!_netManager.clientLoadedScene)
        // {
        if (!NetworkClient.ready)
            NetworkClient.Ready();

        if (_netManager.autoCreatePlayer)
            NetworkClient.AddPlayer();
        // }
        UIManager.Instance.SpawnGroupToogle();
    }
}
