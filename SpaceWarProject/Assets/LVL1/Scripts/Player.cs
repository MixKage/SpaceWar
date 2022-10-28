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

    [SyncVar] private int HitPoints = 100;
    [SyncVar] private int MaxHitPoints = 100;
    public Camera PlayerCamera;
    private float cameraHeight = 30;
    private Rigidbody _rb;
    private readonly float SPEED = 20;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    [SerializeField] private GameObject DyingInfoRespawn;

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

    public void Update()
    {
        
        if (isLocalPlayer)
        {
            Vector3 movementVector = new Vector3();
            movementVector.x = Input.GetAxis("Horizontal");
            movementVector.z = Input.GetAxis("Vertical");

            //Стрельба с перезарядкой
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Piwe");
                    CmdShootPlayer(netId);

                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

            MovePlayer(movementVector);

            LookAtMouse();
            CameraUpdate();
        }
    }

    public float GetHpSliderValue()
    {
        return 1 - ((float)HitPoints / (float)MaxHitPoints);
    }

    public void SetActivePlayer(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void ReturnPlayerInGame()
    {
        gameObject.SetActive(true);
        HitPoints = MaxHitPoints;
    }

    public void CameraUpdate()
    {
        PlayerCamera.transform.position = this.transform.position + new Vector3(0, cameraHeight, 0);
    }

    private void SetInputManagerPlayer()
    {
        InputManager.Instance.SetPlayer(this);
    }

    public void LookAtMouse()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 lookRotation = new Vector3(hit.point.x, _bodySpaceShip.transform.position.y, hit.point.z);
            _bodySpaceShip.transform.LookAt(lookRotation);
        }
    }

    //Функция движение
    public void MovePlayer(Vector3 _movementVector)
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
            {
                //NetworkServer.Destroy(gameObject);
                //Вместо удаления корабля мы будем его отключать
                SetActivePlayer(false);
            }
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