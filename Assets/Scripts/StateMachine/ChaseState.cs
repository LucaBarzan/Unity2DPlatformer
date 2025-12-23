using UnityEngine;

public class ChaseState : MoveToTargetState
{
    [SerializeField] private GameObject attackRangeSensor;

    protected override void OnEnable()
    {
        base.OnEnable();
        attackRangeSensor.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        attackRangeSensor.SetActive(false);
    }
}
