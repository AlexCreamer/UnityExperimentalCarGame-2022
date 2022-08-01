using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            MeshRenderer playerMesh = other.gameObject.GetComponentInChildren<MeshRenderer>();
            GetComponent<MeshRenderer>().material.color = playerMesh.material.color;
            gameObject.tag = "Hit";

            //Scorekeeper +1 score for player ID
            MyNetworkPlayer NP = GetComponentInChildren<MyNetworkPlayer>();
            Scoreboard.addPoint(NP.PlayerSteamID, 1);
        }
    }

}
