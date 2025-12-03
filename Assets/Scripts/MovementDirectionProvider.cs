using UnityEngine;

public class MovementDirectionProvider : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }

    public void Set(Vector2 direction) => MoveDirection = direction.normalized;
}