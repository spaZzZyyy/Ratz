using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour
{
   [SerializeField] healthManager healthManager;

   
    private void OnCollisionEnter2D(Collision2D respawn){
        if(respawn.gameObject.CompareTag("Player")) {
            healthManager.takeDamage(2);
            healthManager.respawn = true;
        }
    }
}
