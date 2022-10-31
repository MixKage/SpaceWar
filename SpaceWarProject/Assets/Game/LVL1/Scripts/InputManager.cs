using System;
using System.Collections;
using System.Collections.Generic;
using kcp2k;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : NetworkBehaviour
{
    #region Singletone
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] public Player _pl;
    private Vector3 movementVector = new Vector3();

    [SerializeField] private TMP_InputField _inputField;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    public void SetPlayer(Player _pl)
    {
        this._pl = _pl;
    }

    public void SpawnPlayer()
    {
        PlayerManager.Instance.SpawnPlayer();
    }

    public void SendName(string name)
    {
        PlayerManager.Instance.PLayerName = _inputField.text;
    }
}
