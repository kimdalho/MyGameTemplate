using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Unit
{
    public PlayerStat playerStat;
    public Agent agent;
    public List<IPlayerHit> iplayerhit = new List<IPlayerHit>();

    private void Awake()
    {
        playerStat = new PlayerStat();
        playerStat.Setup();
        status = eUnitType.Player;
    }

    public override int Hit(Unit AttackUnit)
    {
        foreach(var _obj in iplayerhit)
        {
            _obj.HitPlayer();
        }
        playerStat.curHp = Math.Clamp(playerStat.curHp - AttackUnit.GetAttack(), 0, playerStat.maxHp);
        return playerStat.curHp;
    }

    public override int GetAttack()
    {
        return playerStat.atk;
    }

    public override int GetCurrentHp()
    {
        return playerStat.curHp;
    }

    public int ChangeCurrentHp(int hill)
    {
        return Math.Clamp(GetCurrentHp()+ hill, 0, playerStat.maxHp);
    }

    public override void Dead()
    {
        Debug.Log("GameOver");
    }

    public Node GetNowNode()
    {
        return agent.nowNode;
    }

}
