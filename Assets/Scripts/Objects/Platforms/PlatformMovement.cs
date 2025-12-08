using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private PhysicsController2D physicsController2D;
    [SerializeField] private WaypointPatrollerState patrolState;
    [SerializeField] private float speed = 10.0f;

    private void Start()
    {
        // No need to use a state machine since patrol is the only state being used
        patrolState.enabled = true;
    }

    private void FixedUpdate()
    {
        physicsController2D.SetVelocity(movementDirectionProvider.MoveDirection * speed);
    }
}