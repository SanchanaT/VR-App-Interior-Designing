using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : GazeableButton
{
    [SerializeField]
    private InputMode mode;

    public override void OnPress(RaycastHit hitobj)
    {
        base.OnPress(hitobj);
        if (parentpanel.currentbutton != null)
        {

            Player.instance.activemode = mode;
        }

    }
}
