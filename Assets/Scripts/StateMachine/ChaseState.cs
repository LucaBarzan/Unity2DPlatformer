using UnityEngine;

public class ChaseState : State
{
    public Transform TargetTransform;
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private CharacterMovement characterMovement;

    protected override void OnEnable()
    {
        base.OnEnable();
        characterMovement.enabled = true;
    }

    private void Update()
    {
        if (TargetTransform == null)
        {
            SetStateComplete();
            return;
        }

        movementDirectionProvider.Set((TargetTransform.position - characterMovement.Position).normalized);
    }

    private void OnDisable()
    {

    }
}
