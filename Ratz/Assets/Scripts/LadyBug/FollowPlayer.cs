
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;
    Transform headTrans;
    Animator ani;
    public bool ikActive = false;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        headTrans = GetComponent<Transform>();
        ani = GetComponent<Animator>();
    }

    void OnAnimatorIK() {
        if(ikActive){
            if(player != null){
                if (headTrans != null){
                    ani.SetLookAtWeight(1);
                    ani.SetLookAtPosition(player.transform.position);
                }
            }
        } else {
            ani.SetLookAtWeight(0);
        }
    }
}
