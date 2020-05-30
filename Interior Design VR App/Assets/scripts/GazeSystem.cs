using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour
{
    public GameObject reticle;

    public Color inactivecolor = Color.grey;
    public Color activecolor = Color.green;

    private GazeableObject currentGazeableObject;
    private GazeableObject currentselectedobj;


    private RaycastHit lasthit;

    // Start is called before the first frame update
    void Start()
    {
        SetReticleColor(inactivecolor);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGaze();
        CheckForInput(lasthit);

    }

    public void ProcessGaze()
    {
        Ray raycastray = new Ray(transform.position, transform.forward);
        RaycastHit hitobj;

        Debug.DrawRay(raycastray.origin, raycastray.direction * 100);

        if (Physics.Raycast(raycastray, out hitobj))
        {
            //get gameobject
            GameObject hitgazeobj = hitobj.collider.gameObject;
            //get gaze of the gameobj
            GazeableObject gazeobj = hitgazeobj.GetComponentInParent<GazeableObject>();

            if (gazeobj != null)
            {
                if (gazeobj != currentGazeableObject)
                {
                    ClearCurrentObject();
                    currentGazeableObject = gazeobj;
                    currentGazeableObject.OnGazeEnter(hitobj);
                    SetReticleColor(activecolor);
                }
                else
                {

                    currentGazeableObject.OnGaze(hitobj);
                }
            }
            else
            {
                ClearCurrentObject();

            }

            lasthit = hitobj;


        }
        else
        {
            ClearCurrentObject();
        }

    }

    private void SetReticleColor(Color reticleColor)
    {
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
    }

    private void CheckForInput(RaycastHit hitobj)
    {
        //down
        if (Input.GetMouseButtonDown(0) && currentGazeableObject != null)
        {
            currentselectedobj = currentGazeableObject;
            currentselectedobj.OnPress(hitobj);

        }
        //hold
        if (Input.GetMouseButton(0) && currentselectedobj != null)
        {
            currentselectedobj.OnHold(hitobj);

        }
        //exit
        if (Input.GetMouseButtonUp(0) && currentselectedobj != null)
        {
            currentselectedobj.OnRelease(hitobj);
            currentselectedobj = null;
        }
    }

    private void ClearCurrentObject()
    {
        if (currentGazeableObject != null)
            currentGazeableObject.OnGazeExit();
        SetReticleColor(inactivecolor);
        currentGazeableObject = null;

    }

}
