using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLobbyMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject lobbyCanvas = GameObject.Find("LobbyCanvas");
        lobbyCanvas.SetActive(false);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
