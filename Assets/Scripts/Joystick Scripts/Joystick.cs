﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private PlayerMoveJoystick playerMove;

    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMoveJoystick>();
    }

    public void OnPointerUp(PointerEventData data)
    {
        playerMove.StopMoving();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (gameObject.name == "Left")
        {
            playerMove.SetMoveLeft(true);
        }
        else
        {
            playerMove.SetMoveLeft(false);
        }
    }
}
