using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBuffExert : MonoBehaviour
{
    public ScriptableBuff buff;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TimedBuff timedBuff = buff.InstantiateBuff(other.gameObject);
            BuffManager manager = other.gameObject.GetComponent<BuffManager>();
            manager.AddBuff(timedBuff);
        }
    }
}
