using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    static public ItemUse Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public void ItemHeal()
    {
        GameManager.Instance.UseItemHeal();
    }

    public void ItemPowerATK()
    {
        GameManager.Instance.isPowerAtk = true;
    }

    public void ItemDoubleATK()
    {
        GameManager.Instance.isDoubleAtk = true;
    }
}
