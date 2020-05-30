using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : GazeableObject
{
    public override void OnPress(RaycastHit hitobj)
    {
        base.OnPress(hitobj);

        if (Player.instance.activemode == InputMode.TELEPORT)
        {
            Vector3 destination = hitobj.point;
            destination.y = Player.instance.transform.position.y;
            Player.instance.transform.position = destination;
        }
        else if (Player.instance.activemode == InputMode.FURNITURE)
        {
            //place furniture
            GameObject placefurniture = GameObject.Instantiate(Player.instance.activefurnitureprefab) as GameObject;

            //set position of thr  furniture
            placefurniture.transform.position = hitobj.point;

        }
    }
}
