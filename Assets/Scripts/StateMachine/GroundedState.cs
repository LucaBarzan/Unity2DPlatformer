using UnityEngine;

public class GroundedState : State
{
    [SerializeField] private CharacterIdleState idleState;
    [SerializeField] private WalkState walkState;
    [SerializeField] private SlopeState slopeState;
    [SerializeField] private GroundDataHandler groundData;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    private Vector2 movementDirection => movementDirectionProvider.MoveDirection;

    private readonly StateMachine stateMachine = new StateMachine();

    protected override void Awake()
    {
        base.Awake();

        stateMachine.Add(walkState)
            .Add(idleState)
            .Add(slopeState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SelectState();
    }

    private void Update()
    {
        if (!groundData.IsGrounded)
        {
            SetStateComplete();
            return;
        }

        SelectState();
    }

    private void OnDisable()
    {
        stateMachine.DisableAllStates();
    }

    private void SelectState()
    {
        if (groundData.IsOnSlope)
        {
            stateMachine.Set(slopeState);
            return;
        }

        if (movementDirection == Vector2.zero)
        {
            stateMachine.Set(idleState);
            return;
        }

        stateMachine.Set(walkState);
    }
}
