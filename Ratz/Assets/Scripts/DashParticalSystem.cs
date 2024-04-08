using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashParticalSystem : MonoBehaviour
{
    ParticleSystem dashPS;
    [SerializeField] ScriptControls scriptControls;
    float dashThic;
    public PlayerControls playerControls;
    private InputAction DashButton;
    private PlayerMovement playerMovement;
    private bool heldLeft;
    private bool heldRight;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {       

        DashButton = playerControls.Gameplay.Dash;
        DashButton.Enable();
        DashButton.performed += playDashedPart;

        playerControls.Gameplay.MoveLeft.Enable();
        playerControls.Gameplay.MoveRight.Enable();


    }
    void OnDisable() {
        DashButton.Disable();
    }

    void Start()
    {
        dashPS = GetComponent<ParticleSystem>();
        dashThic = transform.localScale.y;
        playerMovement = this.transform.parent.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        heldLeft = playerControls.Gameplay.MoveLeft.ReadValue<float>() > 0;
        heldRight = playerControls.Gameplay.MoveRight.ReadValue<float>() > 0;
    }

    private void playDashedPart(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Dashed try");
        flip();
        if(ctx.performed && playerMovement._canDash){
            dashPS.Play();
        } 
    }

    void flip(){
        #region FlipSprite

                if (playerControls.Gameplay.MoveLeft.ReadValue<float>() > 0)
                {
                    Debug.Log("Left");
                    Vector3 localScale = transform.localScale;
                    localScale.y = -dashThic;
                    transform.localScale = localScale;
                }

                if (playerControls.Gameplay.MoveRight.ReadValue<float>() > 0)
                {
                    Debug.Log("right");
                    Vector3 localScale = transform.localScale;
                    localScale.y = dashThic;
                    transform.localScale = localScale;
                }
            #endregion
    }
}
