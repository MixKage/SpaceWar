using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private GameObject _spawnGroupContainer;

    public void SpawnGroupToogle()
    {
        _spawnGroupContainer.SetActive(!_spawnGroupContainer.activeSelf);
    }
}
