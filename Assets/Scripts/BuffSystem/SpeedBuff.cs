using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="buffs/speedBuff")]
public class SpeedBuff : ScriptableBuff
{
    public float speedIncrease;
    public override TimedBuff InstantiateBuff(GameObject obj)
    {
        return new TimedSpeedBuff(Duration, this, obj);
    }
}
