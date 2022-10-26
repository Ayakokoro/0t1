using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoyStick : JoyStick
{

    protected override void Update()
    {
        base.Update();
        if (isEnabled)
        {
            Vector3 direct = Vector3.Cross(mainCharacter.forwardGlobal, dir);
            DIRECTION idir = direct.z > 0 ? DIRECTION.Counterclockwise : DIRECTION.Clockwise;
            mainCharacter.Movement(idir);
        }
    }
}
