using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class PlayerAnimations : MonoBehaviour
{
    public ScriptControls scriptControls;
    public ScriptMovement scriptMovement;
    PlayerMovement player_movement;
    Animator playerAni;
    bool onGround;

    private void OnEnable() {
        Actions.OnPlayerJump += PlayerJumped;
        Actions.OnPlayerDashed += PlayerDashed;
    }
    private void OnDisable() {
        Actions.OnPlayerJump -= PlayerJumped;
        Actions.OnPlayerDashed -= PlayerDashed;
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
    }

    private void PlayerDashed()
    {
        if(Input.GetKey(scriptControls.moveRight) || Input.GetKey(scriptControls.moveLeft)){
            playerAni.SetBool("Dashed", true);
            StartCoroutine(OnDashed());
        }
    }

    public void PlayerJumped(){
        playerAni.SetTrigger("jumpKeyPressed");
        StartCoroutine(OnJump());
    }

    IEnumerator OnDashed(){
        yield return new WaitForSeconds(scriptMovement.dashDuration);
        playerAni.SetBool("Dashed", false);
    }

    IEnumerator OnJump(){
        yield return new WaitForSeconds(0.1f);
        playerAni.SetBool("jumpKeyPressed", false);
    }

}
