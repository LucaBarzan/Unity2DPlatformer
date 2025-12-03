using System;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public event Action OnCompleted;
    public virtual bool IsAvailable => true;

    private float startTime;

    public float TimePassed => Time.time - startTime;

    protected virtual void Awake() => enabled = false;

    protected virtual void OnEnable() => startTime = Time.time;

    protected void SetStateComplete()
    {
        OnCompleted?.Invoke();
        enabled = false;
    }
}