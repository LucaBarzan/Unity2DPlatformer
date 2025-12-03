using UnityEngine;

public class WanderState : State
{
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float waypointRestTime;

    private Transform waypointTransform => waypoints[currentWaypoint];

    private MoveState state = MoveState.Idle;
    private int currentWaypoint;
    private float waypointRestTimer;
    private Vector2 moveDirection;

    private enum MoveState
    {
        Idle,
        Moving
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        state = MoveState.Moving;
        UpdateMovementDirection();
    } 

    private void Update()
    {
        switch (state)
        {
            case MoveState.Moving:
                MoveToTarget();
                break;

            case MoveState.Idle:
                if (Time.time >= waypointRestTimer)
                {
                    currentWaypoint++;
                    currentWaypoint %= waypoints.Length;
                    UpdateMovementDirection();
                    state = MoveState.Moving;
                }
                break;

        }
    }

    private void MoveToTarget()
    {
        if (ReachedOrPassedTarget())
        {
            movementDirectionProvider.Set(Vector2.zero);
            waypointRestTimer = waypointRestTime + Time.time;
            state = MoveState.Idle;
            return;
        }
        
        movementDirectionProvider.Set(moveDirection);
    }

    private bool ReachedOrPassedTarget()
    {
        if (waypointTransform == null)
            return true;

        return Vector2.Dot(GetTargetDirection(), moveDirection) <= 0.0f;
    }

    private void UpdateMovementDirection() => moveDirection = GetTargetDirection();
    private Vector2 GetTargetDirection() => (waypointTransform.position - characterTransform.position).normalized;
}
