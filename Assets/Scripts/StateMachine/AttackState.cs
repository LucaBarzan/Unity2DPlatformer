using UnityEngine;

public class AttackState : State
{
    [SerializeField] private MovementDirectionProvider movementDirectionProvider;
    [SerializeField] private GameObject attackObject;
    [SerializeField] private float antecipationTime = 1.0f;
    [SerializeField] private float attackTime = 1.0f;

    private float attackTimer;
    private float antecipationTimer;
    private AttackSubState subState;
    private enum AttackSubState
    {
        antecipation,
        attacked
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        movementDirectionProvider.Set(Vector2.zero);
        antecipationTimer = Time.time + antecipationTime;
        subState = AttackSubState.antecipation;
    }

    private void Update()
    {
        switch (subState)
        {
            case AttackSubState.antecipation:
                if (Time.time > antecipationTimer)
                {
                    attackTimer = Time.time + attackTime;
                    attackObject.SetActive(true);
                    subState = AttackSubState.attacked;
                }
                break;

            case AttackSubState.attacked:
                if (Time.time > attackTimer)
                {
                    SetStateComplete();
                }
                break;
        }
    }

    private void OnDisable()
    {
        attackObject.SetActive(false);
    }
}