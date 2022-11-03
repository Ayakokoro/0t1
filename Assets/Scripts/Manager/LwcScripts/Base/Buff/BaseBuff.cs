using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Buff 种类
/// </summary>
public enum Buff { }

public class BaseBuff : MonoBehaviour
{
    protected GameObject target;
    protected int lastTime;
    protected int perTime;
    protected bool isPermanent;
    /// <summary>
    /// 开启Buff
    /// </summary>
    /// <param name="_target">Buff目标</param>
    /// <param name="_isPermanent">是否永久持续</param>
    /// <param name="_lastTime">Buff持续时间</param>
    /// <param name="_perTime">Buff发动间隔</param>
    public void StartBuff(GameObject _target, bool _isPermanent, int _lastTime, int _perTime)
    {
        target = _target;
        isPermanent = _isPermanent;
        lastTime = _lastTime;
        perTime = _perTime;
        StartCoroutine(DoBuff());
        StartCoroutine(EndBuff());
    }
    /// <summary>
    /// 开启Buff
    /// </summary>
    /// <param name="_target">Buff目标</param>
    /// <param name="_isPermanent">是否永久持续</param>
    public void StartBuff(GameObject _target, bool _isPermanent)
    {
        target = _target;
        isPermanent = _isPermanent;
        StartCoroutine(DoBuff());
    }
    protected virtual IEnumerator DoBuff() { yield break; }
    private IEnumerator EndBuff()
    {
        yield return new WaitForSeconds(lastTime);
        StopCoroutine(DoBuff());
    }
}
