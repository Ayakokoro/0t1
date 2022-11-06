using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class TimedPotionedBuff : TimedBuff
{
    private PotionedBuff potionedBuff;
    public TimedPotionedBuff(float duration, ScriptableBuff buff, GameObject obj) : base(duration, buff, obj)
    {
        
        potionedBuff = (PotionedBuff)buff;
    }
    public override void Tick(float detTime)
    {

        base.Tick(detTime);
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }
    public override void End()
    {
        throw new System.NotImplementedException();
    }
}
