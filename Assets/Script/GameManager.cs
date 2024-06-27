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

    [SerializeField] bool isPlayer1Turn = false;

    private void Awake()
    {
        Instance = this;
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
