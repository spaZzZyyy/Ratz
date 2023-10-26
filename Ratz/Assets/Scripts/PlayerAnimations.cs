using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement player_movement;
    Animator playerAni;
    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        playerAni = GetComponent<Animator>();
        player_movement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate() {
        onGround = player_movement.IsGrounded();
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && onGround){
            playerAni.SetBool("isRunning", true);
        }
        else{
            playerAni.SetBool("isRunning", false);
        }
    }
}
