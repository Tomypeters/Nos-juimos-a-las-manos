using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Entity entity) : base(entity)
    {
        this.stateId = "Idle";
        this.transitions = new List<Transition>();
        this.AddTransition(new Transition(true, new AttackState(entity, 0.1f)));
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
