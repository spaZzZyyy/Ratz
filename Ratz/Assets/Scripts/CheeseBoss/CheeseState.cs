using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseState 
{
    protected Enemy enemy;
    protected CheeseStateMachine cheeseStateMachine;

    public CheeseState(Enemy enemy, CheeseStateMachine cheeseStateMachine)
    {
        this.enemy = enemy;
        this.cheeseStateMachine = cheeseStateMachine;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
