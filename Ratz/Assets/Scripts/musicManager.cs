using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class musicManager : MonoBehaviour
{
    #region Variables
    [SerializeField] AudioSource track1;
    [SerializeField] AudioSource track2;


    // [SerializeField] AudioSource track3;
    // [SerializeField] AudioSource track4;


    //[SerializeField] AudioSource stopTime;
    /*
    track1 = normal
    track2 = halfime
    track3 = madness
    track4 = madness halftime
    */
    double musicTimer = 0;
    List<AudioSource> trackList;
     public int trackToPlay = 0;
    AudioSource audioSourceToPlay;
    [HideInInspector] public bool musicIsPlaying;
    public int numOfTracks;
    public float trackSpeed;
    const float pitchIncrements = 0.1f;
    const double musicPerSecond = 0.015;
    const float pitchMax = 1.2f;
    const float pitchMin = 0.8f;
    [HideInInspector] public bool startGame = false;
    bool gameStarted = false;
    //bool stopTimePlaying = false;
    [SerializeField] ScriptControls scriptControls;
    public bool musicChanged;
    public PlayerControls playerControls;
    private InputAction changeSong;
    private InputAction slowSong;
    //! eliCode
    [SerializeField] BeatManager beatManager;
    [SerializeField] ResourceManager resourceManager;
    public bool halfOut;
    public bool madOut;
    private bool halfIt;
    public bool platformMad;
    AudioLowPassFilter audioFilter1;
    AudioLowPassFilter audioFilter2;
    //!
    #endregion

    
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {

        changeSong = playerControls.Gameplay.ChangeSong;
        changeSong.Enable();
        changeSong.performed += switchTracksAction;

        slowSong = playerControls.Gameplay.Slow;
        slowSong.Enable();
        slowSong.performed += slowDownTrackAction;

        Actions.OnBossStart += startMusic;
    }

    private void OnDisable()
    {
        Actions.OnBossStart -= startMusic;
        changeSong.Disable();
        slowSong.Disable();
    }


    void Start()
    {
        startMusicBox();
        audioSourceToPlay = trackList[trackToPlay];
        platformMad = false;
        madOut = true;
        halfOut = true;
        audioFilter1 = track1.GetComponent(typeof(AudioLowPassFilter)) as AudioLowPassFilter;
        audioFilter2 = track2.GetComponent(typeof(AudioLowPassFilter)) as AudioLowPassFilter;
        
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
        //track1.AudioDistortionFilter.distortionLevel = 1;



        if(halfOut == true) {
            if(trackToPlay == 2) {
                stopMusic();
                halftimeTracks();
                changeHalfTime();
                startMusic();
                resourceManager.halfOn = false;
            }
        }
        

        if (startGame == true && gameStarted == false) {
            startMusic();
            // stopTime.Play();
            // stopTime.Pause();
            gameStarted = true;
            Debug.Log("Game start");
        }

        #region Control
        //stop music
        /*if(Input.GetKeyDown(scriptControls.musicStartStop)){ //Note since key down and up holding freezes music
            stopMusic();
        }
        //start music
        if(Input.GetKeyUp(scriptControls.musicStartStop) && startGame == true){ //and letting go resumes
            startMusic();
        }*/



        /*  if(Input.GetKeyDown(scriptControls.pitchUp)){
              changePitch(true);
          }
          if(Input.GetKeyDown(scriptControls.pitchDown)){
              changePitch(false);
          }*/
        #endregion

        // #region StopTime
        // if (musicIsPlaying == false && stopTimePlaying == false) {
        //     stopTimePlaying = true;
        //     stopTime.UnPause();
        // } else if (musicIsPlaying == true) {
        //     stopTime.Pause();
        //     stopTimePlaying = false;
        // }
        // #endregion

    }

    private void FixedUpdate() {
        if (musicIsPlaying) {
            musicTimer += musicPerSecond;
        }
    }

    void halftimeTracks(){
        //going to halftime
        if (trackToPlay == 0){
            trackToPlay++;
            musicChanged = true;
            //trackSpeed = .5f;
            halfIt = true;
            beatManager._bpm = beatManager._bpm / 2;
            resourceManager.halfOn = true;

        //leaving halftime
        } else {
            trackToPlay--;
            musicChanged = true;
            //trackSpeed = 1;
            halfIt = false;
            beatManager._bpm = beatManager._bpm * 2;
            resourceManager.halfOn = false;
        }
        audioSourceToPlay = trackList[trackToPlay];
        beatManager._audioSource = trackList[trackToPlay];
        
        
        // beatManager._audioSource = trackList[trackToPlay];
        // if (trackToPlay == 1) {
        //     beatManager._bpm = beatManager._bpm / 2;
        // } else {
        //     beatManager._bpm = beatManager._bpm * 2;
        // }
    }

    void madnessTracks(){
        //entering madness
        if (platformMad == false){
            // trackToPlay = trackToPlay + 2;
            // musicChanged = true;
            //!turning off boxCollider goes here
            platformMad = true;
            resourceManager.madOn = true;
            Actions.OnPlayerEnterMadness();

        //leaving madness
        } else {
            // trackToPlay = trackToPlay - 2;
            // musicChanged = true;
            //!turning on boxCollider goes here
            platformMad = false;
            resourceManager.madOn = false;
            Actions.OnPlayerExitMadness();
        }
        // audioSourceToPlay = trackList[trackToPlay];
        // beatManager._audioSource = trackList[trackToPlay];
    }

    void stopMusic() {
        musicIsPlaying = false;
        for (int i = 0; i < numOfTracks; i++) {
            trackList[i].Pause();
            trackList[i].mute = true;
        }
    }

    public void startMusic() {
        musicIsPlaying = true;
        for (int i = 0; i < numOfTracks; i++) {
            trackList[i].UnPause();
        }
        audioSourceToPlay.mute = false;
    }

    void startMusicBox() { // Creates a list to iterate through all the tracks and prime them to a ready state
        trackList = new List<AudioSource>();
        trackList.Add(track1);
        trackList.Add(track2);
        numOfTracks = trackList.Count;
        for (int i = 0; i < numOfTracks; i++) {
            trackList[i].Play();
            trackList[i].mute = true;
            trackList[i].Pause();
            trackList[i].pitch = 1;
        }
        //Puts all tracks in a pause state.
        trackSpeed = 1;
    }

   
    void switchTracksAction(InputAction.CallbackContext ctx)
    {
        //switch tracks
        if (ctx.performed)
        {
            // stopMusic();
            // Actions.OnPlayerSwitchTrack();
            // Debug.Log("PlayerSwitchedTrack");
            //switchTracks();
            //startMusic();

            //check for resources if entering halftime
            if(trackToPlay == 0) {
                if(halfOut == false) {
                    stopMusic();
                    Actions.OnPlayerSwitchTrack();
                    halftimeTracks();
                    changeHalfTime();
                    startMusic();
                    //no more spamming, adds 10% everytime you start
                    resourceManager.halfAmount = resourceManager.halfAmount + (resourceManager.halfMax / 10);
                }
            } else {
                stopMusic();
                Actions.OnPlayerSwitchTrack();
                halftimeTracks();
                changeHalfTime();
                startMusic();
            }   
        }
    }
    //! NAMES ARE GOOFED, THIS IS FOR ENTERING MADNESS
    void slowDownTrackAction(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            //if(trackToPlay == 0 || trackToPlay == 1) {
            if(madOut == false) {
                //stopMusic();
                madnessTracks();
                changeMadness();
                //startMusic();
            //  }
            } //else {
                //stopMusic();
                //madnessTracks();
                //startMusic();
            //}
            
        }
    }

    void changeMadness() {
        for(int i = 0; i < numOfTracks; i++) {
            if(platformMad == true) {    
                audioFilter1.enabled = true;
                audioFilter2.enabled = true;
            } else {
                audioFilter1.enabled = false;
                audioFilter2.enabled = false;            }
        }
    }

    void changeHalfTime(){
        for (int i = 0; i < numOfTracks; i++)
        {
            if(halfIt) {
                trackList[i].timeSamples = trackList[i].timeSamples / 2;
            } else {
                trackList[i].timeSamples = trackList[i].timeSamples * 2;
            }
            
        }
    }

    public void switchTracksActionCopy()
    {
        //switch tracks
        
            stopMusic();
            Actions.OnPlayerSwitchTrack();
            Debug.Log("PlayerSwitchedTrack");
            //switchTracks();
            startMusic();

            //check for resources if entering halftime
            if (trackToPlay == 0)
            {
                if (halfOut == false)
                {
                    stopMusic();
                    Actions.OnPlayerSwitchTrack();
                    halftimeTracks();
                    changeHalfTime();
                    startMusic();
                    //no more spamming, adds 10% everytime you start
                    resourceManager.halfAmount = resourceManager.halfAmount + (resourceManager.halfMax / 10);
                }
            }
            else
            {
                stopMusic();
                Actions.OnPlayerSwitchTrack();
                halftimeTracks();
                changeHalfTime();
                startMusic();
            }
        
    }

    /*
    void setMusicSpeed(){
        for (int i = 0; i < numOfTracks; i++)
        {
            trackList[i].pitch = trackSpeed;
        }
    }

    
    void changePitch(bool upDown){ // true for up false for down
        if(upDown == true && trackSpeed < pitchMax){
            trackSpeed += pitchIncrements;
        }
        if (upDown == false && trackSpeed > pitchMin){
            trackSpeed -= pitchIncrements;
        }
        setMusicSpeed();
    }
    */
}
