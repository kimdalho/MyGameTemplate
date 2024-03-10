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

public abstract class Unit : UiBase
{
    public EntryHud hud;
    /// <summary>
    /// ParentNode는 유닛의 현제 좌표다
    /// </summary>
    public bool onLive;
    public eUnitType status;
    public string unitName;
    public Sprite image;
   

    public virtual void SetData(UnitItem item, bool isFront, Node parent)
    {
        Setup();
        onLive = true;
        status = item.status;
        transform.position = parent.transform.position;
        transform.position += Vector3.up * UnitManager.Offset;
        image = item.render;
        unitName = item.name;
    }

    private void OnMouseDown()
    {
        UiManager.Instance.deskHud.OnClickedUnit(this);
    }


    public virtual Node GetTileOffSetPos()
    {
        Debug.LogError("Not Exist Pos");
        return null;
    }

    public virtual void Dead()
    {
        onLive = false;
        Destroy(this.gameObject);
    }

    public async void A()
    {
        
        await Task.Delay(1000);
    }

    public virtual int Hit(Unit AttackUnit)
    {
        return 0;
    }

    public virtual int GetAttack()
    {
        return 0;
    }

    public virtual int GetMaxHp()
    {
        return 0;
    }

    public virtual int GetCurrentHp()
    {
        return 0;
    }

}
