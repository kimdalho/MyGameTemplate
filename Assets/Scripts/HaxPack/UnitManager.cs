using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] GameObject playerPrefab;

    public void CreatePlayer(Vector3 pos)
    {
        //8.5의 오차가 발생 

        Vector3 vec = new Vector3(pos.x, pos.y + 8.5f, 0f);
        var go = Instantiate(playerPrefab, vec, Util.QI);
        
    }
}
