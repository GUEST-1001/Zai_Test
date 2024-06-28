using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("-----Game Element-----")]
    static public GameManager Instance;
    [SerializeField] GameUpdateConfig configGoogleSheet;
    [SerializeField] PlayerController player1, player2;
    [SerializeField] TMP_Text windText, p1HpText, p2HpText;
    [SerializeField] SkeletonAnimation p1Spine, p2Spine;
    Spine.AnimationState p1SpineState, p2SpineState;
    [SerializeField] TwoWaySlider windSlider;
    [SerializeField] Image windSliderFill;
    [SerializeField] Slider p1HpSlider, p2HpSlider;

    [Header("-----Game Value-----")]
    public float windValue;
    public float mainTargetSpeed;
    int p1Hp, p2Hp;
    public bool isPlayer1Turn = false;

    #region Google Sheet Config
    [Header("-----Google Sheet Config-----")]
    public ConfigList playerHP;
    public ConfigList enemyEasy;
    public ConfigList enemyNormal;
    public ConfigList enemyHard;
    public ConfigList normalAtk;
    public ConfigList smallAtk;
    public ConfigList PowAtk;
    public ConfigList DoubleAtk;
    public ConfigList heal;
    public ConfigList timeToThink;
    public ConfigList timeToWarn;

    #endregion

    private void Awake()
    {
        Instance = this;
        p1SpineState = p1Spine.AnimationState;
        p2SpineState = p2Spine.AnimationState;

        StartCoroutine(SyncGoogleSheet());
        p1Hp = playerHP.HP;
        p2Hp = playerHP.HP;
        // player1.isTurn = true;
    }

    private void Start()
    {
        SetHPUI();
        RandomWind();
        FilpTurn();
    }

    IEnumerator SyncGoogleSheet()
    {
        configGoogleSheet.Sync();
        foreach (var item in configGoogleSheet.config)
        {
            switch (item.name)
            {
                case "PlayerHP":
                    playerHP = item;
                    break;
                case "EnemyEasy":
                    enemyEasy = item;
                    break;
                case "EnemyNormal":
                    enemyNormal = item;
                    break;
                case "EnemyHard":
                    enemyHard = item;
                    break;
                case "NormalAttack":
                    normalAtk = item;
                    break;
                case "SmallAttack":
                    smallAtk = item;
                    break;
                case "PowerAttack":
                    PowAtk = item;
                    break;
                case "DoubleAttack":
                    DoubleAtk = item;
                    break;
                case "Heal":
                    heal = item;
                    break;
                case "TimeToThink":
                    timeToThink = item;
                    break;
                case "TimeToWarning":
                    timeToWarn = item;
                    break;

            }
        }
        yield return null;
    }

    public void FilpTurn()
    {
        // player1.isTurn = !player1.isTurn;
        // player2.isTurn = !player2.isTurn;
        PlayAni("Idle UnFriendly 1", "Idle UnFriendly 1");
        isPlayer1Turn = !isPlayer1Turn;
        switch (isPlayer1Turn)
        {
            case true:
                player1.SetHitBox(false);
                player2.SetHitBox(true);
                player1.StartTurn();
                break;
            case false:
                player1.SetHitBox(true);
                player2.SetHitBox(false);
                player2.StartTurn();
                break;
        }
    }

    public void EndTurn()
    {
        RandomWind();
        FilpTurn();
    }

    void RandomWind()
    {
        windValue = Random.Range(0f, 5f);
        if (windValue < 1f)
        {
            windValue = 0f;
        }
        else
        {
            switch (Random.Range(0, 2))
            {
                case 1:
                    windValue = -windValue;
                    windSliderFill.color = Color.red;
                    break;
                case 0:
                    windSliderFill.color = Color.blue;
                    break;
            }
        }
        windSlider.UpdateSliderValue(windValue);
        windText.text = windValue.ToString();
    }

    public void SetHPUI()
    {
        p1HpText.text = p1Hp.ToString();
        p2HpText.text = p2Hp.ToString();
    }

    public void CalcEndTurn(string hitType)
    {
        switch (hitType)
        {
            case "CritHit":
                CalcHitDmg(normalAtk.damage);
                StartCoroutine(PlayAniAndWait("Happy Friendly", "Moody UnFriendly", 3f));
                break;
            case "Hit":
                CalcHitDmg(smallAtk.damage);
                StartCoroutine(PlayAniAndWait("Happy Friendly", "Moody UnFriendly", 3f));
                break;
            case "NoHit":
                CalcHitDmg(0);
                StartCoroutine(PlayAniAndWait("Moody UnFriendly", "Happy Friendly", 3f));
                break;
        }
    }

    void CalcHitDmg(int dmg)
    {
        switch (isPlayer1Turn)
        {
            case true:
                p2Hp -= dmg;
                p2Hp = p2Hp < 0 ? 0 : p2Hp;
                break;
            case false:
                p1Hp -= dmg;
                p1Hp = p1Hp < 0 ? 0 : p1Hp;
                break;
        }
        SetHPUI();
    }

    void PlayAni(string p1, string p2)
    {
        p1SpineState.SetAnimation(0, p1, true);
        p2SpineState.SetAnimation(0, p2, true);
    }

    IEnumerator PlayAniAndWait(string nowPlayer, string targetPlayer, float waitTime)
    {
        if (p1Hp > 0 && p2Hp > 0)
        {
            switch (isPlayer1Turn)
            {
                case true:
                    PlayAni(nowPlayer, targetPlayer);
                    break;
                case false:
                    PlayAni(targetPlayer, nowPlayer);
                    break;
            }
            yield return new WaitForSeconds(waitTime);
            EndTurn();
        }
        else
        {
            WinLoseCheck();
        }
    }

    void WinLoseCheck()
    {
        if (p1Hp <= 0)
        {
            PlayAni("Moody UnFriendly", "Cheer Friendly");
            Debug.Log("Player 2 Win");
        }
        else
        {
            PlayAni("Cheer Friendly", "Moody UnFriendly");
            Debug.Log("Player 1 Win");
        }
    }

}
