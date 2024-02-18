using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Movement Variables")]
public class ScriptMovement : ScriptableObject
{
    public float gravityForce;
    public float movementSpeed;
    public float jumpForce;
    public int numJumps;
    public LayerMask groundLayer;
    public float groundCheckDistance;
    public float minJumpHeight;
    public float dashDistance;
    public float dashCoolDown;
    public float dashDuration;
    public float coyoteTime;
}
