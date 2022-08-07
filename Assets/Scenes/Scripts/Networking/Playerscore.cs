using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Playerscore : MonoBehaviour
{
    [SerializeField] public int ConnectionID;
    [SerializeField] public int PlayerIdNumber;
    public ulong PlayerSteamID;
    //[SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    public string PlayerName;

    public Text PlayerNameText;
    public RawImage PlayerIcon;
    public Text PlayerScore;

    public GameObject ScoreboardListViewContent;

    public int score = 0;
    public PlayerListItem playerItem;
    // Start is called before the first frame update
    void Start()
    {
        //ConnectionID = playerItem.ConnectionID;
        //PlayerSteamID = playerItem.PlayerSteamID;
        //PlayerName = playerItem.PlayerName;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScore.text = score.ToString();
    }

    public void addPoint(int point)
    {
        score++;
    }

    public void SetPlayerValues()
    {
        PlayerNameText.text = PlayerName;
        if (playerItem.AvatarReceived) { GetPlayerIcon(); }
    }


    private void GetPlayerIcon()
    {
        PlayerIcon.texture = playerItem.PlayerIcon.texture;
    }

}
