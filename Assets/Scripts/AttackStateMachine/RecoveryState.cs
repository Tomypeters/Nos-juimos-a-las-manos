using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : BaseState
{

    public float timer = 0;
    public float recoveryTime = 0.5f;

    public RecoveryState(Entity entity) : base(entity)
    {
        this.stateId = "Recovery";
        this.transitions = new List<Transition>();

    }

    public override void Enter(BaseStateMachine machine)
    {
        entity.FinishAttack();
        timer = recoveryTime;
    }

    public override void Execute(BaseStateMachine machine)
    {
        timer -= Time.deltaTime;

        if (timer < 0)
            machine.ChangeState(new IdleState(entity));
    }

    public override void Exit(BaseStateMachine machine)
    {

    }
}
