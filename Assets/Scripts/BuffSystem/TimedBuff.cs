using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public abstract class TimedBuff
{
    protected float duration;
    protected ScriptableBuff buff;
    protected GameObject obj;
    protected BuffType buffType;
    public BuffType BuffType
    {
        get { return buffType; }
    }

    public Sprite icon;
    public abstract void Activate(BoolController boolController);
    public abstract void End();
    public abstract void Refresh();

    public TimedBuff(float duration, ScriptableBuff buff, GameObject obj)
    {
        this.duration = duration;
        this.buff = buff;
        this.obj = obj;
        this.buffType = buff.buffType;
        this.icon = buff.Icon;
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
