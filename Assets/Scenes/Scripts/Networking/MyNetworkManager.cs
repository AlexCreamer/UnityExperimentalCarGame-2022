using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] private MyNetworkPlayer GamePlayerPrefab;

    public List<MyNetworkPlayer> GamePlayers { get; } = new List<MyNetworkPlayer>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            MyNetworkPlayer GamePlayerInstance = Instantiate(GamePlayerPrefab);

            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);

            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);


            Color displayColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );

            GamePlayerInstance.SetDisplayName($"Player {numPlayers}");
            GamePlayerInstance.SetDisplayColor(displayColor);

        }
    }

    public void StartGame(string SceneName)
    {
        ServerChangeScene(SceneName);
    }

    /*
    // Happens First
    public override void OnStartClient(NetworkConnectio n conn)
    {
    }
    */
}
