using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerState currentState;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        PlayerState next = currentState.RunState();
        if (next != null)
        {
            currentState = next;
        }
    }
}
