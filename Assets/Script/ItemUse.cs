using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
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
