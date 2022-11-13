using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//״̬ö��
public enum StateType
{
    Idle,
    Chase,
    Hit
}

//�����б�
[Serializable]
public class Parameter
{
    public float chaseSpeed;//׷���ٶ�
    public float chaseRadius;//׷�ٰ뾶
    public Transform target;//׷��Ŀ��
    public Transform campPosition;//Ӫ������
    public float VigilanceValue;//����ֵ
    public float currentVigilValue;//��ǰ�ľ���ֵ
    public CircleCollider2D camp;//Ӫ�ص���ײ��
    public GridNodes nodes; //��ͼ��Ϣ
    public Astar astar;//A���㷨�ű�
    public Animator animator;//������
    public LinkedList<Node> path;//·����
    public Vector2Int lastTarget;//��һ��Ŀ���Ƿ�ı�

}


//״̬��
public class FSM : MonoBehaviour
{
    //��ǰ״̬
    public Istate currentState;
    private Dictionary<StateType, Istate> states = new Dictionary<StateType, Istate>();

    public Parameter parameter;

    private void Awake()
    {
        //��ʼ��״̬�ֵ�
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Hit, new HitState(this));

        parameter.animator = GetComponent<Animator>();
        parameter.astar = GetComponent<Astar>();
        parameter.path = new LinkedList<Node>();
        //��¼�������λ��
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
            parameter.currentVigilValue -= 0.5f * Time.deltaTime;//�ٶȿ��Ը��ģ�֮��������
        }
    }

    public void TranslateState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    //��A�ǵõ�������·���ϵ����е�
    public void AtarToPath()
    {
        Vector2Int start = MapManager.instance.UseWolrdPosCalcGridPos(this.transform.position);
        Vector2Int target = MapManager.instance.UseWolrdPosCalcGridPos(parameter.target.position);
        if (target != parameter.lastTarget)
        {
            //��������·��
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

