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
    
    public double musicTimer = 0;
    int trackPointer = 1;
    int trackMax;
    List<AudioClip> musicToPlay;
    [SerializeField] ScriptControls scriptControls;

    void Start()
    {
        List<AudioClip> musicToPlay = new List<AudioClip>();
        musicToPlay.Add(muTrackOne);
        musicToPlay.Add(muTrackTwo);
        
        trackMax = musicToPlay.Count;
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

        if (trackPointer < trackMax){
            trackPointer++;
        }
        else {
            trackPointer = 0;
        }

        audioSource.clip = musicToPlay[trackPointer];
    }

    void stopMusic(){
        musicTimer = audioSource.time;
        audioSource.Pause();
    }

    void startMusic(){
        audioSource.clip = musicToPlay[trackPointer];
        audioSource.PlayScheduled(musicTimer);
    }
}
