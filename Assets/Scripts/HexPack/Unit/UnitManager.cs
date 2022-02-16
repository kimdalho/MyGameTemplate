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
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject unitPrefab;

    [SerializeField] UnititemSO bossUnitSO;
    [SerializeField] UnititemSO savagesUnitSO;
    [SerializeField] UnititemSO hidingUnitSO;
    [SerializeField] UnititemSO towerUnitSO;

    public Unit targetUnit;

    public Sprite hideSprite;

    public static float Offset =  8.5f;

    public void CreatePlayer()
    {
        var baseNode = GridManager.Instance.GetHaxgonTile(0,0);
        Vector2 pos = baseNode.transform.position;
        Vector3 vec = new Vector3(pos.x, pos.y + Offset, 0f);
        var go = Instantiate(playerPrefab, vec, Util.QI);
        Agent agent = go.GetComponent<Agent>();
        agent.nowNode = baseNode;
        PathFindingManager.Instance.Agent = agent;
    }

    /// <summary>
    /// 유닛 제너레이터이다
    /// 반드시 배치해야하는곳 ab
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
                case TileItem.eType.PlainsCastle:
                case TileItem.eType.Plains:
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
        var tiles = GridManager.Instance.GetHexagonTiles(type);

        
        foreach(Tile tile in tiles)
        {
            switch (tile.GeteType())
            {
                case TileItem.eType.Mountain:
                    UnitCreator<Creature>(tile, savagesUnitSO , true);
                    break;
                case TileItem.eType.Volcano:
                    UnitCreator<Creature>(tile, bossUnitSO, true);
                    break;
                case TileItem.eType.Forest:
                case TileItem.eType.PlainsCastle:
                    UnitCreator<Creature>(tile, hidingUnitSO, false);
                    break;
                case TileItem.eType.Plains:
                    UnitCreator<Tower>(tile, towerUnitSO , true);
                    break;

            }
        }
        
    }
    /// <summary>
    /// 포악한 타입의 유닛을 생성한다
    /// 유닛의 좌표는 parent가 된다
    /// </summary>

    private void UnitCreator<T>(Tile parent , UnititemSO so ,bool isFront) where T : Unit
    {
        var go = Instantiate(unitPrefab);
        go.AddComponent<T>();
        Unit unit = go.GetComponent<Unit>();

        int rnd = Random.Range(0, so.items.Length);
        unit.SetData(so.items[rnd], isFront, parent);
        
    }

    public void SetTargetUnit(Unit unit)
    {
        targetUnit = unit;

        QuestManager.Instance.CreateQuest(targetUnit.item.questType);
    }

    public void TargetUnitRelese()
    {
        if(targetUnit == null)
        {
            Util.Log("잘못된 접근 시도");
            return;
        }

        targetUnit.parent.isWall = false;
        targetUnit.End();
        targetUnit = null;

    }
}

