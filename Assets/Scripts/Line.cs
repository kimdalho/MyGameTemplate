using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line 
{
    //Line id
    [SerializeField]
    int id;
    //Bat Count
    [SerializeField]
    int Count;

    public void SetCount(int count)
    {
        this.Count = count;
    }

}
