using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar][SerializeField] private float speed;
    private Rigidbody _rb;
    private readonly float SPEED = 1;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        if (isClient && isLocalPlayer)
        {
            SetInputManagerPlayer();
        }
        if (isServer)
        {
            //Синхронизирую значения с клиентами с помощью SyncVar
            speed = SPEED;
        }
    }

    private void SetInputManagerPlayer()
    {
        InputManager.Instance.SetPlayer(this);
    }
    //Функция для сервера
    [Command]
    public void CmdMovePlayer(Vector3 _movementVector)
    {
        _rb.AddForce(_movementVector.normalized * speed);
    }
    //Функция для клиента
    public void MovePlayer(Vector3 _movementVector)
    {
        _rb.AddForce(_movementVector.normalized * speed);
    }
}
