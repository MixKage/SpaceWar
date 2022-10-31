using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace SpaceWar.Pvp
{
    public class PlayerController : NetworkBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<ShootingController>().CmdShootPlayer(netId);
            }
        }
    }
}