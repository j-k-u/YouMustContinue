using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    public GameObject sittingChair;
    public GameObject exitChair;
    public GameObject playerCamera;

    public void PlayExit()
    {
        sittingChair.SetActive(false);
        exitChair.SetActive(true);
        Vector3 standUp = new Vector3(0,0.4f,0);
        playerCamera.transform.position += standUp;
        Player_Movement.speed = 2.0f;
        ExitDoor.enableExit = true;
    }

}
