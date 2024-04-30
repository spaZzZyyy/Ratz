using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    private int health;
    [SerializeField] private TMP_Text textHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        textHealth.text = "Health: " + health.ToString();
    }

    public void damage()
    {
        health--;
        textHealth.text = "Health: " + health.ToString();
    }

}
