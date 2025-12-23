using UnityEngine;

public class GroundedState : State
{
    [SerializeField] private CharacterIdleState idleState;
    [SerializeField] private WalkState walkState;
    [SerializeField] private SlopeState slopeState;
    [SerializeField] private GroundDataHandler groundData;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    private Vector2 movementDirection => movementDirectionProvider.MoveDirection;

    protected override void Awake()
    {
        base.Awake();

        StateMachine.Add(walkState)
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

    private void SelectState()
    {
        if (groundData.IsOnSlope)
        {
            StateMachine.Set(slopeState);
            return;
        }

        if (movementDirection == Vector2.zero)
        {
            StateMachine.Set(idleState);
            return;
        }

        StateMachine.Set(walkState);
    }
}
