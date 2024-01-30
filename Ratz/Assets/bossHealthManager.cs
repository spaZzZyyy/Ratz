using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    private int health;
    [SerializeField] private GameObject boss; 
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    private void Update()
    {
        if(health <= 0) { 
            killBoss();
        }
    }

    public void damage()
    {
        health -=1;
    }

    private void killBoss()
    {
        Debug.Log("Boss Dead");
    }
}
