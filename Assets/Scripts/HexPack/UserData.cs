using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public Dictionary<eGoodsType, int> userGoods = new Dictionary<eGoodsType, int>();

    private void Awake()
    {
        var saveData = SaveLoad();

        SetData(saveData);
    }

    private bool SaveLoad()
    {
        return false;
    }

    private void SetData(bool existSaveData = false)
    {
        foreach (eGoodsType good in Enum.GetValues(typeof(eGoodsType)))
        {
            userGoods.Add(good, 0);
        }
    }
}
