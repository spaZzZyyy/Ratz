using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squishDetection : MonoBehaviour
{
     private void OnCollisionEnter2D(Collision2D collision) {
        
        Debug.Log("I'm getting squished");
    }
}
