using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private string doorTag = "Door";
    public static bool enableExit = false;
    public TMP_Text prompt;
    public GameObject endScreen;

    //mobile
    public Button clickButton;
    public bool buttonClicked = false;

    void Start()
    {
        enableExit = false;
    }
    
    //mobile
    public void click()
    {
        buttonClicked=true;
    }

    private void Update()
    {
        if(!EscMenu.isPaused){
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(doorTag) && enableExit)
                {
                    prompt.text = "Exit";
                    //if (Input.GetMouseButtonDown(0)){
                    //mobile
                    if(buttonClicked){
                        StartCoroutine(ExitCoroutine());
                    }
                }

                else
                {
                    prompt.text = "";
                }
            }
        }
        //mobile
        buttonClicked = false;
    }

    IEnumerator ExitCoroutine()
    {
        
        //play sound
        FindObjectOfType<AudioManager>().Play("door_click");
        //black screen + "End"
        endScreen.SetActive(true);
        //wait 1s
        yield return new WaitForSeconds(1.0f);
        // wait for input
        yield return new WaitWhile(() => (!Input.anyKey));
        //go back to main menu
        SceneManager.LoadScene(0);
    }
}
