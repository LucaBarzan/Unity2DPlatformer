using System.Collections.Generic;

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

        State = null;
    }

    private bool ContainsState(State state) => states != null && states.Count > 0 && states.Contains(state);

    public List<State> GetActiveStateBranch(List<State> states = null)
    {
        if (states == null)
            states = new List<State>();

        if (State == null || !State.enabled)
            return states;

        states.Add(State);
        return State.StateMachine.GetActiveStateBranch(states);
    }
}