using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platForming : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    [SerializeField] Animator platformAni;
    float trackSpeed;

    void Update()
    {
        trackSpeed = musicManager.trackSpeed;
        //Debug.Log(trackSpeed);
        if (musicManager.musicIsPlaying == true) {
            switch(trackSpeed){
                case 1.2f:
                    //Debug.Log("speed 1.2");
                    platformAni.SetInteger("trackSpeed",5);
                break; 

                case 1.1f:
                    //Debug.Log("speed 1.1");
                    platformAni.SetInteger("trackSpeed",4);
                break; 

                case 1f:
                    //Debug.Log("Default Speed");
                    platformAni.SetInteger("trackSpeed",3);
                break; 

                case 0.9f:
                    //Debug.Log("speed 0.9");
                    platformAni.SetInteger("trackSpeed",2);
                break; 

                default: //Case 0.8f
                    //Debug.Log("speed 0.8");
                    platformAni.SetInteger("trackSpeed",1);
                break;
            }
        }
    }
}
