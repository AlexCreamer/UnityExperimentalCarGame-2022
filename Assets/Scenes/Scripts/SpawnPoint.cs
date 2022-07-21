using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject player;
    
    private Vector3 respawnLocation;

    private void Awake() 
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("Spawn Point");
    }

    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject)Resources.Load("Player Car", typeof(GameObject));

        respawnLocation = player.transform.position;

        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlayer()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        Debug.Log("Spawning player at " + spawnLocations[spawn].transform.position);
        GameObject.Instantiate(player, spawnLocations[spawn].transform.position, Quaternion.identity);

    }
}
