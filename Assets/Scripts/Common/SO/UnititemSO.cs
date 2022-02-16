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
    public QuestManager.EQusetType questType;
    public int id;
}


public class TowerUnitSO : ScriptableObject
{
    //아직 이름 안정했다.
    public Tower nameless;
}