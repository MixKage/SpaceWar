using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
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
