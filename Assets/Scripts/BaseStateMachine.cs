using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : ScriptableObject
{
    public IdleState idleState;
    public HeavyAttackState heavyAttackState;
    public AttackState attackState;
    public KnockedState knockedState;
    public ThinkingState thinkingState;
    public BlockState blockState;
    public RecoveryState recoveryState;

    public Entity owner;

    public BaseState CurrentState { get; set; }


    public void Init(Entity owner)
    {
        this.owner = owner;

        idleState = new IdleState(owner);
        heavyAttackState = new HeavyAttackState(owner, owner.attackCooldown * 2);
        attackState = new AttackState(owner, owner.attackCooldown);
        knockedState = new KnockedState(owner, owner.attackCooldown);
        thinkingState = new ThinkingState(owner);
        blockState = new BlockState(owner, owner.attackCooldown * 2);
        recoveryState = new RecoveryState(owner);

        CurrentState = idleState;
        CurrentState.Enter(this);
    }

    public void Update()
    {
        if (CurrentState != null)
            CurrentState.Execute(this);
    }

    public void ChangeState(BaseState newState)
    {
        CurrentState.Exit(this);
        CurrentState = newState;
        CurrentState.Enter(this);
    }

}
