using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillJoyStick : JoyStick
{
    public LineRenderer line;
    public float attackRange;
    public Weapon weapon;

    protected override void Update()
    {
        base.Update();
        if (isEnabled)
        {
            line.enabled = true;
            range.gameObject.SetActive(true);
            line.SetPosition(0, mainCharacter.transform.position);
            line.SetPosition(1, mainCharacter.transform.position + dir * attackRange);

            Debug.Log(dir);
            weapon.DoAttack(dir);
        }
        else
        {
            range.gameObject.SetActive(false);
            line.enabled = false;
        }
    }
}
