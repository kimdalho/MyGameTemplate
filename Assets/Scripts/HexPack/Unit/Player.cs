using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit ,ITurnSystem
{
    public void EndPlayerMove()
    {
        throw new System.NotImplementedException();
    }

    public void StartPlayerTurn()
    {
        
    }

    void ITurnSystem.TurnAwake()
    {
       //연public void 
    }
}
