using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
/// <summary>
/// 필드에 존재하는 모든 객체이다
/// </summary>
/// 1. 외부에서 호출하는 함수는 존재하면 안된다
/// 2. 외부에서는 단지 PRS 또는 생성에대한 접근을한다
/// 3. 유닛이 실행해야하는 기능은 UnitManager에서 호출한다

public class Unit : MonoBehaviour
{
    //이는 뒷면과 같다 유닛의 정체를 밝히지않기위해 존재한다.
    public Sprite hideSprite;
    //이는 정체가 밝혀진 상태이다 유닛의 정보를 보여준다.
    public Sprite unitSprtie;
    public SpriteRenderer render;

    /// <summary>
    /// ParentNode는 유닛의 현제 좌표다
    /// </summary>
    public Node parent;

    /// <summary>
    /// Get만하여 사용
    /// </summary>
    public UnitItem item;


    public void Setup(UnitItem item, bool isFront, Node parent)
    {
        this.item = item;
        transform.position = parent.transform.position;
        transform.position = Vector3.up * UnitManager.Offset;
    }

}
