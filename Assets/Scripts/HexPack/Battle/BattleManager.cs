using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex_Package
{
    public class BattleManager : Singleton<BattleManager>
    {
        public void RequsetBattle()
        {
            Util.Log(string.Format("전투를 진행한다"));
        }
    }
}
