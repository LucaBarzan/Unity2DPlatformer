using UnityEngine;

public class IdleState : State
{
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private bool useTimer = false;
    [SerializeField] private float idleTime = 0.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        movementDirectionProvider.Set(Vector2.zero);
        
        if (useTimer)
            Invoke(nameof(SetStateComplete), idleTime);
    }

    private void Update()
    {
        if (!useTimer || movementDirectionProvider.MoveDirection != Vector2.zero)
            SetStateComplete();
    }
}