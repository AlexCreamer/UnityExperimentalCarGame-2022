using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scoreboard : MonoBehaviour
{
    //public static Dictionary<PlayerListItem, int> scores = new Dictionary<PlayerListItem, int>();
    public static Dictionary<PlayerListItem, Playerscore> scorekeeper = new Dictionary<PlayerListItem, Playerscore>();
    //public GameObject ScoreboardListViewContent;
    // Update is called once per frame

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
    }

    public static void addPoint(PlayerListItem playerItem, int points)
    {
        scorekeeper[playerItem].addPoint(points);
    }

    public static void addPlayer(PlayerListItem NewPlayerItem)
    {
        Playerscore playerScore = new Playerscore { playerItem = NewPlayerItem, score = 0 };
        scorekeeper.Add(NewPlayerItem, playerScore);
    }
}
