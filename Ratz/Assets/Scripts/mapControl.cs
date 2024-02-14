using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapControl : MonoBehaviour
{
    Animator ani;
    [SerializeField] ScriptControls scriptControls;
    [SerializeField] musicManager musicManager;
    float trackSpeed;
    int trackTime;
    bool canChangeSpeed = true;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        trackTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        switch(trackTime){
            case 5:
                trackSpeed = 6;
            break;
            case 4:
                trackSpeed = 3;
            break;
            case 3:
                trackSpeed = 1;
            break;
            case 2:
                trackSpeed = 0.2f;
            break;

            default: // case 1
                trackSpeed = 0f;
            break;
        }
        ani.speed = trackSpeed;

        if (Input.GetKeyDown(scriptControls.musicStartStop)){ // Pause Music
            ani.speed = 0;
        }
        if (Input.GetKeyUp(scriptControls.musicStartStop)){ // UnPause Music
            ani.speed = trackSpeed;
        }

        if(trackTime > 1 && trackTime < 5){
            canChangeSpeed = true;
        }else {
            canChangeSpeed = false;
        }

        if(Input.GetKeyDown(scriptControls.pitchUp) && canChangeSpeed == true){
            trackTime += 1;
        }

        if(Input.GetKeyDown(scriptControls.pitchDown) && canChangeSpeed == true){
            trackTime -= 1;
        }
    }
}
