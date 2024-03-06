using System;
using UnityEngine;

[System.Serializable]
public class ResearchModel
{
    public int id;
    public Sprite thumbnail;
    public string name;
    public string description;
    public string startScript;
    public string endScript;
    public int hungerCost;
    public int goldCost;
    public int turnCost;
    public int castleLevel;
    public eRelicType reward;
}
