using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态枚举
public enum StateType
{
    Idle,
    Chase,
    Hit
}

//属性列表
[Serializable]
public class Parameter
{
    public float chaseSpeed;//追逐速度
    public float chaseRadius;//追踪半径
    public Transform target;//追逐目标
    public Transform campPosition;//营地中心
    public float VigilanceValue;//警觉值
    public float currentVigilValue;//当前的警觉值
    public CircleCollider2D camp;//营地的碰撞器
    public GridNodes nodes; //地图信息
    public Astar astar;//A星算法脚本
    public Animator animator;//动画器
    public LinkedList<Node> path;//路径点
    public Vector2Int lastTarget;//上一个目标是否改变

}


//状态机
public class FSM : MonoBehaviour
{
    //当前状态
    public Istate currentState;
    private Dictionary<StateType, Istate> states = new Dictionary<StateType, Istate>();

    public Parameter parameter;

    private void Awake()
    {
        //初始化状态字典
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Hit, new HitState(this));

        parameter.animator = GetComponent<Animator>();
        parameter.astar = GetComponent<Astar>();
        parameter.path = new LinkedList<Node>();
        //记录好最初的位置
        parameter.camp = this.transform.parent.GetComponentInChildren<CircleCollider2D>();
        parameter.camp.radius = parameter.chaseRadius;
        parameter.campPosition = parameter.camp.transform;
        parameter.target = parameter.campPosition;
        parameter.lastTarget = new Vector2Int(0, 0);
        TranslateState(StateType.Idle);
    }

    private void Update()
    {
        print(currentState);
        currentState.OnUpdate();
        if (parameter.currentVigilValue > 0)
        {
            parameter.currentVigilValue -= 0.5f * Time.deltaTime;//速度可以更改，之后可以设参
        }
    }

    public void TranslateState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    //由A星得到的数组路径上的所有点
    public void AtarToPath()
    {
        Vector2Int start = MapManager.instance.UseWolrdPosCalcGridPos(this.transform.position);
        Vector2Int target = MapManager.instance.UseWolrdPosCalcGridPos(parameter.target.position);
        if (target != parameter.lastTarget)
        {
            //更新行走路线
            parameter.path.Clear();
            parameter.lastTarget = target;
            parameter.nodes = parameter.astar.BiuldPath(start, target);
            if (parameter.nodes != null)
            {
                Node node = parameter.nodes.GetGridNode(target.x, target.y);
                while(node.parentNode != null && node.parentNode.gridPosition != start)
                {
                    print(node.gridPosition.x +"," + node.gridPosition.y);
                    parameter.path.AddFirst(node);
                    node = node.parentNode;
                }
                parameter.path.AddFirst(node);
            }
        }
    }
}

