using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    [SerializeField] PlayerController player1, player2;
    [SerializeField] TMP_Text windText;

    public float windValue;

    public float mainTargetSpeed;

    public bool isPlayer1Turn = false;

    [SerializeField] GameUpdateConfig configGoogleSheet;

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

    private void Awake()
    {
        Instance = this;
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

        RandomWind();
        FilpTurn();
        // player1.isTurn = true;
    }

    public void FilpTurn()
    {
        // player1.isTurn = !player1.isTurn;
        // player2.isTurn = !player2.isTurn;
        isPlayer1Turn = !isPlayer1Turn;
        switch (isPlayer1Turn)
        {
            case true:
                player1.StartTurn();
                break;
            case false:
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
        switch (Random.Range(0, 2))
        {
            case 1:
                windValue = -windValue;
                break;
        }
        windText.text = windValue.ToString();
    }



}
