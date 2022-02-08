using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using TMPro;
/// <summary>
/// 필드에 존재하는 모든 객체이다
/// </summary>
/// 1. 외부에서 호출하는 함수는 존재하면 안된다
/// 2. 외부에서는 단지 PRS 또는 생성에대한 접근을한다
/// 3. 유닛이 실행해야하는 기능은 UnitManager에서 호출한다

public class Unit : UiBase
{
   
    //이는 정체가 밝혀진 상태이다 유닛의 정보를 보여준다.
    public Sprite unitSprtie;

    /// <summary>
    /// ParentNode는 유닛의 현제 좌표다
    /// </summary>
    public Node parent;

    /// <summary>
    /// Get만하여 사용
    /// </summary>
    public UnitItem item;


    enum eSprites
    {
        iconRender  = 0,
        CircleMask = 1,
        CircleLine = 2,
        CircleAtk = 3,
        CircleHealth = 4,
    }

    enum eTMPs
    {
        AtkTMP = 0,
        HealthTMP = 0,
    }

    public override void Setup()
    {
        Bind<SpriteRenderer>(typeof(eSprites));
        Bind<TMP_Text>(typeof(eTMPs));
    }

    public void SetData(UnitItem item, bool isFront, Node parent)
    {
        Setup();
        //깊은 커플링
        this.item = item;
        this.parent = parent;
        this.parent.unit = this;
        unitSprtie = this.item.render;
        transform.position = parent.transform.position;
        transform.position += Vector3.up * UnitManager.Offset;
        Get<SpriteRenderer>(0).sprite = isFront ? unitSprtie : UnitManager.Instance.hideSprite;
        this.parent.isWall = true;
    }

    public virtual void Evnet()
    {
        BattleManager.Instance.RequsetBattle(this);
    }

}
