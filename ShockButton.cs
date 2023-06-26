using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShockButton : MonoBehaviour
{
    //serializefields
    [SerializeField] private GameObject shockButton;
    
    //mobile
    public Button clickButton;
    public bool buttonClicked = false;

    public ResearcherDialogue researcherDialogue;
    public static bool enabled = false;
    
    //mobile
    public void click()
    {
        buttonClicked=true;
    }
    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        //mobile
        //if (buttonClicked)
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == shockButton)
            {
                FindObjectOfType<AudioManager>().Play("button_click_shock");
                

                if (enabled==true)
                {
                    //FindObjectOfType<AudioManager>().PlayDelayed("shock_charging", 0.5f);
                    FindObjectOfType<AudioManager>().PlayDelayed("shock_impact", 1.5f);

                    ResearcherDialogue.interrupted = false;
                    enabled = false;
                    researcherDialogue.Speak();
                }
            }
        }

        //mobile
        buttonClicked = false;
    }
}
