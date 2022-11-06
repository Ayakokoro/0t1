using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="buffs/potionedBuff")]
public class PotionedBuff : ScriptableBuff
{
    public Image Icon;
    public float damage;
    public float delay;
    public override TimedBuff InstantiateBuff(GameObject obj)
    {
        return new TimedPotionedBuff(Duration, this, obj);
    }
}
