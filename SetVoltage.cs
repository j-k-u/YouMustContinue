using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetVoltage : MonoBehaviour
{
    //serializefields
    [SerializeField] private GameObject minusButton;
    [SerializeField] private GameObject plusButton;

    [SerializeField] private GameObject led1;
    [SerializeField] private GameObject led2;
    [SerializeField] private GameObject led3;
    [SerializeField] private GameObject led4;
    [SerializeField] private GameObject led5;
    [SerializeField] private GameObject led6;
    [SerializeField] private GameObject led7;
    [SerializeField] private GameObject led8;

    [SerializeField] private Material litMaterial;
    [SerializeField] private Material unlitMaterial;

    public TMP_Text voltageDisplay;
    public GameObject warning;

    public static int voltage = 0;
    public static int minVoltage = 0;
    public static int maxVoltage = 45;
    private bool flashDisabled = true;

    //mobile
    public Button clickButton;
    public bool buttonClicked = false;

    void Start()
    {
        voltage = 0;
        minVoltage = 0;
        maxVoltage = 45;
        warning.SetActive(false);
    }

    //mobile
    public void click()
    {
        buttonClicked=true;
    }

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Voltage is " + voltage);
        voltageDisplay.text = voltage.ToString();

        if (voltage<15){
            led1.GetComponent<MeshRenderer>().material = unlitMaterial;
            led2.GetComponent<MeshRenderer>().material = unlitMaterial;
            led3.GetComponent<MeshRenderer>().material = unlitMaterial;
            led4.GetComponent<MeshRenderer>().material = unlitMaterial;
            led5.GetComponent<MeshRenderer>().material = unlitMaterial;
            led6.GetComponent<MeshRenderer>().material = unlitMaterial;
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;
            warning.SetActive(false);
        }

        if (voltage>=15 && voltage<30){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = unlitMaterial;
            led3.GetComponent<MeshRenderer>().material = unlitMaterial;
            led4.GetComponent<MeshRenderer>().material = unlitMaterial;
            led5.GetComponent<MeshRenderer>().material = unlitMaterial;
            led6.GetComponent<MeshRenderer>().material = unlitMaterial;
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;
            warning.SetActive(false);
        }

        else if (voltage>=30 && voltage<45){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = unlitMaterial;
            led4.GetComponent<MeshRenderer>().material = unlitMaterial;
            led5.GetComponent<MeshRenderer>().material = unlitMaterial;
            led6.GetComponent<MeshRenderer>().material = unlitMaterial;
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;
            warning.SetActive(false);
        }

        else if (voltage>=45 && voltage<90){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = litMaterial; //led3 lights at 45V
            led4.GetComponent<MeshRenderer>().material = unlitMaterial;
            led5.GetComponent<MeshRenderer>().material = unlitMaterial;
            led6.GetComponent<MeshRenderer>().material = unlitMaterial;
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;
            warning.SetActive(false);
        }

        else if (voltage>=90 && voltage<150){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = litMaterial; //led3 lights at 45V
            led4.GetComponent<MeshRenderer>().material = litMaterial; //led4 lights at 90V
            led5.GetComponent<MeshRenderer>().material = unlitMaterial;
            led6.GetComponent<MeshRenderer>().material = unlitMaterial;
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;
            warning.SetActive(false);
        }

        else if (voltage>=150 && voltage<225){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = litMaterial; //led3 lights at 45V
            led4.GetComponent<MeshRenderer>().material = litMaterial; //led4 lights at 90V
            led5.GetComponent<MeshRenderer>().material = litMaterial; //led5 lights at 150V
            led6.GetComponent<MeshRenderer>().material = unlitMaterial;
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;
            warning.SetActive(false);
        }

        else if (voltage>=225 && voltage<315){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = litMaterial; //led3 lights at 45V
            led4.GetComponent<MeshRenderer>().material = litMaterial; //led4 lights at 90V
            led5.GetComponent<MeshRenderer>().material = litMaterial; //led5 lights at 150V
            led6.GetComponent<MeshRenderer>().material = litMaterial; //led6 lights at 225V
            led7.GetComponent<MeshRenderer>().material = unlitMaterial;
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;

            warning.SetActive(true);
            //also light warning symbol
        }

        else if (voltage>=315 && voltage<450){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = litMaterial; //led3 lights at 45V
            led4.GetComponent<MeshRenderer>().material = litMaterial; //led4 lights at 90V
            led5.GetComponent<MeshRenderer>().material = litMaterial; //led5 lights at 150V
            led6.GetComponent<MeshRenderer>().material = litMaterial; //led6 lights at 225V
            led7.GetComponent<MeshRenderer>().material = litMaterial; //led7 lights at 315V
            led8.GetComponent<MeshRenderer>().material = unlitMaterial;

            warning.SetActive(true);
            // also light warning symbol
        }

        else if (voltage>=450){
            led1.GetComponent<MeshRenderer>().material = litMaterial; //led1 lights at 15V
            led2.GetComponent<MeshRenderer>().material = litMaterial; //led2 lights at 30V
            led3.GetComponent<MeshRenderer>().material = litMaterial; //led3 lights at 45V
            led4.GetComponent<MeshRenderer>().material = litMaterial; //led4 lights at 90V
            led5.GetComponent<MeshRenderer>().material = litMaterial; //led5 lights at 150V
            led6.GetComponent<MeshRenderer>().material = litMaterial; //led6 lights at 225V
            led7.GetComponent<MeshRenderer>().material = litMaterial; //led7 lights at 315V
            led8.GetComponent<MeshRenderer>().material = litMaterial; //led8 lights at 450V

            StartCoroutine(flashWarning());
            // also flash warning symbol
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && !EscMenu.isPaused)
        //mobile
        //if (buttonClicked && !EscMenu.isPaused)
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == plusButton)
            {
                FindObjectOfType<AudioManager>().Play("button_click_plus");
                if (voltage<maxVoltage){
                    voltage += 15;
                }
            }

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == minusButton)
            {
                FindObjectOfType<AudioManager>().Play("button_click_minus");
                if (voltage>minVoltage){
                    voltage -= 15;
                }
            }
        }
        //mobile
        buttonClicked = false;
    }

    IEnumerator flashWarning()
    {
        if (flashDisabled)
        {
            flashDisabled = false;
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                warning.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                warning.SetActive(true);
            }
        }
    }
}
