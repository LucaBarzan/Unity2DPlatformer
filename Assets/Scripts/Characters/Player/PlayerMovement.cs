using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private JumpState jumpState;
    [SerializeField] private InputActionReference input_Jump;
    [SerializeField] private InputActionReference input_Slide;

    protected override void Awake()
    {
        base.Awake();
        StateMachine.Add(jumpState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        jumpState.OnCompleted += JumpState_OnCompleted;
        SubscribeToInputEvents();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
        StateMachine.Set(jumpState);
    }

    private void JumpState_OnCompleted() => SelectState();

    protected override void SelectState()
    {
        if (!jumpState.enabled)
            base.SelectState();
    }
}
