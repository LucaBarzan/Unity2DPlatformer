using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Vector3 Position => Transform.position;

    [SerializeField] private SurfaceContactSensor contactSensor;
    [SerializeField] private GroundedState groundedState;
    [SerializeField] private AirborneState airborneState;

    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        Transform = transform;

        stateMachine.Add(groundedState).Add(airborneState);
    }

    private void OnEnable()
    {

    }

    private void Start()
    {
        stateMachine.Set(airborneState);

    }

    private void Update()
    {
        SelectState();
    }

    private void FixedUpdate()
    {

    }

    private void OnDisable()
    {
        stateMachine.DisableAllStates();
    }

    private void SelectState()
    {
        if (contactSensor.GroundHit)
        {
            stateMachine.Set(groundedState);
        }
        else
        {
            stateMachine.Set(airborneState);
        }
    }
}
