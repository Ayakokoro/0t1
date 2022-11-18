using System;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    protected void Start()
    {
        health = maxHealth;
    }
    #region 生命
    private int health;
    public int maxHealth;

    private void KineticEnergyDamage(int atk)
    {
        health -= atk;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void ChemicalDamege(int atk)
    {
        health -= atk;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void DirectedEnergyDamage(int atk)
    {
        health -= atk;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="kind">伤害类型</param>
    /// <param name="atk">攻击力</param>
    public void Damage(BulletKind kind, int atk)
    {
        if (kind == BulletKind.KINETICENERGY) KineticEnergyDamage(atk);
        else if (kind == BulletKind.CHEMICAL) ChemicalDamege(atk);
        else if (kind == BulletKind.DIRECTEDENERGY) DirectedEnergyDamage(atk);

        if (health <= 0) Destroy(gameObject);
    }
    #endregion

    #region 护甲
    [Header("护甲")]
    public int baseArmor;
    public int kineticEnergyArmor;
    public int chemicalArmor;
    public int directedEnergyArmor;
    #endregion
}
