using Mirror;
using UnityEngine;

namespace SpaceWar.Menu.NetworkRoom
{
    public class PlayerScore : NetworkBehaviour
    {
        [SyncVar] public int index;

        [SyncVar] public string name;

        [SyncVar] public uint score;

        void OnGUI()
        {
            GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), $"P{index}: {score:0000000}");
        }
    }
}
