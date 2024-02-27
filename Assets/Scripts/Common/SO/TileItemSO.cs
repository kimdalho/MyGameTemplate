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
        Volcano = 1,
        Plains = 2,
        Mountain = 3,
        PlainsCastle = 4,
        Forest = 5,

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


