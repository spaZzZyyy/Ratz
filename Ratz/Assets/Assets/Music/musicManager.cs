using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class musicManager : MonoBehaviour
{   
    [SerializeField] AudioSource track1;
    [SerializeField] AudioSource track2;
    double musicTimer = 0;
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
        /*Used for debugging
        if(audioSourceToPlay == trackList[0]){
            Debug.Log("track1");
        }
        if(audioSourceToPlay == trackList[1]){
            Debug.Log("track2");

        }
        */

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
        if (trackToPlay < numOfTracks - 1){
            trackToPlay++;
        } else {
            trackToPlay = 0;
        }
        audioSourceToPlay = trackList[trackToPlay];
    }

    void stopMusic(){
        musicIsPlaying = false;
        for (int i = 0; i < numOfTracks; i++){
            trackList[i].Pause();
            trackList[i].mute = true;
        }
    }

    void startMusic(){
        musicIsPlaying = true;
        for (int i = 0; i < numOfTracks; i++){
            trackList[i].UnPause();
        }
        audioSourceToPlay.mute = false;
    }

    void startMusicBox(){
        trackList = new List<AudioSource>();
        trackList.Add(track1);
        trackList.Add(track2);
        numOfTracks = trackList.Count;
        for (int i = 0; i < numOfTracks; i++){
            trackList[i].Play();
            trackList[i].mute = true;
            trackList[i].Pause();
        }
    //Puts all tracks in a pause state.

    }

}
