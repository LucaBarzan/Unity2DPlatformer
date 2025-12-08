using UnityEngine;

public static class Helpers
{
    public static bool IsCloseTo(this Vector3 positionA, Vector3 positionB, float threshold = 0.1f)
    {
        float distanceSqr = (positionA - positionB).sqrMagnitude;
        return distanceSqr <= (threshold * threshold);
    }

    public static bool IsCloseTo(this Vector2 positionA, Vector2 positionB, float threshold = 0.1f) => IsCloseTo(positionA, positionB, threshold);

    /// <summary>
    /// Finds the closest point on a line to a given point.
    /// </summary>
    /// <param name="lineStart">The start point of the line.</param>
    /// <param name="lineEnd">The end point of the line.</param>
    /// <param name="point">The point to find the closest point on the line from.</param>
    /// <returns>The closest point on the line to the given point.</returns>
    public static Vector3 ClosestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 line = lineEnd - lineStart;
        float lineLengthSq = line.sqrMagnitude; // Squared length of the line

        // Handle degenerate line
        if (lineLengthSq <= Mathf.Epsilon)
        {
            return lineStart;
        }

        // Calculate the projection of the vector from lineStart to point onto the line
        float t = Vector3.Dot(point - lineStart, line) / lineLengthSq;

        // Clamp 't' to be between 0 and 1, ensuring the point lies within the line
        t = Mathf.Clamp01(t);

        // Calculate the closest point on the line
        return lineStart + t * line;
    }

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