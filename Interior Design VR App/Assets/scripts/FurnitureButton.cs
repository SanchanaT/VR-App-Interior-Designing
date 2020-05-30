using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButton : GazeableButton
{
    public Object prefab;

    public override void OnPress(RaycastHit hitobj)
    {
        base.OnPress(hitobj);
        //set player mode to furniture
        Player.instance.activemode = InputMode.FURNITURE;

        //set active furniture prefab to its prefab
        Player.instance.activefurnitureprefab = prefab;
    }
}
