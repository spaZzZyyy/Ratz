using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class startBox : MonoBehaviour
{   
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip muTrackOne;
    [SerializeField] AudioClip muTrackTwo;
    
    [SerializeField] double musicTimer = 0;
    int trackPointer = 0;
    int trackMax;
    AudioClip audioClipToPlay;
    List<AudioClip> musicToPlay;
    [SerializeField] ScriptControls scriptControls;

    void Start()
    {
        //setTrack();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        //StartCoroutine("playMusic");
    }

    void Update()
    {
        if(Input.GetKeyDown(scriptControls.musicStartStop)){
            stopMusic();
        }
        if(Input.GetKeyUp(scriptControls.musicStartStop)){
            startMusic();
        }

        if(Input.GetKeyDown(scriptControls.switchTracks)){
            stopMusic();
            switchTracks();
            startMusic();
        }
        
    }

    void switchTracks(){

        if (trackPointer < trackMax-1){
            trackPointer++;
        }
        else {
            trackPointer = 0;
        }

        //setTrack();
    }

    void stopMusic(){
        musicTimer = audioSource.time;
        audioSource.Pause();
    }

    void startMusic(){
        audioSource.clip = audioClipToPlay;
        audioSource.PlayScheduled(musicTimer);
    }

/*Depricated , switched to using seperate audio sources for tracks. 
    void setTrack(){
        List<AudioClip> musicToPlay = new List<AudioClip>();
        musicToPlay.Add(muTrackOne);
        musicToPlay.Add(muTrackTwo);
        
        trackMax = musicToPlay.Count;
        audioClipToPlay = musicToPlay[trackPointer];
    }
    */
}
