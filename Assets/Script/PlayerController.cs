using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isTurn = false;
    ThrowSystem throwSystem;
    BoxCollider2D mouseArea;

    private void Awake()
    {
        throwSystem = this.GetComponent<ThrowSystem>();
        mouseArea = this.GetComponent<BoxCollider2D>();
        throwSystem._switchIsTurn = SwitchIsTurn;
    }

    private void OnMouseDown()
    {
        if (isTurn)
        {
            // target.transform.position += Vector3.left * Time.deltaTime;
            throwSystem.isMousePressed = true;
        }
    }

    private void OnMouseUp()
    {
        if (isTurn)
        {
            throwSystem.isMousePressed = false;
            throwSystem.ThrowOBJ();
            SwitchIsTurn(false);
        }
    }

    void SwitchIsTurn(bool _isTurn)
    {
        isTurn = _isTurn;
    }

    // public void MyTurn()
    // {
    //     mouseArea.enabled = false;
    // }

    public void StartTurn()
    {
        SwitchIsTurn(true);
    }
}
