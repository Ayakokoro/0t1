using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int cur;
    public void Damage(int atk)
    {
        cur -= atk;
        if (cur < 0)
        {
            Destroy(gameObject);
        }
    }
}
