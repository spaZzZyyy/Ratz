using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticalSystem : MonoBehaviour
{
    ParticleSystem dashPS;
    [SerializeField] ScriptControls scriptControls;
    void OnEnable()
    {
        Actions.OnPlayerDashed += playDashedPart;
    }
    void OnDisable() {
        Actions.OnPlayerDashed -= playDashedPart;
    }

    void Start()
    {
        dashPS = GetComponent<ParticleSystem>();
    }

    private void playDashedPart()
    {
        if(Input.GetKey(scriptControls.moveLeft) || Input.GetKey(scriptControls.moveRight)){
            dashPS.Play();
        } 
    }
}
