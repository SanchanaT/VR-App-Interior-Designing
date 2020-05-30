using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GazeableButton : GazeableObject
{
    protected VRCanvas parentpanel;

    // Start is called before the first frame update
    void Start()
    {
        parentpanel = GetComponentInParent<VRCanvas>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetButtonColor(Color buttoncolor)
    {
        GetComponent<Image>().color = buttoncolor;
    }

    public override void OnPress(RaycastHit hitobj)
    {
        base.OnPress(hitobj);
        if (parentpanel != null)
        {
            parentpanel.SetActiveButton(this);

        }
        else
        {
            Debug.Log(" button not child of the object with vrCanvas" + this);
        }
    }

}
 