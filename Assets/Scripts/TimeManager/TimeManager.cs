using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region �����Ӫ
    [Header("���")]
    public int minionsSummonCD;
    readonly List<Transform> playerSpawns;
    readonly List<GameObject> minions;
    private void PlayerUpdate()
    {
        foreach (Transform spawn in playerSpawns)
        {
            foreach(GameObject minion in minions)
            {
                GameObject now = Instantiate(minion, spawn.position, Quaternion.identity);
                // TODO ��ʼ��С��
            }
        }
    }
    #endregion

    #region Ұ��
    [Header("Ұ��")]
    public List<int> monstersSummonCDs;
    readonly List<Transform> monsterSpawns;
    // ÿ�� List[0] ΪͷĿ, ����ΪС��
    readonly List<List<GameObject> > monsters;
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
        // �����߳�
    }
}
