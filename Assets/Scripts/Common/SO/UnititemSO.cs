using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//대분류
public enum eUnitType
{
    Creture = 0,
    Tower = 1,
}
//소분류
public enum eCretureType
{
    None = 0,
    //임프
    Imp = 1,
    //무생물
    Inanimate = 2,
    //포유류
    Mammalia = 3,

}

public enum eTowerType
{
    None = 0,
    Barracks = 1,
    Mushroom = 2,

}

[CreateAssetMenu(fileName = "HexItemSO",menuName = "Scriptable Object/Hex/UnitItemSO")]
public class UnititemSO : ScriptableObject
{
   public UnitItem[] items;
}


[System.Serializable]
public class UnitItem
{
    public eTowerType towerType;
    public eCretureType cretureType;
    public eUnitType status;
    public int spawn;
    public string name;
    public string description;
    public Sprite render;
    public int grad;
    public UnitStat baseStat;
    public int id;
    public List<int> cretureUnits;
}


public class TowerUnitSO : ScriptableObject
{
    //아직 이름 안정했다.
    public Tower nameless;
}