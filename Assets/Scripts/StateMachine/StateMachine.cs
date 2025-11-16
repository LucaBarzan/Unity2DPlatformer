using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public sealed class StateMachine
{
    public State State { get; private set; }

    public void Set(State newState, bool forceReset = false)
    {
        if (newState != State || forceReset)
        {
            State?.Exit();
            State = newState;
            State?.Enter();
        }
    }
}