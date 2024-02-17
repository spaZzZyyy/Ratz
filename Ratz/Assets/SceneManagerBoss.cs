using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerBoss : MonoBehaviour
{
    [SerializeField] private GameObject Boss ;
    [SerializeField] private GameObject pillars ;
    [SerializeField] private GameObject left ;
    [SerializeField] private GameObject right ;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Boss.GetComponent<CheeseMovement>().attackChoice =1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            Boss.GetComponent<CheeseMovement>().attackChoice =2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            Boss.GetComponent<CheeseMovement>().attackChoice =3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            pillars.GetComponent<test>().turnOn = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            left.GetComponent<testSpears>().turnOn = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            right.GetComponent<testSpears>().turnOn = true;
        }
    }
}
