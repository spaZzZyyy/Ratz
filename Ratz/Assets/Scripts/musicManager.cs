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
    [SerializeField] AudioSource track3;
    [SerializeField] AudioSource track4;
    [SerializeField] AudioSource stopTime;
    /*
    track1 = normal
    track2 = halftime
    track3 = madness
    track4 = madness halftime
    */
    double musicTimer = 0;
    List<AudioSource> trackList;
    [HideInInspector] public int trackToPlay = 0;
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
    bool stopTimePlaying = false;
    [SerializeField] ScriptControls scriptControls;
    public bool musicChanged;
    public PlayerControls playerControls;
    private InputAction changeSong;
    private InputAction slowSong;
    #endregion

    //! eliCode
    [SerializeField] BeatManager beatManager;
    [SerializeField] ResourceManager resourceManager;
    //!
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
    }

    private void OnDisable()
    {
        changeSong.Disable();
        slowSong.Disable();
    }


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

        if (startGame == true && gameStarted == false) {
            startMusic();
            stopTime.Play();
            stopTime.Pause();
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

        #region StopTime
        if (musicIsPlaying == false && stopTimePlaying == false) {
            stopTimePlaying = true;
            stopTime.UnPause();
        } else if (musicIsPlaying == true) {
            stopTime.Pause();
            stopTimePlaying = false;
        }
        #endregion

    }

    private void FixedUpdate() {
        if (musicIsPlaying) {
            musicTimer += musicPerSecond;
        }
    }

    void switchTracks(){
        if (trackToPlay < numOfTracks){
            trackToPlay++;
            musicChanged = true;
        } else {
            trackToPlay = 0;
            musicChanged = true;
        }
        audioSourceToPlay = trackList[trackToPlay];
        
        //! eliCode
        beatManager._audioSource = trackList[trackToPlay];
        if (trackToPlay == 1) {
            beatManager._bpm = beatManager._bpm / 2;
        } else {
            beatManager._bpm = beatManager._bpm * 2;
        }
    }

    void stopMusic() {
        musicIsPlaying = false;
        for (int i = 0; i < numOfTracks; i++) {
            trackList[i].Pause();
            trackList[i].mute = true;
        }
    }

    void startMusic() {
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
            stopMusic();
            Actions.OnPlayerSwitchTrack();
            switchTracks();
            startMusic();
        }
    }

    void slowDownTrackAction(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            //For the slow song, put all code in here.  Everything else should be set up when the button is pressed.
            Debug.Log("THIS IS WHERE CHANGING TO SLOW SONG NEEDS TO BE PUT");
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
