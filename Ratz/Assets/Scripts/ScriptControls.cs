using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player Controls")]
public class ScriptControls : ScriptableObject
{
    //for movement
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode moveJump;
    public KeyCode moveCrouch;
    public KeyCode dash;
    
    //for combat
    public KeyCode parry;

    //For Music Control
    public KeyCode musicStartStop;
    public KeyCode switchTracks;
    public KeyCode pitchUp;
    public KeyCode pitchDown;
}
