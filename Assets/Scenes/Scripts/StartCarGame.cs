using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCarGame : MonoBehaviour
{
    GameObject gameScoreboard;
    // Start is called before the first frame update
    void Start()
    {
        gameScoreboard = LobbyController.scoreboard;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if(gameScoreboard.activeSelf)
            {
                gameScoreboard.SetActive(false);
            }
            else
            {
                gameScoreboard.SetActive(true);
            }
        }
    }
}
