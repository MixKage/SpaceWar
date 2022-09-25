using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.Serialization;

public class Player : NetworkBehaviour
{
    //SyncVar переменная зависит от сервера
    [SyncVar][SerializeField] private float _speed;
    [SyncVar][SerializeField] private string _playerName;
    [SerializeField] private TextMeshProUGUI _playerNameText;
    Camera PlayerCamera;
    private float cameraHeight = 30;
    private Rigidbody _rb;
    private readonly float SPEED = 1;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        if (isClient && isLocalPlayer)
        {
            SetInputManagerPlayer();
            PlayerCamera = Camera.main;
        }
        if (isServer)
        {
            //Синхронизирую значения с клиентами с помощью SyncVar
            _speed = SPEED;
            CmdSetPlayerName(PlayerManager.Instance.PLayerName);
        }
    }

    public void CameraUpdate()
    {
        PlayerCamera.transform.position = this.transform.position + new Vector3(0, cameraHeight, 0);
    }

    private void SetInputManagerPlayer()
    {
        InputManager.Instance.SetPlayer(this);
    }

    //Функция для сервера
    [Command]
    public void CmdMovePlayer(Vector3 _movementVector)
    {
        _rb.AddForce(_movementVector.normalized * _speed);
    }

    //Функция для клиента
    public void MovePlayer(Vector3 _movementVector)
    {
        _rb.AddForce(_movementVector.normalized * _speed);
    }

    [Command]
    public void CmdSetPlayerName(string _plName)
    {
        _playerName = _plName;
        RpcSetVisibleName(_playerName);
    }
    
    //Срабатывает для всех клинтов
    [ClientRpc]
    public void RpcSetVisibleName(string _plName)
    {
        _playerNameText.text = _plName;
    }
}
