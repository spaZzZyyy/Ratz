using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject mainRespawnPoint;
    public GameObject respawnPoint;
    public GameObject player;
    

    private void OnTriggerEnter2D(Collider2D respawn){
        if(respawn.gameObject.CompareTag("respawn")) {
            respawnPoint = respawn.gameObject;
        }
        if(respawn.gameObject.CompareTag("mainRespawn")) {
            mainRespawnPoint = respawn.gameObject;
            respawnPoint = mainRespawnPoint;
        }
        //ForLights
        Actions.OnPlayerHitCheckPoint(respawn.gameObject);
    }

    public void Respawn(){
        player.transform.position = respawnPoint.transform.position;
    }
}
