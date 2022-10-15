using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.Serialization;

public class Player : NetworkBehaviour
{
    //SyncVar переменная зависит от сервера
    [SyncVar] [SerializeField] private float _speed;
    [SyncVar] [SerializeField] private string _playerName;
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private GameObject _missile;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _bodySpaceShip;
    [SyncVar] public int HitPoints = 100;
    public Camera PlayerCamera;
    private float cameraHeight = 30;
    private Rigidbody _rb;
    private readonly float SPEED = 20;

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

    [Command]
    public void LookAtMouse(Vector3 cords)
    {
        // transform.LookAt(cords);
        // transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        _bodySpaceShip.transform.LookAt(cords);
        _bodySpaceShip.transform.rotation = Quaternion.Euler(new Vector3(0, _bodySpaceShip.transform.rotation.eulerAngles.y, 0));
    }

    //Функция для сервера движение
    [Command]
    public void CmdMovePlayer(Vector3 _movementVector)
    {
        _rb.AddRelativeForce(_movementVector.normalized * _speed);
    }

    // Функция для сервера стрельба
    [Command]
    public void CmdShootPlayer(uint owner)
    {
        Debug.Log("Fire");
        GameObject missle = Instantiate(_missile, _shotPoint.position, _bodySpaceShip.transform.rotation);
        NetworkServer.Spawn(missle);
        missle.GetComponent<Missile>().Init(owner);
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Missile>() != null)
        {
            HitPoints -= 20;
            if (HitPoints == 0)
                NetworkServer.Destroy(gameObject);
        }
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