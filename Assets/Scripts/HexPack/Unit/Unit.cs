using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using TMPro;
using System;
using System.Threading.Tasks;
/// <summary>
/// 필드에 존재하는 모든 객체이다
/// </summary>
/// 1. 외부에서 호출하는 함수는 존재하면 안된다
/// 2. 외부에서는 단지 PRS 또는 생성에대한 접근을한다
/// 3. 유닛이 실행해야하는 기능은 UnitManager에서 호출한다

public class Unit : UiBase
{
    public UnitHud hud;
    /// <summary>
    /// ParentNode는 유닛의 현제 좌표다
    /// </summary>
    public Node parent;

    /// <summary>
    /// Get만하여 사용
    /// </summary>
    public UnitStat stat;
    public eCretureType crtureType;
    public eTowerType towerType;
    public bool onLive;
    public eUnitType status;
    protected List<int> cretureList;
    public virtual void SetData(UnitItem item, bool isFront, Node parent)
    {
        Setup();
        onLive = true;
        stat = new UnitStat();
        var model = item.baseStat;
        stat.curHp = model.curHp;
        stat.maxHp = model.maxHp;
        stat.atk = model.atk;
        stat.move = model.move;
        stat.fullness = model.fullness;
        stat.maxHp = model.maxHp;
        status = item.status;
        this.parent = parent;
        this.parent.unit = this;

        this.crtureType = item.cretureType;
        this.towerType = item.towerType;

        transform.position = parent.transform.position;
        transform.position += Vector3.up * UnitManager.Offset;
        this.parent.isWall = true;
        this.cretureList = item.cretureUnits;
        hud = GetComponent<UnitHud>();
        hud.Draw(item,stat); 
    }

    public Node GetTileOffSetPos()
    {
        return parent;
    }

    public virtual void Dead()
    {
        onLive = false;
        parent.isWall = false;
        Destroy(this.gameObject);
    }

    public async void A()
    {
        await Task.Delay(1000);
    }

    public virtual int Hit(Unit AttackUnit)
    {
        stat.curHp = Math.Clamp(stat.curHp - AttackUnit.GetAttack(), 0, stat.maxHp);
        return stat.curHp;
    }

    public virtual int GetAttack()
    {
        return stat.curHp;
    }

    public virtual int GetCurrentHp()
    {
        return stat.curHp;
    }

}
