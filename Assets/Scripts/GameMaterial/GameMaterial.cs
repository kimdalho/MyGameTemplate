using UnityEngine;
using System.Collections;

public enum eGoodsType
{
    None = 0,
    Wood = 1,
    Stone = 2,
    GoldIngot = 3,
    Food = 4,
    Gem = 5,
    Bone = 6,
}

[System.Serializable]
public class GameMaterial 
{
    public int id;
    public eGoodsType type;
    public string materialName;
    public int count;
    public int grade;
    public Sprite icon;

    public void Drop()
    {
        UserData.Instance.userGoods[type] += count;
    }
}
