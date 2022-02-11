using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HexItemSO",menuName = "Scriptable Object/Hex/UnitItemSO")]
public class UnititemSO : ScriptableObject
{
   public UnitItem[] items;
}


[System.Serializable]
public class UnitItem
{
    public string name;
    public Sprite render;
    public int hp;
    public int atk;
    public string quest;
    public int id;
}
