using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    KineticEnergyWeapon,
    EnergyWeapon,
    ChemicalWeapon,
}

public class Weapon : MonoBehaviour
{
    public float weight;
    public float attackRange;
    public float spatterRange;
    public float attackSpeed;
    public float buildCost;
    public float useCost;

    protected virtual void Attack() { }


}
