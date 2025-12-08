using UnityEngine;

public class CharacterIdleState : State
{
    [SerializeField] private PhysicsController2D physicsController2D;
    [SerializeField] private GroundDataHandler groundData;

    [SerializeField] private float deceleration;
    [SerializeField] private float gravity;

    private float horizontalSpeed = 0.0f;
    private Vector2 velocity;

    protected override void OnEnable()
    {
        base.OnEnable();
        velocity = physicsController2D.Velocity;
        horizontalSpeed = velocity.x;
    }

    private void Update()
    {
        HandleDirection();
        HandleGravity();
        physicsController2D.SetVelocity(velocity);
    }

    private void HandleDirection()
    {
        horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, deceleration * Time.deltaTime);

        // Handle Slope horizontal movement
        velocity = horizontalSpeed * groundData.GroundDirection;
    }

    private void HandleGravity()
    {
        // Add little gravity to stick to the ground
        velocity -= groundData.GroundNormal * gravity;
    }
}
