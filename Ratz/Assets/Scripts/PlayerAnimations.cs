using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAnimations : MonoBehaviour
{
    public ScriptControls scriptControls;
    public ScriptMovement scriptMovement;
    PlayerMovement player_movement;
    Animator playerAni;
    bool onGround;
    bool playerRunning;
    PlayerControls playerControls;
    //Material material;

    private InputAction jump;
    private InputAction MoveLeft;
    private InputAction MoveRight;
    private InputAction DashButton;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }


    private void OnEnable()
    {
        /* Actions.OnPlayerJump += PlayerJumped;
         Actions.OnPlayerDashed += PlayerDashed;
         Actions.OnParry += PlayerParried;
         Actions.NotParry += PlayerNotParried;*/
        Actions.PlayerTookDamage += TakeDamage;
        Actions.OnPlayerSwitchTrack += switchTracks;
        Actions.OnPlayerEnterMadness += PlayerEnterMadness;
        Actions.OnPlayerExitMadness += PlayerExitMadness;
        jump = playerControls.Gameplay.Jump;
        jump.Enable();
        jump.performed += PlayerJumped;

        MoveLeft = playerControls.Gameplay.MoveLeft;
        MoveLeft.Enable();
        MoveLeft.performed += PlayerRun;

        MoveRight = playerControls.Gameplay.MoveRight;
        MoveRight.Enable();
        MoveRight.performed += PlayerRun;

        DashButton = playerControls.Gameplay.Dash;
        DashButton.Enable();
        DashButton.performed += PlayerDashed;

    }

    private void OnDisable()
    {
        //  Actions.OnPlayerJump -= PlayerJumped;
        /*   Actions.OnPlayerDashed -= PlayerDashed; */
        //Actions.OnParry -= PlayerParried;
        //Actions.NotParry -= PlayerNotParried;
        Actions.PlayerTookDamage -= TakeDamage;
        Actions.OnPlayerSwitchTrack -= switchTracks;
        Actions.OnPlayerEnterMadness -= PlayerEnterMadness;
        Actions.OnPlayerExitMadness -= PlayerExitMadness;
        jump.Disable();
        MoveLeft.Disable();
        MoveRight.Disable();
        DashButton.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAni = GetComponent<Animator>();
        player_movement = GetComponent<PlayerMovement>();
        //material = GetComponent<SpriteRenderer>().material;

    }

    void PlayerEnterMadness(){
        playerAni.SetTrigger("madSwitch");
        playerAni.SetBool("isMad", true);
    }

    void PlayerExitMadness(){
        playerAni.SetTrigger("madSwitch");
        playerAni.SetBool("isMad", false);
    }

    void switchTracks(){
        playerAni.SetTrigger("SwitchTrack");
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

    }

    private void Update()
    {        
        playerRunning = playerControls.Gameplay.MoveLeft.ReadValue<float>() > 0 || playerControls.Gameplay.MoveRight.ReadValue<float>() > 0;
        if(!playerRunning)
        {
            playerAni.SetBool("isRunning", false);
        }
    }


    private void PlayerDashed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GetComponent<PlayerMovement>()._canDash)
        {
            playerAni.SetBool("Dashed", true);
            StartCoroutine(OnDashed());
        }
    }

    private void PlayerRun(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            playerAni.SetBool("isRunning", true);
        }
    }

    public void PlayerJumped(InputAction.CallbackContext ctx){
        if (ctx.performed)
        {
            playerAni.SetTrigger("jumpKeyPressed");
            StartCoroutine(OnJump());
        }
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

    /*private void PlayerDashed()
 {
     if(Input.GetKey(scriptControls.moveRight) || Input.GetKey(scriptControls.moveLeft)){
         playerAni.SetBool("Dashed", true);
         StartCoroutine(OnDashed());
     }
 }*/

}
