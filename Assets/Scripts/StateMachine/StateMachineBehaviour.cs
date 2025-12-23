using UnityEngine;

public abstract class StateMachineBehaviour : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public StateMachine StateMachine { get; private set; }

    protected virtual void Awake()
    {
        Transform = transform;
        StateMachine = new StateMachine();
    }

    protected virtual void OnDisable()
    {
        StateMachine.DisableAllStates();
    }
}