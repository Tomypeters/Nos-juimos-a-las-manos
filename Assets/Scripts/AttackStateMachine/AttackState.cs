using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    private float timer;
    private float cooldownTime = 1;

    public AttackState(Entity entity, float cooldownTime) : base(entity)
    {
        this.cooldownTime = cooldownTime;
    }


    public override void Enter(BaseStateMachine machine)
    {
        timer = cooldownTime;
        entity.Attack();
    }

    public override void Execute(BaseStateMachine machine)
    {
        if (timer < 0)
        {
            machine.ChangeState(machine.recoveryState);
        }
        else
            timer -= Time.deltaTime;
    }

    public override void Exit(BaseStateMachine machine)
    {
        entity.FinishAttack();
    }
}
