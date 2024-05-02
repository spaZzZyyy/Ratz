using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMap : MonoBehaviour
{
    bool bossMapState = true; //False for state2 true for state1
    Animator bossMapAni;
    private void OnEnable() {
        Actions.OnPlayerSwitchTrack += switchState;
    }

    private void OnDisable() {
        Actions.OnPlayerSwitchTrack -= switchState;
    }

    void Start()
    {
        bossMapAni = GetComponent<Animator>();
    }
    void switchState(){
        if (bossMapState == true){
            bossMapAni.SetTrigger("SwitchState2");
            bossMapState = false;
        }
        else if (bossMapState == false){
            bossMapAni.SetTrigger("SwitchState1");
            bossMapState = true;
        }
    }
}
