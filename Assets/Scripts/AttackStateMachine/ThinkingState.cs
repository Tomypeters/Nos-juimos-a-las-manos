using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingState : BaseState
{

    private float thinkTimer = 0.3f;

    public ThinkingState(Entity entity) : base(entity)
    {
    }

    public override void Enter(BaseStateMachine machine)
    {
        thinkTimer = 0.3f;
    }

    public override void Execute(BaseStateMachine machine)
    {
        thinkTimer -= Time.deltaTime;
        if(thinkTimer < 0)
        {
            float rand = Random.Range(1, 3);
            if (rand == 1)
                machine.ChangeState(machine.attackState);
            else if (rand == 2)
                machine.ChangeState(machine.heavyAttackState);
            else if (rand == 3)
                machine.ChangeState(machine.blockState);
        }


    }

    public override void Exit(BaseStateMachine machine)
    {

    }

}
