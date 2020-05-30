using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvas : MonoBehaviour
{
    public GazeableButton currentbutton;

    public Color unselectedcolor = Color.gray;
    public Color selectedcolor = Color.blue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    public void SetActiveButton(GazeableButton activebutton)
    {
        if (currentbutton != null)
        {
            currentbutton.SetButtonColor(unselectedcolor);
        }

        if (activebutton != null && currentbutton != activebutton)
        {
            currentbutton = activebutton;
            currentbutton.SetButtonColor(selectedcolor);
        }
        else
        {
            Debug.Log("Resetting");
            currentbutton = null;
            Player.instance.activemode = InputMode.NONE;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 playerpos = Player.instance.transform.position;
        Vector3 vectoplayer = playerpos - transform.position;

        Vector3 lookatpos = transform.position - vectoplayer;

        transform.LookAt(lookatpos); // makes both the vector to look in the same direction
    }
}
