using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CheeseIdleState : CheeseState
{
    [SerializeField] private float waitTime = 5f;
    private float timeWait;
    public CheeseIdleState(Enemy enemy, CheeseStateMachine cheeseStateMachine) : base(enemy, cheeseStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        timeWait = waitTime;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timeWait -= Time.deltaTime;

        if (timeWait <=0) {
            enemy.StateMachine.ChangeState(enemy.attackState);
        }
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
