using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    public event Action OnCompleted;
    public virtual bool IsAvailable => true;
    public float TimePassed => Time.time - startTime;

    protected float startTime;

    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    protected virtual void OnEnable() => startTime = Time.time;

    protected void SetStateComplete()
    {
        enabled = false;
        OnCompleted?.Invoke();
    }
}