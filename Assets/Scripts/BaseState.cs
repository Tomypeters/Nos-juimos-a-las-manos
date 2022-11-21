using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public Entity entity;

    public BaseState(Entity entity)
    {
        this.entity = entity;
    }

    public abstract void Enter(BaseStateMachine machine);
    public abstract void Execute(BaseStateMachine machine);
    public abstract void Exit(BaseStateMachine machine);

}