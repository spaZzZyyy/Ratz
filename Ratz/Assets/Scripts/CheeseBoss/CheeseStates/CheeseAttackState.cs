using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseAttackState : CheeseState
{
    public CheeseAttackState(Enemy enemy, CheeseStateMachine cheeseStateMachine) : base(enemy, cheeseStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {

        base.EnterState();
        int randAttack = Random.Range(1, 6);
        switch (randAttack)
        {
            case 0:
                enemy.StateMachine.ChangeState(enemy.chargeState);
                break;
            case 1:
                break;
            case 2:
                break;
        }
        
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
}
