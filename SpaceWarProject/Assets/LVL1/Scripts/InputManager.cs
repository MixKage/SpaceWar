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

        if (isLocalPlayer) Debug.Log("ImHere");

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
                Debug.Log("Piwe");
                _pl.CmdShootPlayer(netId);

                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        _pl.CmdMovePlayer(movementVector);

        Ray ray = _pl.PlayerCamera.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - _pl.PlayerCamera.transform.position).magnitude;
        Vector3 wCords = ray.origin + ray.direction * midPoint;
        _pl.LookAtMouse(wCords);
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
