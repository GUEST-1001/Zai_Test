using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObject/GameConfig")]
public class GameUpdateConfig : ScriptableObject
{
    [SerializeField] string sheetID, gridID;
    public List<ConfigList> config;

    [ContextMenu("Sync")]
    public void Sync()
    {
        ReadGoogleSheets.FillData<ConfigList>(sheetID, gridID, list =>
        {
            config = list;
            ReadGoogleSheets.SetDirty(this);
        });
    }

    [ContextMenu("OpenSheet")]
    private void OpenSheet()
    {
        ReadGoogleSheets.OpenUrl(sheetID, gridID);
    }
}

[Serializable]
public class ConfigList
{
    public string name;
    public int amount;
    public int damage;
    public int HP;
    public float MissedChance;
    public float sec;
}
