using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] bool turnOn;
    [SerializeField] private float timer;
    bool fall1 = false;
    bool fall2 = false;
    bool fall3 = false;

    // Update is called once per frame
    void Update()
    {
        if (turnOn)
        {
            timer = 5f;
            turnOn = false;
            fall1 = true;
            fall2 = true;
            fall3 = true;
        }

        timer -= Time.deltaTime;

        if (timer < 3f && fall1)
        {
            fall1 = false;
            transform.GetChild(0).gameObject.GetComponent<pillarMovement>().boolFall = true;
        }
        if (timer < 1f && fall2)
        {
            fall2 = false;
            transform.GetChild(1).gameObject.GetComponent<pillarMovement>().boolFall = true;
        }
        if (timer < 0f && fall3)
        {
            fall3 = false;
            transform.GetChild(2).gameObject.GetComponent<pillarMovement>().boolFall = true;
        }
        

    }
}
