using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타일
[System.Serializable]
public class TileItem
{
    public enum eType
    {
        None = 0,
        Base = 1,
        PlainsCastle = 2,
        Forest = 3,
        Mountain = 4,
        Volcano = 5,
    }

    public string name;
    public eType type;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "HexItemSO", menuName = "Scriptable Object/Hex/ItemSO")]
public class TileItemSO : ScriptableObject
{
    public TileItem[] items;
}


