using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseStateMachine 
{
    public CheeseState currentCheeseState {  get; set; }

    public void Initialize (CheeseState startState)
    {
        currentCheeseState = startState; ;
        currentCheeseState.EnterState();
    }

    public void ChangeState(CheeseState state)
    {
        currentCheeseState.ExitState();
        currentCheeseState = state;
        currentCheeseState.EnterState();
    }
}
