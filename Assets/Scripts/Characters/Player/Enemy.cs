
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] WaypointPatrollerState patrolState;
    [SerializeField] MoveToTargetState chaseState;
    [SerializeField] AttackState attackState;

    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        stateMachine.Add(patrolState)
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
        stateMachine.Set(patrolState);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnDisable()
    {
        attackState.OnCompleted -= AttackState_OnCompleted;
    }

    private void AttackState_OnCompleted()
    {
        stateMachine.Set(chaseState);
    }

    public void OnPlayerInsideRangeAttack()
    {
        stateMachine.Set(attackState);
    }

    public void OnPlayerDetected(Collider2D collider2D)
    {
        chaseState.TargetTransform = collider2D.transform;
        stateMachine.Set(chaseState);
    }
}