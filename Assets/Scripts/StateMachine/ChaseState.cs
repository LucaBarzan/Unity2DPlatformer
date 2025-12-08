using UnityEngine;

public class ChaseState : MoveToTargetState
{
    [SerializeField] private GameObject attackRangeSensor;

    protected override void OnEnable()
    {
        base.OnEnable();
        attackRangeSensor.SetActive(true);
    }

    private void OnDisable()
    {
        attackRangeSensor.SetActive(false);
    }
}
