using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.U2D.Animation;

public class PlayerAnimations : MonoBehaviour
{
    public ScriptControls scriptControls;
    public ScriptMovement scriptMovement;
    PlayerMovement player_movement;
    Animator playerAni;
    bool onGround;
    //Material material;

    private void OnEnable() {
        Actions.OnPlayerJump += PlayerJumped;
        Actions.OnPlayerDashed += PlayerDashed;
        Actions.OnParry += PlayerParried;
        Actions.NotParry += PlayerNotParried;
        Actions.PlayerTookDamage += TakeDamage;
    }

    private void OnDisable() {
        Actions.OnPlayerJump -= PlayerJumped;
        Actions.OnPlayerDashed -= PlayerDashed;
        Actions.OnParry -= PlayerParried;
        Actions.NotParry -= PlayerNotParried;
        Actions.PlayerTookDamage -= TakeDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAni = GetComponent<Animator>();
        player_movement = GetComponent<PlayerMovement>();
        //material = GetComponent<SpriteRenderer>().material;

    }

    void TakeDamage(){
        playerAni.SetTrigger("gotHurt");
        // material.SetInt("_OnOff", 1); //sets onOff to true
        // material.SetColor("_Color", new Color(1,0,0,2));
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
        if((Input.GetKey(scriptControls.moveLeft) || Input.GetKey(scriptControls.moveRight)) && onGround){
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

    private void PlayerParried()
    {
        playerAni.SetBool("isParry", true);
    }

    private void PlayerNotParried()
    {
        playerAni.SetBool("isParry", false);
    }


}
