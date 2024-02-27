using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
public class Tower : Unit,ITurnSystem
{
    public List<Node> spawnzones;
    public Node location;


    public void EndPlayerMove()
    {
        throw new System.NotImplementedException();
    }

    public void StartPlayerTurn()
    {
        throw new System.NotImplementedException();
    }

    public void TurnAwake()
    {
        throw new System.NotImplementedException();
    }
}
