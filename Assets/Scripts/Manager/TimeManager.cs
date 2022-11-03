using System.Collections;
using System.Collections.Generic;
using MyTimer;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    /// <summary>
    /// test
    /// </summary>
    //Metronome timer = new Metronome();


    #region �����Ӫ
    [Header("���")]
    public List<Transform> playerSpawns;
    public List<GameObject> players;
    private void PlayerUpdate()
    {
        foreach (Transform spawn in playerSpawns)
        {
            foreach (GameObject player in players)
            {
                GameObject now = Instantiate(player, spawn.position, Quaternion.identity);
                // TODO ��ʼ�����
            }
        }
    }
    [Header("С��")]
    public int minionsSummonCD;
    public List<Transform> minionsSpawns;
    public List<GameObject> minions;
    private void MinionsUpdate(float p)
    {
        foreach (Transform spawn in minionsSpawns)
        {
            foreach (GameObject minion in minions)
            {
                GameObject now = Instantiate(minion, spawn.position, Quaternion.identity);
                // TODO ��ʼ��С��
            }
        }
    }
    [Header("������")]
    public List<Transform> turretSpawns;
    public List<GameObject> turrets;
    private void TurretInit()
    {
        foreach (Transform spawn in turretSpawns)
        {
            foreach (GameObject turret in turrets)
            {
                GameObject now = Instantiate(turret, spawn.position, Quaternion.identity);
                // TODO ��ʼ��������
            }
        }
    }
    #endregion

    #region Ұ��
    [Header("Ұ��")]
    public List<int> monstersSummonCD;
    public List<Transform> monsterSpawns;
    // ÿ�� List[0] ΪͷĿ, ����ΪС��
    public List<List<GameObject> > monsters;
    private void MonsterUpdate(int idx)
    {
        foreach (GameObject monster in monsterSpawns[idx])
        {
            GameObject now = Instantiate(monster, monsterSpawns[idx].position, Quaternion.identity);
            // TODO ��ʼ��Ұ��
        }
    }
    #endregion

    private void Start()
    {
        // ��ʼ��
        TurretInit();
        // �����߳�
        //timer.OnComplete += MinionsUpdate;
        //timer.Initialize(1);

        //timer.Paused = true;
    }
}
