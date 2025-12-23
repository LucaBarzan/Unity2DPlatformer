using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : StateMachineBehaviour
{
    public Vector3 Position => Transform.position;

    [SerializeField] private SurfaceContactSensor contactSensor;
    [SerializeField] private GroundedState groundedState;
    [SerializeField] private AirborneState airborneState;


    protected override void Awake()
    {
        base.Awake();

        StateMachine.Add(groundedState).Add(airborneState);
    }

    protected virtual void OnEnable()
    {
        groundedState.OnCompleted += GroundedState_OnCompleted;
        airborneState.OnCompleted += AirborneState_OnCompleted;
    }

    private void AirborneState_OnCompleted() => SelectState();

    private void GroundedState_OnCompleted() => SelectState();

    protected virtual void Start() => SelectState();

    protected virtual void SelectState()
    {
        if (contactSensor.GroundHit)
        {
            StateMachine.Set(groundedState);
        }
        else
        {
            StateMachine.Set(airborneState);
        }
    }
}