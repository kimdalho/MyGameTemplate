using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameMaterialSO", menuName = "Scriptable Object/GameMaterial/GameMaterialSO")]
public class GameMaterialSO : ScriptableObject
{
    public GameMaterial[] models;

    public Dictionary<eGoodsType, Sprite> goodsIcons;

    GameMaterialSO() { 

    }

}
