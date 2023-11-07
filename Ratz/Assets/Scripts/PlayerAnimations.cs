using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class PlayerAnimations : MonoBehaviour
{
    public ScriptControls scriptControls;
    PlayerMovement player_movement;
    Animator playerAni;
    bool onGround;

    private void OnEnable() {
        Actions.OnPlayerJump += PlayerJumped;
    }
    private void OnDisable() {
        Actions.OnPlayerJump -= PlayerJumped;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAni = GetComponent<Animator>();
        player_movement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate() {
        #region Grounded
        if(onGround = player_movement.IsGrounded()){
            playerAni.SetBool("isGrounded", true);
        }
        else{
            playerAni.SetBool("isGrounded", false);
        }
        #endregion

        #region RunningAnimation
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && onGround){
            playerAni.SetBool("isRunning", true);
        }
        else{
            playerAni.SetBool("isRunning", false);
        }
        #endregion

        #region JumpingAnimation
        
        #endregion
    }

    public void PlayerJumped(){
        playerAni.SetTrigger("jumpKeyPressed");
    }
}
