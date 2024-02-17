using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDmg : MonoBehaviour
{
    [SerializeField] healthManager healthManager;
    void OnCollisionEnter2D(Collision2D other)
    {
        healthManager.takeDamage(1);
    }
}
