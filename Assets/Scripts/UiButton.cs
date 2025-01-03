using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject uiLayout;
    public int buttonClick;

    public bool zoom;

    public CameraMovement cam;
    public int camClick;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClickEnter()
    {

        buttonClick += 1;
        if (buttonClick % 2 == 0)
        {
            uiLayout.SetActive(false);
        }
        else
        {
            uiLayout.SetActive(true);
        }
    }

    public void TurnOnPanning()
    {
        camClick += 1;
        if (camClick % 2 == 0 || camClick == 0)
        {
            cam.isPanning = false;
        }
        else
        {
            cam.isPanning = true;
        }
    }

    public void TurnOnZooming()
    {
        camClick += 1;
        if (camClick % 2 == 0 || camClick == 0)
        {
            cam.isZooming = false;
        }
        else
        {
            cam.isZooming = true;
        }
    }


}
