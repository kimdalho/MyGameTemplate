using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GameMaterialSO", menuName = "Scriptable Object/GameMaterial/GameMaterialSO")]
public class GameMaterialSO : ScriptableObject
{
    public GameMaterial[] models;
}
