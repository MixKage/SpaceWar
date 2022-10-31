using Mirror;
using Mirror.Examples.NetworkRoom;
using UnityEngine;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace SpaceWar.Menu.NetworkRoom
{
    [AddComponentMenu("")]
    public class NetworkRoomManagerExt : NetworkRoomManager
    {
        /// <summary>
        /// Вызывается на сервере, когда заканчивается
        /// загрузка сетевой сцены
        /// </summary>
        /// <param name="sceneName">Имя новой стены</param>
        public override void OnRoomServerSceneChanged(string sceneName)
        {
            // // spawn the initial batch of Rewards
            // if (sceneName == GameplayScene)
            //     Spawner.InitialSpawn();
        }

        /// <summary>
        /// Вызывается сразу после создания игрока (prefab SpaceShip)
        /// Идиальная точка для передачи каких-либо данных
        /// </summary>
        /// <param name="roomPlayer"></param>
        /// <param name="gamePlayer"></param>
        /// <returns>true, если какой-то код здесь не решит, что ему нужно прервать замену</returns>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
            playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
            Debug.Log("Player created");
            return true;
        }

        public override void OnRoomStopClient()
        {
            base.OnRoomStopClient();
        }

        public override void OnRoomStopServer()
        {
            base.OnRoomStopServer();
        }
        //Показывать ли кнопку начала игры (только для хоста/сервера)
        bool showStartButton;

        public override void OnRoomServerPlayersReady()
        {
            // Вызов базового метода вызывает ServerChangeScene, как
            // только все игроки находятся в состоянии готовности (Ready).
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            showStartButton = true;
#endif
        }
        // Код ниже будет полностью переписан
        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // скрыть кнопку в игровой сцене
                showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }
    }
}
