using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region 玩家阵营
    [Header("玩家")]
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
                // TODO 初始化小兵
            }
        }
    }
    #endregion

    #region 野怪
    [Header("野怪")]
    public List<int> monstersSummonCDs;
    readonly List<Transform> monsterSpawns;
    // 每个 List[0] 为头目, 其余为小怪
    readonly List<List<GameObject> > monsters;
    private void MonsterUpdate(int idx)
    {
        foreach (GameObject monster in monsterSpawns[idx])
        {
            GameObject now = Instantiate(monster, monsterSpawns[idx].position, Quaternion.identity);
            // TODO 初始化野怪
        }
    }
    #endregion

    private void Start()
    {
        // 开启线程
    }
}
