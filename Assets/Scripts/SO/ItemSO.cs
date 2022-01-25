using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public string name;
    public int attack;
    public int health;
    public Sprite sprite;
    public Skill skill;
}

//반드시 외우다
[CreateAssetMenu(fileName = "ItemSO",menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}


[System.Serializable]
public class Skill
{

}