using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardItem
{
    public string name;
    public int attack;
    public int health;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "TCGItemSO", menuName = "Scriptable Object/TCG/ItemSO")]
public class CardItemSO : ScriptableObject
{
    public CardItem[] items;
}


