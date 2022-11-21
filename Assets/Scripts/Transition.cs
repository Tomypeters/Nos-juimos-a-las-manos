using System;
using UnityEngine;

public class Transition : ScriptableObject
{
    //private Func<float, bool> condition;
    private BaseState nextState;

    //public Transition(Func<float, bool> condition, BaseState nextState)
    public Transition(BaseState nextState)
    {
        //this.condition = condition;
        this.nextState = nextState;
    }

}
