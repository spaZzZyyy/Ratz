using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squishDetection : MonoBehaviour
{
    [SerializeField] healthManager healthManager;

    
     private void OnCollisionEnter2D(Collision2D collision) {
        healthManager.takeDamage(1);
    }
}
