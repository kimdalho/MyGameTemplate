using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int maxHunger;
    public int hunger;
    public int maxHp;
    public int curHp;
    public int atk;
    public int move;

    public int eat;


    public void Setup()
    {
        maxHunger = 100;
        hunger = maxHunger;
        maxHp = 10;
        curHp = maxHp;
        atk = 3;
        move = 1;
    }
}

