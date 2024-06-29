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
    [SerializeField] GameObject itemBar;

    [Header("-----AI Setting-----")]
    [SerializeField] bool isPlayWithAI = false;
    Difficulty _difficulty;
    [SerializeField] int missedRate;
    [SerializeField] GameObject playerCritPoint;
    [SerializeField] bool itemHeal = true, itemPow = true, itemDouble = true;

    private void Awake()
    {
        throwSystem = this.GetComponent<ThrowSystem>();
        throwSystem._switchIsTurn = SwitchIsTurn;
        throwSystem.throwSlider = throwSlider;
        itemBar.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isTurn)
        {
            // target.transform.position += Vector3.left * Time.deltaTime;
            throwSlider.SetActive(true);
            throwSystem.isMousePressed = true;
            itemBar.SetActive(false);
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
                itemBar.SetActive(true);
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
        bool isHit = UnityEngine.Random.Range(1, 101) > missedRate;
        Debug.Log(isHit);

        if (GameManager.Instance.GetPlayerHP() <= GameManager.Instance.GetPlayerMaxHP() / 2)
        {
            if (itemHeal)
            {
                ItemUse.Instance.ItemHeal();
                itemHeal = false;
            }
        }

        if (isHit)
        {
            if (_difficulty == Difficulty.Normal || _difficulty == Difficulty.Hard)
            {
                if (GameManager.Instance.windValue <= 2f && GameManager.Instance.windValue >= -2f)
                {
                    if (itemDouble)
                    {
                        ItemUse.Instance.ItemDoubleATK();
                        itemDouble = false;
                    }
                }
            }
            if (_difficulty == Difficulty.Hard)
            {
                if (GameManager.Instance.windValue > 2f && GameManager.Instance.windValue < -2f)
                {
                    if (itemPow)
                    {
                        ItemUse.Instance.ItemPowerATK();
                        itemPow = false;
                    }
                }
            }
            throwSystem.AIThrowOBJ(playerCritPoint.transform.position, true);
        }
        else
            throwSystem.AIThrowOBJ(playerCritPoint.transform.position, false);
    }
}
