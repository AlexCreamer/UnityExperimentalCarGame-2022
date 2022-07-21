using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        Color displayColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );

        player.SetDisplayName($"Player {numPlayers}");
        player.SetDisplayColor(displayColor);

    }

    /*
    // Happens First
    public override void OnStartClient(NetworkConnectio n conn)
    {
    }
    */
}
