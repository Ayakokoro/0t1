using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScriptableBuff : ScriptableObject
{
    public Sprite Icon;
    public float Duration;
    public BuffType buffType;
    public abstract TimedBuff InstantiateBuff(GameObject obj);
}
