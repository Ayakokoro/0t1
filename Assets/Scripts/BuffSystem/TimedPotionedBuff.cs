using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class TimedPotionedBuff : TimedBuff
{
    private BoolController bindBool;
    private PotionedBuff potionedBuff;
    private PlayerController playerController;
    private float Timer = 0;
    public TimedPotionedBuff(float duration, ScriptableBuff buff, GameObject obj) : base(duration, buff, obj)
    {
        playerController = obj.GetComponent<PlayerController>();
        potionedBuff = (PotionedBuff)buff;
    }
    public override void Tick(float detTime)
    {
        Timer -= detTime;
        bindBool.Tick(detTime);
        if (Timer <=0)
        {
            Timer = potionedBuff.delay;
            playerController.hp -= potionedBuff.damage;
        }
        base.Tick(detTime);
    }

    public override void Activate(BoolController UIController)
    {
        Timer = potionedBuff.delay;
        playerController.hp -= potionedBuff.damage;

        bindBool = UIController;
        bindBool.Duration = duration;
        bindBool.Activate();
    }

    public override void End()
    {
        Timer = 0;
        bindBool.End();
        bindBool = null;
    }

    public override void Refresh()
    {
        duration = potionedBuff.Duration;
        bindBool.Refresh();
    }
}
