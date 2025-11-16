using UnityEngine;

public class GroundedState : State 
{
    private StateMachineBlackboard_CharacterMovement myBlackBoard => Blackboard as StateMachineBlackboard_CharacterMovement;

    public override void Enter()
    {
        base.Enter();

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // if(!myBlackBoard.SurfaceContactSensor.GroundHit)
        // {
        //     myBlackBoard.StateMachine.Set(myBlackBoard.AirborneState);
        //     return;
        // }
        // 
        // if(myBlackBoard.)
    }

    public override void Exit()
    {
        base.Exit();

    }
}
