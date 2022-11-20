using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : ScriptableObject
{
    public Entity entity;
    public List<Transition> transitions;
    public string stateId;

    public BaseState(Entity entity)
    {
        this.entity = entity;
    }

    public void AddTransition(Transition t)
    {
        // Add new transition to transitions list
        transitions.Add(t);
    }

    public abstract void Enter(BaseStateMachine machine);
    public abstract void Execute(BaseStateMachine machine);
    public abstract void Exit(BaseStateMachine machine);

}