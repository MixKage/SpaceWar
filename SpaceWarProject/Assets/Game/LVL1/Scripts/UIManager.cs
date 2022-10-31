using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singletone
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion
    public void Awake()
    {
        _instance = this;
    }

    #region UI Groups
    [SerializeField] private GameObject _spawnGroupContainer;
    [SerializeField] private GameObject _playerStatsContainer;
    #endregion

    [SerializeField] private TextMeshProUGUI _nameField;

    public void SetUIPlayerName(string _name)
    {
        _nameField.text = _name;
    }
    public void SpawnGroupToogle()
    {
        _spawnGroupContainer.SetActive(!_spawnGroupContainer.activeSelf);
    }

    public void PLayerStatsGroupToogle()
    {
        _playerStatsContainer.SetActive(!_playerStatsContainer.activeSelf);
    }
}
