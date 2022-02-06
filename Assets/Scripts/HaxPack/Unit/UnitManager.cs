using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using System;
using Random = UnityEngine.Random;
/// <summary>
/// 유닛을 관리하는 클래스이다.
/// </summary>
public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] UnititemSO unitItemSO;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject unitPrefab;

    [SerializeField] UnititemSO savagesUnitBuffer;
    [SerializeField] UnititemSO hidingUnitBuffer;

    public static float Offset =  8.5f;

    public void CreatePlayer(Vector3 pos)
    {
        Vector3 vec = new Vector3(pos.x, pos.y + Offset, 0f);
        var go = Instantiate(playerPrefab, vec, Util.QI);
    }

    /// <summary>
    /// 유닛 제너레이터이다
    /// 반드시 배치해야하는곳
    /// 필드내 모든 화산 ,산 ,성
    /// 랜덤으로 배치해야하는곳
    /// 숲, 초원
    /// </summary>
    public void UnitGenerator()
    {
        foreach(TileItem.eType enumItem in  Enum.GetValues(typeof(TileItem.eType)))
        {
            switch(enumItem)
            {
                //해당 타입은 모두 반드시 유닛이 있어야한다.
                case TileItem.eType.Mountain:
                case TileItem.eType.Volcano:
                case TileItem.eType.Plains:
                case TileItem.eType.PlainsCastle:
                    Unitcomport(enumItem);
                    break;

            }
        }
    }
    /// <summary>
    /// 가져온 타일의 타입에 맞게 처신하여 유닛새성을 돕는다
    /// </summary>
    /// <param name="type"></param>는 유닛의 부모이다
    private void Unitcomport(TileItem.eType type)
    {
        var tiles = TileManager.Instance.GetTiles(type);

        
        foreach(Tile tile in tiles)
        {
            switch (tile.GeteType())
            {
                //사나움
                case TileItem.eType.Mountain:
                case TileItem.eType.Volcano:
                    CreateSavagesUnit(tile);
                    break;
                //숨겨진
                case TileItem.eType.Forest:
                case TileItem.eType.Plains:
                case TileItem.eType.PlainsCastle:
                    CreateHidingUnit(tile);
                    break;

            }
        }
        
    }
    /// <summary>
    /// 포악한 타입의 유닛을 생성한다
    /// 유닛의 좌표는 parent가 된다
    /// </summary>
    private void CreateSavagesUnit(Tile parent)
    {
        Unit unit = Instantiate(unitPrefab).GetComponent<Unit>();
        int rnd = Random.Range(0, savagesUnitBuffer.items.Length);
        unit.Setup(savagesUnitBuffer.items[rnd],true, parent);
    }
    
    private void CreateHidingUnit(Tile parent)
    {
        Unit unit = Instantiate(unitPrefab).GetComponent<Unit>();
        int rnd =   Random.Range(0, hidingUnitBuffer.items.Length);
        unit.Setup(hidingUnitBuffer.items[rnd],false, parent);
    }
}

