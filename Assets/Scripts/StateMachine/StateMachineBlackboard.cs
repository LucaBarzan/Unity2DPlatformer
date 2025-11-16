using UnityEngine;

public class StateMachineBlackboard
{
    public StateMachine StateMachine { get; private set; }
    
    public StateMachineBlackboard()
    {
        StateMachine = new StateMachine();
    }
}

[System.Serializable]
public class StateMachineBlackboard_CharacterMovement : StateMachineBlackboard
{
    public GroundedState GroundedState => groundedState;
    public AirborneState AirborneState => airborneState;
    public IdleState IdleState => idleState;
    public WalkState WalkState => walkState;

    public SurfaceContactSensor SurfaceContactSensor => surfaceContactSensor;

    [Header("States")]
    [SerializeField] private GroundedState groundedState;
    [SerializeField] private AirborneState airborneState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private WalkState walkState;
    
    [Header("Others")]
    [SerializeField] private SurfaceContactSensor surfaceContactSensor;
}