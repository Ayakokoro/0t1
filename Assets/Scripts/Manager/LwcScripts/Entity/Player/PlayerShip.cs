using System;
using System.Collections.Generic;
using UnityEngine;

class PlayerShip : BaseEntity
{
    [SerializeField] private BulletPool bulletPool;
    private string player;
    private void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
    }
    private void Update()
    {
        last += Time.deltaTime;
        KineticEnergyAttack();
    }
    #region 攻击 
    private float last;
    [SerializeField] private float CD;
    #region 动能攻击
    [SerializeField] private int KineticEnergyAtk;
    [SerializeField] private int KineticEnergySpeed;
    [SerializeField] private bool KineticEnergyIsChaser;
    void KineticEnergyAttack()
    {
        if (last >= CD)
        {
            GameObject now = bulletPool.GetBullet(BulletKind.KINETICENERGY, transform.position, transform.rotation);
            now.GetComponent<BaseBullet>().SetBullet(gameObject, BulletKind.KINETICENERGY, KineticEnergyAtk, KineticEnergySpeed, KineticEnergyIsChaser);
        }
    }
    #endregion

    // TODO Other...
    #endregion
}