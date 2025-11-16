using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Detects surface contacts (ground, walls, and ceiling) using BoxCast queries.
/// Attach this to a GameObject with a BoxCollider2D to sense nearby surfaces.
/// </summary>
public class SurfaceContactSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask contactLayerMask;

    [Header("Ground Detection")]
    [SerializeField] private bool checkGround = true;
    [SerializeField, Min(0f)] private float groundCheckDistance = 0.05f;

    [Header("Wall Detection")]
    [SerializeField] private bool checkWalls = true;
    [SerializeField, Min(0f)] private float wallCheckDistance = 0.05f;

    [Header("Ceiling Detection")]
    [SerializeField] private bool checkCeiling = true;
    [SerializeField, Min(0f)] private float ceilingCheckDistance = 0.05f;

    public RaycastHit2D CeilingHit { get; private set; }
    public RaycastHit2D GroundHit { get; private set; }
    public RaycastHit2D WallHit { get; private set; }

    private RaycastHit2D[] groundHits;
    private RaycastHit2D[] ceilingHits;
    private List<RaycastHit2D> wallHits = new List<RaycastHit2D>();
    private Transform Transform;

    private void Awake()
    {
        Transform = transform;

        if (boxCollider == null)
        {
            Debug.LogError($"[{name}] Box collider is null, {name} disabled");
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        PerformCollisionChecks();
        GatherCollisionsValues();
    }

    private void PerformCollisionChecks()
    {
        if (checkGround)
            groundHits = GetBoxCastHits(groundCheckDistance, Vector2.down);

        if (checkCeiling)
            ceilingHits = GetBoxCastHits(ceilingCheckDistance, Vector2.up);

        if (checkWalls)
        {
            wallHits.Clear();
            wallHits.AddRange(GetBoxCastHits(wallCheckDistance, Vector2.left));
            wallHits.AddRange(GetBoxCastHits(wallCheckDistance, Vector2.right));
        }
    }

    private void GatherCollisionsValues()
    {
        GroundHit = GetFirstValidHit(groundHits);
        CeilingHit = GetFirstValidHit(ceilingHits);
        WallHit = GetFirstValidHit(wallHits.ToArray());
    }

    private RaycastHit2D[] GetBoxCastHits(float distance, Vector2 direction) => Physics2D.BoxCastAll(boxCollider.bounds.center,
            boxCollider.size,
            Transform.eulerAngles.z,
            direction,
            distance,
            contactLayerMask);

    private RaycastHit2D GetFirstValidHit(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null && !Physics2D.GetIgnoreCollision(boxCollider, hit.collider))
                return hit;
        }

        return default; // returns an empty RaycastHit2D
    }
}
