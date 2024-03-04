using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Relic 
{
    public int id;
    public Sprite icon;
    public string name;
    public int grade;
    public UnitStat stat;
    public Skill skill;

}

[System.Serializable]
public class UnitStat
{
    public int maxHp;
    public int curHp;
    public int atk;
    public int move;
    public int fullness;
}
