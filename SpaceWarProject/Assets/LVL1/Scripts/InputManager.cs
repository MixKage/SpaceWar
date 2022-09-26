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

    [SerializeField] private Player _pl;
    private Vector3 movementVector = new Vector3();

    [SerializeField] private TMP_InputField _inputField;
    
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    private void Update()
    {
        if (_pl)
        {
            MoveInput();
            _pl.CameraUpdate();
        }
    }

    public void SetPlayer(Player _pl)
    {
        this._pl = _pl;
    }
    private void MoveInput()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        //Стрельба с перезарядкой
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isServer)
                {
                    _pl.RpcShootPlayer();
                    Debug.Log("1134SERVER");
                }
                else
                {
                    //TODO: Не стреляет у других клиентов
                    _pl.CmdShootPlayer(); 
                    //_pl.RpcShootPlayer();
                    Debug.Log("1134CLIENT");
                    _pl.ShootPlayer();
                }

                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        _pl.CmdMovePlayer(movementVector);
        //_pl.MovePlayer(movementVector);
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
