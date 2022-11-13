using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ×·Öð×´Ì¬
/// </summary>
public class ChaseState : Istate
{
    private FSM manager;
    private Parameter parameter;

    private Vector3 chaseDirection;
    private float StopX;
    private float StopY;
    private Node currentTarget;
    private Vector3 current;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.SetBool("IsChase", true);
    }

    public void OnUpdate()
    {
        if(parameter.currentVigilValue <= 0f)
        {
            Debug.Log(1);
            manager.TranslateState(StateType.Idle);
        }
        //°´AÐÇËã·¨ÕÒµ½Â·¾¶
        manager.AtarToPath();
        if (manager.parameter.path.Count != 0)
        {
            currentTarget = manager.parameter.path.First.Value;
            current = MapManager.instance.UseGridCalcPosWolrdPos(currentTarget.gridPosition);
            chaseDirection = (current - manager.transform.position).normalized;
            if(Vector3.Distance(manager.transform.position, current) < 0.1f)
            {
                if(manager.parameter.path.Count >= 2)
                {
                    manager.parameter.path.RemoveFirst();
                    currentTarget = manager.parameter.path.First.Value;
                }
            }
            StopX = chaseDirection.x;
            StopY = chaseDirection.y;
        }
        else
        {
            chaseDirection = Vector3.zero;
            StopX = 0;
            StopY = 0;
        }
        manager.transform.Translate(chaseDirection * parameter.chaseSpeed * Time.deltaTime);
        parameter.animator.SetFloat("InputX", StopX);
        parameter.animator.SetFloat("InputY", StopY);
    }
    public void OnExit()
    {
    }

   
}

