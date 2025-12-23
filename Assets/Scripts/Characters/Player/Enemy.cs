
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] WaypointPatrollerState patrolState;
    [SerializeField] MoveToTargetState chaseState;
    [SerializeField] AttackState attackState;

    protected override void Awake()
    {
        base.Awake();
        StateMachine.Add(patrolState)
            .Add(chaseState)
            .Add(attackState);
    }

    private void OnEnable()
    {
        attackState.OnCompleted += AttackState_OnCompleted;
        Application.targetFrameRate = -1;
    }

    private void Start()
    {
        StateMachine.Set(patrolState);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        attackState.OnCompleted -= AttackState_OnCompleted;
    }

    private void AttackState_OnCompleted()
    {
        if (chaseState.TargetTransform != null)
            StateMachine.Set(chaseState);
        else
            StateMachine.Set(patrolState);
    }

    public void OnPlayerInsideRangeAttack()
    {
        StateMachine.Set(attackState);
    }

    public void OnPlayerDetected(Collider2D collider2D)
    {
        chaseState.TargetTransform = collider2D.transform;
        StateMachine.Set(chaseState);
    }
}