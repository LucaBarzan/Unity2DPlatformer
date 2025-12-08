using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{

    private void SetControllable(bool controllable)
    {
        var inputManager = InputManager.Instance;

        if (inputManager == null)
            return;

        if (controllable && !inputManager.PlayerMap.enabled)
        {
            inputManager.PlayerMap.Enable();
        }
        else if (!controllable && inputManager.PlayerMap.enabled)
        {
            inputManager.PlayerMap.Disable();
        }
    }
}