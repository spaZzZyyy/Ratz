using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
   [SerializeField] private PlayerState jumpingState;
    public override PlayerState RunState()
    {
        Debug.Log("Grounded");
        return null;
    }
}
