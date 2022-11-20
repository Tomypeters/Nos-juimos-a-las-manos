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
        this.stateId = "Attack";
        this.transitions = new List<Transition>();
    }


    public override void Enter(BaseStateMachine machine)
    {
        timer = cooldownTime;
    }

    public override void Execute(BaseStateMachine machine)
    {
        if (timer < 0)
            machine.ChangeState(new RecoveryState(entity));
        else
            timer -= Time.deltaTime;
    }

    public override void Exit(BaseStateMachine machine)
    {
        entity.Attack();
    }
}
