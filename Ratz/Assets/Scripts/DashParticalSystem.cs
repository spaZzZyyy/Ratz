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

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {       

        DashButton = playerControls.Gameplay.Dash;
        DashButton.Enable();
        DashButton.performed += playDashedPart;
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

    private void playDashedPart(InputAction.CallbackContext ctx)
    {
        flip();
        if(ctx.performed && playerMovement._canDash){
            dashPS.Play();
        } 
    }

    void flip(){
        #region FlipSprite
                if (playerControls.Gameplay.MoveLeft.ReadValue<float>() > 0)
                {
                    Vector3 localScale = transform.localScale;
                    localScale.y = -dashThic;
                    transform.localScale = localScale;
                }

                if (playerControls.Gameplay.MoveRight.ReadValue<float>() > 0)
                {
                    Vector3 localScale = transform.localScale;
                    localScale.y = dashThic;
                    transform.localScale = localScale;
                }
            #endregion
    }
}
