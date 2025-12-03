using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    [SerializeField] InputActionReference input_Movement;
    [SerializeField] MovementDirectionProvider movementDirectionProvider;

    void Update()
    {
        if (input_Movement.action.enabled)
            movementDirectionProvider.Set(input_Movement.action.ReadValue<Vector2>());
    }
}