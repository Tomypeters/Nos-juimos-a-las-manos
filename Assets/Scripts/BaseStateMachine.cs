using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _initialState;

    public void Awake()
    {
        CurrentState = _initialState;
    }

    public BaseState CurrentState { get; set; }

    public void Update()
    {
        CurrentState.Execute(this);
    }

    public void ChangeState(BaseState newState)
    {
        CurrentState.Exit(this);
        CurrentState = newState;
        CurrentState.Enter(this);
    }

    public void AttemptTransition(string nextStateId)
    {
        foreach (Transition t in CurrentState.transitions)
        {
            bool transitioned = t.AttemptTransition(this, nextStateId);
            if (transitioned)
                return;
        }
    }
}
