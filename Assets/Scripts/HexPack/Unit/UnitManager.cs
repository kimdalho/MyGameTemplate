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

//크리쳐랑 타워는 턴인터페이스의 관리가 존재한다 


public class UnitManager : Singleton<UnitManager>, ITurnSystem
{
    public List<Node> CretrueSpawnList;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] UnititemSO bossUnitSO;
    [SerializeField] UnititemSO savagesUnitSO;
    [SerializeField] UnititemSO hidingUnitSO;
    [SerializeField] UnititemSO towerUnitSO;
    [SerializeField] Transform unitHolder;
    [SerializeField] GameMaterialSO gameMaterialSO;

    [Header("Resource")]
    [SerializeField] private GameObject pick;

    public Unit targetUnit;
    public const float Offset =  8.5f;
    public const float playerPosZ = -30f;

    public List<Unit> units;

    public void Setup()
    {
        units = new List<Unit>();
        CretrueSpawnList = new List<Node>();
        CampGenerator();

        foreach(var unit in units)
        {
            if(unit?.GetComponent<Tower>())
            {
                Tower tower = (Tower)unit;
                tower.SetNeighbors();
            }
        }
    }


    public Player CreatePlayer()
    {
        var hexagonGrid = GridManager.Instance.hexagonGrid;
        int playerPosX = hexagonGrid.width  /2;
        int playerPosY = hexagonGrid.hegiht /2;

        var baseNode = GridManager.Instance.GetHaxgonTile(playerPosX,playerPosY);
        Vector2 pos = baseNode.transform.position;
        Vector3 vec = new Vector3(pos.x, pos.y + Offset, playerPosZ);
        var go = Instantiate(playerPrefab, vec, Util.QI);
        Agent agent = go.GetComponent<Agent>();
        go.transform.SetParent(unitHolder);
        agent.Setup(pick);
        agent.nowNode = baseNode;
        PathFindingManager.Instance.agent = agent;
        Debug.Log("Create Player success");
        return go.GetComponent<Player>();
    }

    /// <summary>
    /// 유닛 제너레이터이다
    /// 반드시 배치해야하는곳 ab
    /// 필드내 모든 화산 ,산 ,성
    /// 랜덤으로 배치해야하는곳
    /// 숲, 초원
    /// </summary>
    private void CampGenerator()
    {
        foreach (eCampType enumItem in  Enum.GetValues(typeof(eCampType)))
        {
            CreateTowerByCampeType(enumItem);
        }

        foreach(var unit in units)
        {
            if(true == unit.GetComponent<Tower>())
            {
                CretrueSpawnList.Add(unit.parent);
            }

        }

    }

    /// <summary>
    /// 가져온 타일의 타입에 맞게 처신하여 유닛새성을 돕는다
    /// </summary>
    /// <param name="type"></param>는 유닛의 부모이다
    private void CreateTowerByCampeType(eCampType type)
    {
        var tiles = GridManager.Instance.GetTilesByType(type);
        var typeHolder = new GameObject();
        typeHolder.name = $"{type}Holder";
        typeHolder.transform.SetParent(this.unitHolder);


        foreach (Tile tile in tiles)
        {
            int i = 0;
            Debug.Log($"index {i} , {tile.GeteType()}");
            i++;
            switch (tile.GeteType())
            {
                case eCampType.Mountain:
                case eCampType.Volcano:
                case eCampType.Jungle:
                    CreateCamp(typeHolder.transform, tile, towerUnitSO, true);
                    break;
                case eCampType.Forest:
                case eCampType.PlainsCastle:
                default:
                    break;
            }
        }
        
    }
    /// <summary>
    /// 포악한 타입의 유닛을 생성한다
    /// 유닛의 좌표는 parent가 된다
    /// </summary>

    private void CreateCamp(Transform parent, Tile _tile , UnititemSO so ,bool isFront)
    {
        var go = Instantiate(unitPrefab);
        Tower tower = go.AddComponent<Tower>();
        tower.location = _tile;
        Unit NewUnit = go.GetComponent<Unit>();
        int rnd = Random.Range(0, so.items.Length);
        NewUnit.SetData(so.items[rnd], isFront, _tile);
        NewUnit.transform.SetParent(parent);
        units.Add(NewUnit);
       
    }

    public void CreateCreture(Node neighbor,UnitItem model)
    {
        if (model == null)
        {
            Debug.LogError($"생성 몬스터 모델 데이터가 잘못되었다");
            return;
        }

        var go = Instantiate(unitPrefab);
        go.AddComponent<Creature>();
        Unit NewUnit = go.GetComponent<Unit>();
        NewUnit.SetData(model, true, neighbor);
    }

    public void TryAttack(Unit unit)
    {
        if(null == unit)
        {
            Debug.LogError("there is not unit at tile");
            return;
        }

        StartCoroutine(CoActiveUnit(unit));
    }

    private IEnumerator CoActiveUnit(Unit unit)
    {
        targetUnit = unit;
        yield return StartCoroutine(GameManager.Instance.CoBattle(targetUnit));
    }

    public GameMaterial GetGameMaterial(int materialId)
    {
        return gameMaterialSO.models[materialId];
    }



    public void TargetUnitRelese()
    {
        if(targetUnit == null)
        {
            Util.Log("잘못된 접근 시도");
            return;
        }

        targetUnit.parent.isWall = false;
        //targetUnit.End();
        targetUnit = null;

    }

    public void EndPlayerMove()
    {
        throw new System.NotImplementedException();
    }

    public void StartPlayerTurn()
    {

    }

    public void CreateRandomMonster()
    {

    }


    void ITurnSystem.TurnAwake()
    {
        //연public void 
    }

    public UnitItem GetCreture(int id)
    {
        foreach (var monster in savagesUnitSO.items)
        {
            if (id == monster.id)
                return monster;
        }

        Debug.LogError("NotFound  -> GetCreture Monster id{id}");
        return null;
      
    }
}

