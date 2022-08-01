using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Playerscore : MonoBehaviour
{
    [SerializeField] public int ConnectionID;
    [SerializeField] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    //[SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    [SyncVar] public string PlayerName;

    public int score;
    public PlayerListItem playerItem;
    // Start is called before the first frame update
    void Start()
    {
        ConnectionID = playerItem.ConnectionID;
        PlayerSteamID = playerItem.PlayerSteamID;
        PlayerName = playerItem.PlayerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPoint(int point)
    {
        score++;
    }

}
