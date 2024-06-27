using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    [SerializeField] ThrowSystem player1, player2;
    [SerializeField] TMP_Text windText;

    public float windValue;

    public float mainTargetSpeed;

    private void Awake()
    {
        Instance = this;
        player1.isTurn = true;
        RandomWind();
    }

    public void FilpTurn()
    {
        player1.isTurn = !player1.isTurn;
        player2.isTurn = !player2.isTurn;
        RandomWind();
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
