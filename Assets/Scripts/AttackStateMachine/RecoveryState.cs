using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : BaseState
{

    public float timer = 0;
    public float recoveryTime = 0.3f;

    public RecoveryState(Entity entity) : base(entity)
    {

    }

    public override void Enter(BaseStateMachine machine)
    {
        timer = recoveryTime;
    }

    public override void Execute(BaseStateMachine machine)
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            machine.ChangeState(machine.idleState);
        }
    }

    public override void Exit(BaseStateMachine machine)
    {

    }
}
