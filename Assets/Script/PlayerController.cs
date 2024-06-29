using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isTurn = false;
    ThrowSystem throwSystem;
    [SerializeField] GameObject critHit, hit;
    [SerializeField] GameObject throwSlider;

    [Header("-----AI Setting-----")]
    bool isPlayWithAI = false;
    Difficulty _difficulty;
    int missedRate;
    [SerializeField] GameObject playerCritPoint;

    private void Awake()
    {
        throwSystem = this.GetComponent<ThrowSystem>();
        throwSystem._switchIsTurn = SwitchIsTurn;
        throwSystem.throwSlider = throwSlider;
    }

    private void OnMouseDown()
    {
        if (isTurn)
        {
            // target.transform.position += Vector3.left * Time.deltaTime;
            throwSlider.SetActive(true);
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
        switch (isPlayWithAI)
        {
            case false:
                SwitchIsTurn(true);
                break;
            case true:
                AIPlay();
                break;
        }
    }

    public void SetHitBox(bool hitBox)
    {
        critHit.SetActive(hitBox);
        hit.SetActive(hitBox);
    }

    public void SetAISetting()
    {
        isPlayWithAI = true;
        _difficulty = GameManager.Instance.difficulty;
        missedRate = GameManager.Instance.aiHitRate;
    }

    void AIPlay()
    {
        throwSystem.AIThrowOBJ(playerCritPoint.transform.position, true);
    }
}
