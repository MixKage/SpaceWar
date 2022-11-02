using System.Collections;
using System.Collections.Generic;
using Mirror.SimpleWeb;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceWar.OfflineMenu
{
    public class AnimateMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _textMeshPro;
        public void OnStopAnimate()
        {
            _textMeshPro.SetActive(true);
        }
    }
}
