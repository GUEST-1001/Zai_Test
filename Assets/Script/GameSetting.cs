using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObject/GameSetting")]
public class GameSetting : ScriptableObject
{
    public bool playWithAI;
    public Difficulty selectDifficulty;
}
