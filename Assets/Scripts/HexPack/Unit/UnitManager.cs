using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// 유닛을 관리하는 클래스이다.
/// </summary>
///

//SO 타입은 UnitDataBase 클래스로 분리


public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] UnititemSO bossUnitSO;
    [SerializeField] UnititemSO savagesUnitSO;
    [SerializeField] UnititemSO hidingUnitSO;
    [SerializeField] UnititemSO towerUnitSO;
    [SerializeField] Transform unitHolder;

    public Unit targetUnit;
    public Sprite hideSprite;
    public const float Offset =  8.5f;
    public const float playerPosZ = -30f;

    public void CreatePlayer(Transform parent)
    {
        var hexagonGrid = GridManager.Instance.hexagonGrid;
        int playerPosX = hexagonGrid.width  /2;
        int playerPosY = hexagonGrid.hegiht /2;

        var baseNode = GridManager.Instance.GetHaxgonTile(playerPosX,playerPosY);
        Vector2 pos = baseNode.transform.position;
        Vector3 vec = new Vector3(pos.x, pos.y + Offset, playerPosZ);
        var go = Instantiate(playerPrefab, vec, Util.QI);
        Agent agent = go.GetComponent<Agent>();
        go.transform.SetParent(parent);
    
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
        foreach (TileItem.eCampType enumItem in  Enum.GetValues(typeof(TileItem.eCampType)))
        {
            switch(enumItem)
            {
                //해당 타입은 모두 반드시 유닛이 있어야한다.
                case TileItem.eCampType.Mountain:
                case TileItem.eCampType.Volcano:
                //case TileItem.eCampType.PlainsCastle:
                case TileItem.eCampType.Plains:
                    Unitcomport(enumItem);
                    break;
            }
        }
    }


    /// <summary>
    /// 가져온 타일의 타입에 맞게 처신하여 유닛새성을 돕는다
    /// </summary>
    /// <param name="type"></param>는 유닛의 부모이다
    private void Unitcomport(TileItem.eCampType type)
    {
        var tiles = GridManager.Instance.GetTilesToType(type);
        var typeHolder =  new GameObject();
        typeHolder.name = $"{type}Holder";
        typeHolder.transform.SetParent(this.unitHolder);


        foreach (Tile tile in tiles)
        {
            switch (tile.GeteType())
            {
                case TileItem.eCampType.Mountain:
                    CreateCamp<Creature>(typeHolder.transform,tile, savagesUnitSO , true);
                    break;
                case TileItem.eCampType.Volcano:
                    Debug.Log("x");
                    CreateCamp<Creature>(typeHolder.transform, tile, bossUnitSO, true);
                    break;
                case TileItem.eCampType.Forest:
                case TileItem.eCampType.PlainsCastle:
                    CreateCamp<Creature>(typeHolder.transform, tile, hidingUnitSO, false);
                    break;
                case TileItem.eCampType.Plains:
                    CreateCamp<Tower>(typeHolder.transform, tile, towerUnitSO , true);
                    break;
            }
        }
        
    }
    /// <summary>
    /// 포악한 타입의 유닛을 생성한다
    /// 유닛의 좌표는 parent가 된다
    /// </summary>

    private void CreateCamp<T>(Transform parent, Tile _tile , UnititemSO so ,bool isFront) where T : Unit
    {
        var go = Instantiate(unitPrefab);
        go.AddComponent<T>();
        Unit unit = go.GetComponent<Unit>();
        int rnd = Random.Range(0, so.items.Length);
        unit.SetData(so.items[rnd], isFront, _tile);
        unit.transform.SetParent(parent);
    }

    public void ActiveUnit(Unit unit)
    {
        targetUnit = unit;
        Debug.Log($"Action {unit}");

        //퀘스트 매니저 필요없을수도 있
        //QuestManager.Instance.CreateQuest(targetUnit.item.questType);
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

