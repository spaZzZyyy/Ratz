using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player Controls")]
public class ScriptControls : ScriptableObject
{
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode moveJump;
    public KeyCode moveCrouch;
    public KeyCode dash;
    public KeyCode parry;
}
