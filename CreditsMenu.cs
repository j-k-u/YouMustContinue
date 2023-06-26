using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject MenuText;
    public GameObject CreditsList;

    public void HideCredits()
    {
        MenuText.SetActive(true);
        CreditsList.SetActive(false);
    }
}