using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameSetting gameSetting;

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetPlayWithAI(bool playWithAi)
    {
        gameSetting.playWithAI = playWithAi;
        ReadGoogleSheets.SetDirty(this);
    }

    public void SetDifficultyAndLoadScene(int difficulty)
    {
        gameSetting.selectDifficulty = (Difficulty)difficulty;
        ReadGoogleSheets.SetDirty(this);
        LoadSceneByIndex(1);
    }
}
