using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuText;
    public GameObject CreditsList;
    public GameObject ControlsList;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        MenuText.SetActive(false);
        CreditsList.SetActive(true);
    }

    public void ShowControls()
    {
        MenuText.SetActive(false);
        ControlsList.SetActive(true);
    }

    void Start()
    {
        Cursor.visible = true;
    }
}
