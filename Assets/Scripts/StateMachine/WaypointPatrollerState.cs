using UnityEngine;

public class WaypointPatrollerState : State
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private MoveToTargetState moveToTargetState;
    [SerializeField] private IdleState idleState;

    private Transform waypointTransform => waypoints[currentWaypoint];

    private int currentWaypoint;
    private readonly StateMachine stateMachine = new StateMachine();

    protected override void Awake()
    {
        base.Awake();
        stateMachine.Add(moveToTargetState).Add(idleState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        moveToTargetState.OnCompleted += MoveToTargetState_OnCompleted;
        idleState.OnCompleted += IdleState_OnCompleted;

        moveToTargetState.TargetTransform = waypointTransform;
        stateMachine.Set(moveToTargetState);
    }

    private void OnDisable()
    {
        stateMachine.DisableAllStates();
        moveToTargetState.OnCompleted -= MoveToTargetState_OnCompleted;
        idleState.OnCompleted -= IdleState_OnCompleted;
    }

    private void MoveToTargetState_OnCompleted() => stateMachine.Set(idleState);

    private void IdleState_OnCompleted()
    {
        currentWaypoint++;
        currentWaypoint %= waypoints.Length;
        moveToTargetState.TargetTransform = waypointTransform;
        stateMachine.Set(moveToTargetState);
    }
}
