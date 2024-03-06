using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCampType
{
    PlainsCastle = 0,
    Volcano = 1,
    Plains = 2,
    Mountain = 3, 
    Forest = 5,
    Desert = 6,
    Jungle = 7,
    Ocean = 8,
    ForestSnow = 9,
    None = 10,


}

//타일
[System.Serializable]
public class TileItem
{
    public string name;
    public eCampType type;
    public Sprite sprite;
    public int moveCost;
}

[CreateAssetMenu(fileName = "HexItemSO", menuName = "Scriptable Object/Hex/TileItemSO")]
public class TileItemSO : ScriptableObject
{
    [SerializeField]
    private TileItem[] items;

    public Dictionary<eCampType, TileItem> camps;

    public void Setup()
    {
        camps = new Dictionary<eCampType, TileItem>();
        foreach (var item in items)
        {
            
            try
            {
                camps.Add(item.type, item);
            }
            catch(Exception e)
            {
                Debug.LogError("catch already exist key more set try");
            }
           
        }
    }
}


