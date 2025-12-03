using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] InputActionReference input_Jump;
    [SerializeField] InputActionReference input_Slide;
    [SerializeField] CharacterMovement characterMovement;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        SubscribeToInputEvents();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnDisable()
    {
        UnsubscribeFromInputEvents();
    }

    private void SubscribeToInputEvents()
    {
        input_Jump.action.performed += OnJumpInput_performed;
        input_Slide.action.performed += OnSlideInput_performed;
    }
    private void UnsubscribeFromInputEvents()
    {
        input_Jump.action.performed -= OnJumpInput_performed;
        input_Slide.action.performed -= OnSlideInput_performed;
    }

    private void OnSlideInput_performed(InputAction.CallbackContext obj)
    {

    }

    private void OnJumpInput_performed(InputAction.CallbackContext obj)
    {
        // characterMovement.
    }

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