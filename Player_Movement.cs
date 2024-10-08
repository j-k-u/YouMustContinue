﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public static float speed = 0.0f;
    public float gravity = -9.8f;

    private CharacterController _charCont;

    void Start()
    {
       _charCont = GetComponent<CharacterController> ();
       speed = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical")*speed;
        Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement*=Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);

    }
}
