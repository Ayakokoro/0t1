using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    KineticEnergyWeapon,
    EnergyWeapon,
    ChemicalWeapon,
}

public abstract class Weapon : MonoBehaviour
{
    public float weight;
    public float attackRange;
    public float attackWidth;
    public float spatterRange;
    public float attackDelay;
    public float buildCost;
    public float useCost;
    public float bulletSpeed;

    public Transform CasingPos;

    public abstract void DoAttack(Vector3 dir);
    protected abstract void Shooting(Vector3 dir);
    protected abstract bool isAllowedShoot();


}
