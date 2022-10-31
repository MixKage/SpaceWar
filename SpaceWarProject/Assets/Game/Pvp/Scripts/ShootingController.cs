using System.Collections;
using System.Collections.Generic;
using Mirror;
using SpaceWar.Pvp;
using UnityEngine;

namespace SpaceWar.Pvp
{
    public class ShootingController : NetworkBehaviour
    {
        [SerializeField] private GameObject _missile;
        [SerializeField] private Transform _shotPoint;
        
        // Функция для сервера стрельба
        [Command]
        public void CmdShootPlayer(uint owner)
        {
            Debug.Log("Fire");
            GameObject missle = Instantiate(_missile, _shotPoint.position, GetComponent<PlayerMovementController>().bodySpaceShip.transform.rotation);
            NetworkServer.Spawn(missle);
            missle.GetComponent<Missile>().Init(owner);
        }
    }
}
