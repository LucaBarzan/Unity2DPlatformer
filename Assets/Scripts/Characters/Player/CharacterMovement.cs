using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Vector3 Position => Transform.position;

    [SerializeField] private SurfaceContactSensor contactSensor;
    [SerializeField] private GroundedState groundedState;
    [SerializeField] private AirborneState airborneState;

    protected readonly StateMachine stateMachine = new StateMachine();

    protected virtual void Awake()
    {
        Transform = transform;

        stateMachine.Add(groundedState).Add(airborneState);
    }

    protected virtual void OnEnable()
    {
        groundedState.OnCompleted += GroundedState_OnCompleted;
        airborneState.OnCompleted += AirborneState_OnCompleted;
    }

    private void AirborneState_OnCompleted() => SelectState();

    private void GroundedState_OnCompleted() => SelectState();

    protected virtual void Start() => SelectState();

    protected virtual void OnDisable()
    {
        stateMachine.DisableAllStates();
    }

    protected virtual void SelectState()
    {
        if (contactSensor.GroundHit)
        {
            stateMachine.Set(groundedState);
        }
        else
        {
            stateMachine.Set(airborneState);
        }
    }
}