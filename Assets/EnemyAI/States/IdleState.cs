using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ֹ״̬
/// </summary>
public class IdleState : Istate
{
    private FSM manager;
    private Parameter parameter;
    private Vector3 GoBackDirection;
    private Node currentTarget;
    private Vector3 current;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.target = parameter.campPosition;
        //��Ӫ������תΪĿ��
        manager.AtarToPath();
    }

    public void OnUpdate()
    {
        KeepIdleAtHome();
        if (parameter.currentVigilValue > 0f)
        {
            manager.TranslateState(StateType.Chase);
        }
    }

    public void OnExit()
    {
    }

    /// <summary>
    /// ����Idle״̬ʱ�ڼ�
    /// </summary>
    private void KeepIdleAtHome()
    {
        Vector3 Direction;
        Direction = parameter.campPosition.position - manager.transform.position;
        if (Direction.magnitude > 3f)
        {
            //��A���㷨�ҵ�·��
            if (manager.parameter.path.Count != 0)
            {
                currentTarget = manager.parameter.path.First.Value;
                current = MapManager.instance.UseGridCalcPosWolrdPos(currentTarget.gridPosition);
                GoBackDirection = (current - manager.transform.position).normalized;
                if (Vector3.Distance(manager.transform.position, current) < 0.1f)
                {
                    if (manager.parameter.path.Count >= 2)
                    {
                        manager.parameter.path.RemoveFirst();
                        currentTarget = manager.parameter.path.First.Value;
                    }
                }
                parameter.animator.SetFloat("InputX", GoBackDirection.x);
                parameter.animator.SetFloat("InputY", GoBackDirection.y);
            }
            if (Vector3.Distance(manager.transform.position, current) < 0.1f)
            {
                if (manager.parameter.path.Count != 0)
                {
                    currentTarget = manager.parameter.path.First.Value;
                    manager.parameter.path.RemoveFirst();
                }
            }
            manager.transform.Translate(parameter.chaseSpeed * Time.deltaTime * GoBackDirection);
        }
        else
        {
            if (Direction.magnitude < 0.1f)
            {
                parameter.animator.SetBool("IsChase", false);
                //���úù���ĳ���
                parameter.animator.SetFloat("InputX", 0);
                parameter.animator.SetFloat("InputY", 0);
            }
            else
            {
                manager.transform.Translate(parameter.chaseSpeed * Time.deltaTime * Direction);

            }
        }
    }
}
