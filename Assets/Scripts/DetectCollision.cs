using UnityEngine;
using UnityEngine.Events;

public enum ECollisionCheck : int
{
    None = 0,
    Layer = 1,
    Tag = 2,
    LayerAndTag = 3,
    LayerOrTag = 4
}

[RequireComponent(typeof(Collider2D))]
public class DetectCollision : MonoBehaviour
{
    #region Variables

    public UnityEvent<Collider2D> OnCollisionEnter;
    public UnityEvent<Collider2D> OnCollisionStay;
    public UnityEvent<Collider2D> OnCollisionExit;

    // Inspector
    public ECollisionCheck check = ECollisionCheck.LayerOrTag;
    public LayerMask LayerMaskCollissions;
    public string[] Tags;
    public string[] IgnoreTags;

    public Collider2D Collider2D { get; private set; }

    #endregion // Variables

    #region Engine

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();

        if (Collider2D == null)
        {
            Debug.LogError($"[{nameof(DetectCollision)}] No Collider2D found on {name}. Component removed.");
            Destroy(this);
        }
    }

    // COLLISION
    private void OnCollisionEnter2D(Collision2D other) => HandleEnter(other.collider);
    private void OnCollisionStay2D(Collision2D other) => HandleStay(other.collider);
    private void OnCollisionExit2D(Collision2D other) => HandleExit(other.collider);

    // TRIGGER
    private void OnTriggerEnter2D(Collider2D other) => HandleEnter(other);
    private void OnTriggerStay2D(Collider2D other) => HandleStay(other);
    private void OnTriggerExit2D(Collider2D other) => HandleExit(other);

    // PARTICLES
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Collider2D collider))
            HandleEnter(collider);
    }

    #endregion // Engine

    #region Handlers

    private void HandleEnter(Collider2D other)
    {
        if (enabled && PassesFilter(in other))
            OnCollisionEnter?.Invoke(other);
    }

    private void HandleStay(Collider2D other)
    {
        if (enabled && PassesFilter(in other))
            OnCollisionStay?.Invoke(other);
    }

    private void HandleExit(Collider2D other)
    {
        if (enabled && PassesFilter(other))
            OnCollisionExit?.Invoke(other);
    }

    #endregion // Handlers

    #region Utils

    private bool PassesFilter(in Collider2D other)
    {
        if (check == ECollisionCheck.None)
            return true;

        bool layerMatch = false;
        bool tagMatch = false;

        // Layer filtering
        if (LayerMaskCollissions.value != 0)
            layerMatch = other.IsInLayerMask(LayerMaskCollissions);
        else
            layerMatch = true; // no layer constraint

        // Tag filtering
        tagMatch = MatchesAnyTag(other, Tags);

        // Ignore tag override
        if (MatchesAnyTag(other, IgnoreTags))
            tagMatch = false;

        return check switch
        {
            ECollisionCheck.Layer => layerMatch,
            ECollisionCheck.Tag => tagMatch,
            ECollisionCheck.LayerAndTag => layerMatch && tagMatch,
            ECollisionCheck.LayerOrTag or _ => layerMatch || tagMatch,
        };
    }

    private bool MatchesAnyTag(Collider2D other, string[] tagList)
    {
        if (tagList == null || tagList.Length == 0)
            return true;

        string otherTag = other.tag;

        for (int i = 0; i < tagList.Length; i++)
        {
            var tag = tagList[i];
            if (!string.IsNullOrEmpty(tag) && otherTag == tag)
                return true;
        }

        return false;
    }


    #endregion // Utils
}
