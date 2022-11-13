using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : Istate
{
    private FSM manager;
    private Parameter parameter;

    public HitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

   
}
