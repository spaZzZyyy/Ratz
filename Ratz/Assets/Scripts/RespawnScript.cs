using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void OnCollisionEnter2D(Collision2D respawn){
        if(respawn.gameObject.CompareTag("Player")) {
            player.transform.position = respawnPoint.transform.position;
        }
    }
}
