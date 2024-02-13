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
    float trackSpeed;
    const float pitchIncrements = 0.1f;
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
        #region Control
            //stop music
            if(Input.GetKeyDown(scriptControls.musicStartStop)){ //Note since key down and up holding freezes music
                stopMusic();
            }
            //start music
            if(Input.GetKeyUp(scriptControls.musicStartStop)){ //and letting go resumes
                startMusic();
            }

            //switch tracks
            if(Input.GetKeyDown(scriptControls.switchTracks)){
                stopMusic();
                switchTracks();
                startMusic();
            }
            
            if(Input.GetKeyDown(scriptControls.pitchUp)){
                changePitch(true);
            }
            if(Input.GetKeyDown(scriptControls.pitchDown)){
                changePitch(false);
            }

        #endregion
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
            trackList[i].pitch = 1;
        }
    //Puts all tracks in a pause state.

    trackSpeed = 1;
    }


    void setMusicSpeed(){
        for (int i = 0; i < numOfTracks; i++)
        {
            trackList[i].pitch = trackSpeed;
        }
    }

    void changePitch(bool upDown){ // true for up false for down
        if(upDown == true){
            trackSpeed += pitchIncrements;
        }
        if (upDown == false){
            trackSpeed -= pitchIncrements;
        }
        setMusicSpeed();
    }
}
