using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackState : BaseState
{

    private float timer;
    private float cooldownTime = 2;

    public HeavyAttackState(Entity entity, float cooldownTime) : base(entity)
    {
        this.cooldownTime = cooldownTime;
    }


    public override void Enter(BaseStateMachine machine)
    {
        timer = cooldownTime;
        entity.HeavyAttack();
    }

    public override void Execute(BaseStateMachine machine)
    {
        if (timer < 0) {
            machine.ChangeState(machine.recoveryState);
        }
        else
            timer -= Time.deltaTime;
    }

    public override void Exit(BaseStateMachine machine)
    {
        entity.FinishHeavyAttack();
    }
}
