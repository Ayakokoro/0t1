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


    #region 玩家阵营
    [Header("玩家")]
    public List<Transform> playerSpawns;
    public List<GameObject> players;
    private void PlayerUpdate()
    {
        foreach (Transform spawn in playerSpawns)
        {
            foreach (GameObject player in players)
            {
                GameObject now = Instantiate(player, spawn.position, Quaternion.identity);
                // TODO 初始化玩家
            }
        }
    }
    [Header("小兵")]
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
                // TODO 初始化小兵
            }
        }
    }
    [Header("防御塔")]
    public List<Transform> turretSpawns;
    public List<GameObject> turrets;
    private void TurretInit()
    {
        foreach (Transform spawn in turretSpawns)
        {
            foreach (GameObject turret in turrets)
            {
                GameObject now = Instantiate(turret, spawn.position, Quaternion.identity);
                // TODO 初始化防御塔
            }
        }
    }
    #endregion

    #region 野怪
    [Header("野怪")]
    public List<int> monstersSummonCD;
    public List<Transform> monsterSpawns;
    // 每个 List[0] 为头目, 其余为小怪
    public List<List<GameObject> > monsters;
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
        // 初始化
        TurretInit();
        // 开启线程
        //timer.OnComplete += MinionsUpdate;
        //timer.Initialize(1);

        //timer.Paused = true;
    }
}
