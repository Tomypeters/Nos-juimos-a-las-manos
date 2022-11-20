using UnityEngine;

public class Transition : ScriptableObject
{
    private bool condition;
    private BaseState nextState;

    public Transition(bool condition, BaseState nextState)
    {
        this.condition = condition;
        this.nextState = nextState;
    }

    public bool AttemptTransition(BaseStateMachine stateMachine, string nextStateId)
    {
        if (condition && nextState.stateId == nextStateId)
        {
            stateMachine.ChangeState(nextState);
            return true;
        }

        return false;
    }
}
