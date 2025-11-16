using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateMachineBlackboard Blackboard;
    public bool isComplete { get; protected set; }

    private float startTime;

    public float TimePassed => Time.time - startTime;

    private void Awake() => enabled = false;

    public virtual void Enter()
    {
        startTime = Time.time;
        enabled = true;
    }

    public virtual void Exit() => enabled = false;
}
