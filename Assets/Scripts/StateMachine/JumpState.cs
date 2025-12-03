using UnityEngine;

public class JumpState : AirborneState
{
    public override bool IsAvailable => surfaceContactSensor != null && surfaceContactSensor.GroundHit;

    [SerializeField] private SurfaceContactSensor surfaceContactSensor;
    [SerializeField] private float jumpStrength;

    protected override void OnEnable()
    {
        physicsController2D.AddVelocity(Vector2.up * jumpStrength, AdditionalVelocityType.Additional);
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        if (velocity.y <= 0)
            SetStateComplete();
    }

    private void OnDisable()
    {

    }
}
