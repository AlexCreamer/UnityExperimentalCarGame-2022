using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreboardCanvas : MonoBehaviour
{
    public GameObject ScoreboardListViewContent;

    public Text PlayerNameText;
    public RawImage PlayerIcon;
    public Text Score;

    // Start is called before the first frame update
    void Start()
    {
        foreach(KeyValuePair<PlayerListItem, Playerscore> playerScore in Scoreboard.scorekeeper)
        {
            //GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            //playerScore.transform.SetParent(PlayerListViewContent.transform);
            //playerScore.transform.localScale = Vector3.one;

            PlayerNameText = playerScore.Key.PlayerNameText;
            PlayerIcon = playerScore.Key.PlayerIcon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
