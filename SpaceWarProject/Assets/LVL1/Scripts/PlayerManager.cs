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

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    //Буфер между клиентом и сервером
    [SerializeField] private string _playerName;

    public string PLayerName
    {
        get
        {
            return _playerName;
        }

        set
        {
            _playerName = value;
        }
    }

    [SerializeField] private NetworkManager _netManager;

    public void SpawnPlayer()
    {
        //Проверка на клиенте не самая крутая идея, но пока так
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
        UIManager.Instance.PLayerStatsGroupToogle();
        UIManager.Instance.SetUIPlayerName(_playerName);
    }
}
