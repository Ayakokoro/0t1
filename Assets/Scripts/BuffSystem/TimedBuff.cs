using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedBuff : MonoBehaviour
{
    protected float duration;
    protected ScriptableBuff buff;
    protected GameObject obj;

    public abstract void Activate();
    public abstract void End();

    public TimedBuff(float duration, ScriptableBuff buff, GameObject obj)
    {
        this.duration = duration;
        this.buff = buff;
        this.obj = obj;
    }

    public virtual void Tick (float detTime)
    {
        duration -= detTime;
        if (duration <=0)
        {
            End();
        }
    }
    
    public bool IsFinished
    {
        get { return duration <= 0 ? true : false; }
    }
}
