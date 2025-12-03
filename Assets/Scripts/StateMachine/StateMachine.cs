using System.Collections.Generic;
using UnityEngine;

public sealed class StateMachine
{
    public State State { get; private set; }

    private readonly HashSet<State> states = new();

    public StateMachine Add(State state)
    {
        states.Add(state);
        return this;
    }

    public void Set(State newState, bool forceReset = false)
    {
        if (ContainsState(newState) &&
            (newState != State || forceReset) && // Avoid 
            newState.IsAvailable)
        {
            if (State != null)
                State.enabled = false;

            State = newState;

            if (State != null)
                State.enabled = true;
        }
    }

    public void DisableAllStates()
    {
        foreach (var state in states)
            state.enabled = false;
    }

    private bool ContainsState(State state) => states != null && states.Count > 0 && states.Contains(state);

}