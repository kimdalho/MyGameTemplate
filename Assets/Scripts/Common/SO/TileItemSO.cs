using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타일
[System.Serializable]
public class TileItem
{
    public enum eCampType
    {
        None = 0,
        Base = 1,
        Volcano = 2,
        Plains = 3,
        Mountain = 4,
        PlainsCastle = 5,
        Forest = 6,

    }

    public string name;
    public eCampType type;
    public Sprite sprite;
    public int moveCost;
}

[CreateAssetMenu(fileName = "HexItemSO", menuName = "Scriptable Object/Hex/TileItemSO")]
public class TileItemSO : ScriptableObject
{
    public TileItem[] items;
}


