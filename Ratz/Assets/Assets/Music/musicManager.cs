using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class musicManager : MonoBehaviour
{   
    [SerializeField] AudioSource track1;
    [SerializeField] AudioSource track2;
    
    [SerializeField] double musicTimer = 0;
    List<AudioSource> trackList;
    int trackToPlay = 0;
    AudioSource audioSourceToPlay;
    bool musicIsPlaying;
    int numOfTracks;
    const double musicPerSecond = 0.015;
    [SerializeField] ScriptControls scriptControls;

    void Start()
    {
        startMusicBox();
        audioSourceToPlay = trackList[trackToPlay];
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

    private void FixedUpdate() {
        if(musicIsPlaying){
            musicTimer += musicPerSecond;
        }
    }

    void switchTracks(){
        if (trackToPlay < numOfTracks-1){
            trackToPlay++;
        } else {
            trackToPlay = 0;
        }
        audioSourceToPlay = trackList[trackToPlay];
    }

    void stopMusic(){
        musicIsPlaying = false;
        audioSourceToPlay.Pause();
    }

    void startMusic(){
        musicIsPlaying = true;
        audioSourceToPlay.PlayScheduled(musicTimer);
    }

    void startMusicBox(){
        
        trackList = new List<AudioSource>();
        trackList.Add(track1);
        trackList.Add(track2);
        numOfTracks = trackList.Count;
        for (int i = 1; i < numOfTracks; i++){
            trackList[i].Play();
            trackList[i].Pause();
        }
        
        /*
        track1.Play();
        track2.Play();
        track1.Pause();
        track2.Pause();
        */
    }

}
