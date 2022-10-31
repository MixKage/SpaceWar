using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SpaceWar.Pvp
{
    public class CameraController : NetworkBehaviour
    {
        [HideInInspector] public Camera mainCam;
        private const float CameraHeight = 20f;

        private void Start()
        {
            if (isClient && isLocalPlayer)
            {
                mainCam = Camera.main;
            }
        }

        public override void OnStartLocalPlayer()
        {
            if (mainCam != null)
                mainCam.orthographic = false;
        }

        public override void OnStopLocalPlayer()
        {
            if (mainCam != null)
            {
                mainCam.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(mainCam.gameObject, SceneManager.GetActiveScene());
            }
        }
        // LateUpdate используется, чтобы камера не обгоняла игрока
        private void LateUpdate()
        {
            mainCam.transform.position = transform.position + new Vector3(0, CameraHeight, 0);
        }

    }
}
