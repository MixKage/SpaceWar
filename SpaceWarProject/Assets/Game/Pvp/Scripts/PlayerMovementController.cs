using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceWar.Pvp
{
    public class PlayerMovementController : NetworkBehaviour
    {
        private Rigidbody _rb;
        [FormerlySerializedAs("_bodySpaceShip")][Tooltip("Тело корабля для его разворота, чтобы UI элементы не вертелись вместе с ним")]
        public Transform bodySpaceShip;
        [Header("Movement Settings")] public float moveSpeed = 100f;
        private void Start()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody>();
            GetComponent<NetworkTransform>().clientAuthority = true;
        }

        private Vector3 _movementVector;
        private void Update()
        {
            if (!isLocalPlayer || _rb == null)
                return;
            _movementVector.x = Input.GetAxis("Horizontal");
            _movementVector.z = Input.GetAxis("Vertical");
            LookAtMouse();
        }

        private void FixedUpdate()
        {
            _rb.AddRelativeForce(_movementVector.normalized * moveSpeed);
        }

        private void LookAtMouse()
        {
            Ray ray = GetComponent<CameraController>().mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 lookRotation = new Vector3(hit.point.x, bodySpaceShip.transform.position.y, hit.point.z);
                bodySpaceShip.transform.LookAt(lookRotation);
            }
        }
    }
}
