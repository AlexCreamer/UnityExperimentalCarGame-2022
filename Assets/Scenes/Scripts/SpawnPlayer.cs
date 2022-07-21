using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject player;
    private Vector3 respawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject)Resources.Load("Player Car", typeof(GameObject));

        spawnLocation = GameObject.FindGameObjectWithTag("Spawn Point");

        respawnLocation = player.transform.position;

        SpawnCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCharacter()
    {
        GameObject.Instantiate(player, spawnLocation.transform.position, Quaternion.identity);
    }
}
