using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedState : BaseState
{

    public float timer = 0;
    public float knockedTime = 0.5f;

    public KnockedState(Entity entity, float knockedTime) : base(entity)
    {
        this.knockedTime = knockedTime;
    }

    public override void Enter(BaseStateMachine machine)
    {
    }

    public override void Execute(BaseStateMachine machine)
    {
    }

    public override void Exit(BaseStateMachine machine)
    {

    }
}
