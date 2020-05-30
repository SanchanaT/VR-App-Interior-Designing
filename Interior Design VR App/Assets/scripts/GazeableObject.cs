using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
    public bool isTransformable = false;
    private int objectlayer;
    private const int IGNORE_RAYCAST_LAYER = 2;

    private Vector3 initalobjectrotation;
    private Vector3 initalplayerrotation;

    private Vector3 initalobjectscale;

    private void Start()
    {
        if (isTransformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;
        }
    }

    public virtual void OnGazeEnter(RaycastHit hitobj)
    {
        Debug.Log("on gaze enter " + gameObject.name);

      /* if (isTransformable && (Player.instance.activemode == InputMode.TRANSLATE || Player.instance.activemode == InputMode.ROTATE || Player.instance.activemode == InputMode.SCALE))
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = true;
        }*/
    }

    public virtual void OnGaze(RaycastHit hitobj)
    {
        Debug.Log("on gaze hold " + gameObject.name);
    }

    public virtual void OnGazeExit()
    {
        Debug.Log("on gaze exit " + gameObject.name);

       /* if (isTransformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;
        }*/
    }

    public virtual void OnPress(RaycastHit hitobj)
    {
        Debug.Log("Button Pressed ");

        if (isTransformable)
        {
            objectlayer = gameObject.layer;
            gameObject.layer = IGNORE_RAYCAST_LAYER;

            initalobjectrotation = transform.rotation.eulerAngles;
            initalplayerrotation = Camera.main.transform.eulerAngles;

            initalobjectscale = transform.localScale;
        }
    }

    public virtual void OnHold(RaycastHit hitobj)
    {
        Debug.Log("Button Hold ");

        if (isTransformable)
        {
            GazeTransform(hitobj);
        }
    }

    public virtual void OnRelease(RaycastHit hitobj)
    {
        Debug.Log("Button Released");

        if (isTransformable)
        {
            gameObject.layer = objectlayer;
        }
    }

    public virtual void GazeTransform(RaycastHit hitobj)
    {
        switch (Player.instance.activemode)
        {
            case InputMode.TRANSLATE:
                GazeTranslate(hitobj);
                break;

            case InputMode.ROTATE:
                GazeRotate(hitobj);
                break;

            case InputMode.SCALE:
                GazeScale(hitobj);
                break;


        }
    }

    public virtual void GazeTranslate(RaycastHit hitobj)
    {
        if (hitobj.collider != null && hitobj.collider.GetComponent<Floor>())
        {
            transform.position = hitobj.point;
        }
    }

    public virtual void GazeRotate(RaycastHit hitobj)
    {
        float rotationspeed = 5.0f;

        Vector3 currentplayerrotation = Camera.main.transform.rotation.eulerAngles;

        Vector3 currentobjectrotation = transform.rotation.eulerAngles;

        Vector3 rotationdelta = currentplayerrotation - initalplayerrotation;

        Vector3 newrotation = new Vector3(currentobjectrotation.x, initalobjectrotation.y + (rotationdelta.y * rotationspeed), currentobjectrotation.z);
        transform.rotation = Quaternion.Euler(newrotation); // converts eluer angle to quaternion

    }

    public virtual void GazeScale(RaycastHit hitobj)
    {
        float scalespeed = 0.1f;
        float scalefactor = 1;//obj grows 1 imes bigger 1 is the least

        Vector3 currentplayerrotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 rotationdelta = currentplayerrotation - initalplayerrotation;

        // if looking up DONT FORGET .X
        if (rotationdelta.x < 0.0f && rotationdelta.x > -180.0f || rotationdelta.x > 180.0f && rotationdelta.x < 360.0f)
        {
            // since rotation of 360 is massive keep it less than 180
            if (rotationdelta.x > 180.0f)
            {
                rotationdelta.x = 360.0f - rotationdelta.x;

            }

            scalefactor = 1.0f + Mathf.Abs(rotationdelta.x) * scalespeed; // takes absolute value betwwen 0 to 180
        }

        else
        {
            if (rotationdelta.x < -180.0f)
            {
                rotationdelta.x = 360.0f + rotationdelta.x;  // GETS RID OF NEGATIVE

            }
            scalefactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(rotationdelta.x) * (1.0f / scalespeed)) / 180.0f); // 1.0f - is to keep it from 0 to 1 range , / 180 to shrink by half ,1.0f/speed makes it get smaller more faaster


        }
        transform.localScale = scalefactor * initalobjectscale;
    }
}


