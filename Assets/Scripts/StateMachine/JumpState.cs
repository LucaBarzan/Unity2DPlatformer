using UnityEngine;

public class JumpState : AirborneState
{
    public override bool IsAvailable => surfaceContactSensor != null && surfaceContactSensor.GroundHit;

    [SerializeField] private float jumpStrength;

    protected override void OnEnable()
    {
        physicsController2D.AddVelocity(Vector2.up * jumpStrength, AdditionalVelocityType.Additional);
        base.OnEnable();
    }

    protected override bool IsStateComplete() => velocity.y <= 0;
}