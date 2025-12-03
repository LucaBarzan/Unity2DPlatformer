using UnityEngine;

public static class Helpers
{
    public static bool IsCloseTo(this Vector3 positionA, Vector3 positionB, float threshold = 0.1f)
    {
        float distanceSqr = (positionA - positionB).sqrMagnitude;
        return distanceSqr <= (threshold * threshold);
    }

    public static bool IsCloseTo(this Vector2 positionA, Vector2 positionB, float threshold = 0.1f) => IsCloseTo(positionA, positionB, threshold);

    #region Layermask

    /// <summary> Checks whether a specified layer is included in the given LayerMask. </summary>
    public static bool ContainsLayer(this LayerMask layerMask, int layer)
    {
        return layer.IsInLayerMask(layerMask);
    }

    /// <summary> Checks whether the layer of the GameObject associated with the Collider2D is included in a specified LayerMask. </summary>
    public static bool IsInLayerMask(this Collider2D collider2D, LayerMask layerMask)
    {
        return collider2D.gameObject.layer.IsInLayerMask(layerMask);
    }

    /// <summary> Checks whether a given layer is included in a specified LayerMask </summary>
    /// <returns> Returns a boolean indicating whether the given layer is part of the specified LayerMask</returns>
    public static bool IsInLayerMask(this int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    public static int LayerMaskToLayer(this LayerMask layermask)
    {
        return Mathf.RoundToInt(Mathf.Log(layermask.value, 2));
    }

    #endregion Layermask

}