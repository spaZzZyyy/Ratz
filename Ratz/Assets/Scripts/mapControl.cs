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
    //bool canChangeSpeed = true;
    bool musicPaused = false;
    int trackSpeedMax = 5;
    int trackSpeedMin = 1;
    int trackPlaying;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        trackTime = 3;
        trackSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Track Speed: " + trackSpeed);
        //Debug.Log("Track time: " + trackTime);

        #region Speed Control
        if (musicPaused == false){
            ani.speed = trackSpeed;
        }
        switch(trackTime){
            case 5:
                trackSpeed = 5f;
            break;
            case 4:
                trackSpeed = 3f;
            break;
            case 3:
                trackSpeed = 1f;
            break;
            case 2:
                trackSpeed = 0.5f;
            break;

            default: // case 1
                trackSpeed = 0.1f;
            break;
        }
        

        if (Input.GetKeyDown(scriptControls.musicStartStop)){ // Pause Music
            ani.speed = 0;
            musicPaused = true;
        }
        if (Input.GetKeyUp(scriptControls.musicStartStop)){ // UnPause Music
            ani.speed = trackSpeed;
            musicPaused = false;
        }

        if(Input.GetKeyDown(scriptControls.pitchUp)){
            if(trackTime < trackSpeedMax){
                trackTime += 1;
            }
        }

        if(Input.GetKeyDown(scriptControls.pitchDown)){
            if(trackTime > trackSpeedMin){
                trackTime -= 1;
            }
        }
        #endregion

        #region Track Control
        Debug.Log(musicManager.trackToPlay);
        if(musicManager.trackToPlay == 0 && trackPlaying != 0){ // first track
            ani.SetTrigger("SwitchToTrack1");
            trackPlaying = 0;
        }
        if(musicManager.trackToPlay == 1 && trackPlaying != 1){ // Second track
            ani.SetTrigger("SwitchToTrack2");
            trackPlaying = 1;
        }
        #endregion
    }
}
