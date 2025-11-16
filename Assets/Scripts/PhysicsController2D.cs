using UnityEngine;

public class PhysicsController2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;

    private Vector2 velocity;
    private Vector2 additionalVelocity;
    private Vector2 baseVelocity;
    private Vector2 overrideVelocity;
    private bool overrideActive;

    private void FixedUpdate()
    {
        if (rigidbody2D == null)
            return;

        // Override and Additional velocity changes must be carried over
        if (overrideActive)
            velocity = overrideVelocity;

        velocity += additionalVelocity;

        // Base force cannot be accumulated to not increase over time
        var finalVelocity = velocity + baseVelocity;
        rigidbody2D.linearVelocity = finalVelocity;

        additionalVelocity = baseVelocity = overrideVelocity = Vector2.zero;
        overrideActive = false;
    }

    /// <summary>
    /// Adds the specified velocity to the object based on the given velocity type.
    /// </summary>
    /// <param name="velocity">The velocity to be added, represented as a <see cref="Vector2"/>.</param>
    /// <param name="additionalVelocityType">Specifies the type of velocity to which the value will be added. Use <see
    /// cref="AdditionalVelocityType.Additional"/> to add to the additional velocity, or <see
    /// cref="AdditionalVelocityType.Base"/> to add to the base velocity.</param>
    public virtual void AddVelocity(Vector2 velocity, AdditionalVelocityType additionalVelocityType)
    {
        switch (additionalVelocityType)
        {
            default:
            case AdditionalVelocityType.Additional:
                additionalVelocity += velocity;

                break;
            case AdditionalVelocityType.Base:
                baseVelocity += velocity;
                break;
        }
    }

    // TODO
    // public abstract void ApplyImpulseForce(Vector2 force);

    /// <summary>
    /// Overrides the current velocity with the specified force.
    /// </summary>
    /// <remarks>This method directly sets the velocity to the specified force, replacing any existing
    /// velocity. Use this method to explicitly control the object's movement.</remarks>
    /// <param name="force">The force to apply, represented as a <see cref="Vector2"/>.</param>
    public virtual void OverrideVelocity(Vector2 force)
    {
        overrideActive = true;
        overrideVelocity = force;
    }
}