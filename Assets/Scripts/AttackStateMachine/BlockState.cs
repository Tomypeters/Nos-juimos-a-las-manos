using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : BaseState
{
    private float cooldownTime = 2;

    public BlockState(Entity entity, float cooldownTime) : base(entity)
    {
        this.cooldownTime = cooldownTime;
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
