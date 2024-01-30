using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheeseChargeState : CheeseState
{

    Vector2 dir;
    Rigidbody2D rb;
    public CheeseChargeState(Enemy enemy, CheeseStateMachine cheeseStateMachine) : base(enemy, cheeseStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        rb = GetCo
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
