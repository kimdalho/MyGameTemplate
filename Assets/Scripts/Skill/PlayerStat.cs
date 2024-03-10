using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : UnitStat
{
    public PlayerStat() : base()
    {
       
    }

    public int maxHunger;
    public int hunger;

    public void Setup()
    {
        maxHunger = 30;
        hunger = maxHunger;
        maxHp = 30;
        curHp = maxHp;
        atk = 4;
        move = 3;
    }
}

