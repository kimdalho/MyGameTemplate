using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eUnitType
{
    Creture = 0,
    Tower = 1,
}

[CreateAssetMenu(fileName = "HexItemSO",menuName = "Scriptable Object/Hex/UnitItemSO")]
public class UnititemSO : ScriptableObject
{
   public UnitItem[] items;
}


[System.Serializable]
public class UnitItem
{
    public eUnitType status;
    public string name;
    public Sprite render;
    public int grad;
    public int hp;
    public int atk;
    public int id;
}


public class TowerUnitSO : ScriptableObject
{
    //아직 이름 안정했다.
    public Tower nameless;
}