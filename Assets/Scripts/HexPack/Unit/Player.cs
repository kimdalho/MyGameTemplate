using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Unit
{
    public PlayerState playerStat;

    public List<IPlayerHit> iplayerhit = new List<IPlayerHit>();

    private void Awake()
    {
        playerStat.Setup();
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
        PathFindingManager.Instance.agent.nowNode.isWall = false;
        Debug.Log("GameOver");
    }

}
