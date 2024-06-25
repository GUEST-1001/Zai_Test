using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    [SerializeField] ThrowSystem player1, player2;

    public float mainTargetSpeed;

    private void Awake()
    {
        Instance = this;
        player1.isTurn = true;
    }

    public void FilpTurn()
    {
        player1.isTurn = !player1.isTurn;
        player2.isTurn = !player2.isTurn;
    }

}
