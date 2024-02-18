using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DashParticalSystem : MonoBehaviour
{
    ParticleSystem dashPS;
    [SerializeField] ScriptControls scriptControls;
    float dashThic;
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
        dashThic = transform.localScale.y;
    }

    private void playDashedPart()
    {
        flip();
        if(Input.GetKey(scriptControls.moveLeft) || Input.GetKey(scriptControls.moveRight)){
            dashPS.Play();
        } 
    }

    void flip(){
        #region FlipSprite
                if (Input.GetKey(scriptControls.moveLeft))
                {
                    Vector3 localScale = transform.localScale;
                    localScale.y = -dashThic;
                    transform.localScale = localScale;
                }

                if (Input.GetKey(scriptControls.moveRight))
                {
                    Vector3 localScale = transform.localScale;
                    localScale.y = dashThic;
                    transform.localScale = localScale;
                }
            #endregion
    }
}
