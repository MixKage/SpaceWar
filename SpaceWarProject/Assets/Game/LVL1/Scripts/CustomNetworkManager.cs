using System.Collections;
using System.Collections.Generic;
using Mirror;
using Telepathy;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        //base.OnClientConnect();
        Debug.Log("Connected");
        UIManager.Instance.SpawnGroupToogle();
    }

    public override void OnClientDisconnect()
    {
        UIManager.Instance.PLayerStatsGroupToogle();
        base.OnClientDisconnect();
    }
}
