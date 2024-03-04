using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower :Unit,ITurnSystem
{
    public List<Node> myNeighbors;
    public Node location;
    private int matrixY => location.matrixY;
    private int matrixX => location.matrixX;
    private readonly int OFFSET_LEFT = -1;
    private readonly int OFFSET_RIGHT = 1;
    private UnitHud tower;

    [SerializeField]
    private List<UnitItem> spawnList = new List<UnitItem>();
    [SerializeField]
    private UnitItem targetModel;
    public bool isspawn;
    private void Awake()
    {
        myNeighbors = new List<Node>();
        GameManager.Instance.PlayerMoveEnd += EndPlayerMove;
        tower = GetComponent<UnitHud>();  
        tower.gauge.spawn += () => {

            if(spawnList.Count > 0)
            {
                foreach (var position in myNeighbors)
                {
                    if (position.isWall == true)
                        continue;

                    UnitManager.Instance.CreateCreture(position, targetModel);
                    isspawn = false;
                    break;
                }
            }

        };

    }
    public override void SetData(UnitItem item, bool isFront, Node parent)
    {
        base.SetData(item, isFront, parent);
        isspawn = false;
        foreach (int MonsterId in cretureList)
        {
           var data = UnitManager.Instance.GetCreture(MonsterId);
           spawnList.Add(data);
        }

    }



    public void SetNeighbors()
    {
        if (matrixY % 2 == 0)
        {
            AddNeighbor(matrixX, matrixY + OFFSET_RIGHT);
            AddNeighbor(matrixX + OFFSET_RIGHT, matrixY);
            AddNeighbor(matrixX, matrixY + OFFSET_LEFT);
            AddNeighbor(matrixX + OFFSET_LEFT, matrixY + OFFSET_LEFT);
            AddNeighbor(matrixX + OFFSET_LEFT, matrixY);
            AddNeighbor(matrixX + OFFSET_LEFT, matrixY + OFFSET_RIGHT);
        }
        else
        {
            AddNeighbor(matrixX + OFFSET_RIGHT, matrixY + OFFSET_RIGHT);
            AddNeighbor(matrixX + OFFSET_RIGHT, matrixY);
            AddNeighbor(matrixX + OFFSET_RIGHT, matrixY + OFFSET_LEFT);
            AddNeighbor(matrixX, matrixY + OFFSET_LEFT);
            AddNeighbor(matrixX + OFFSET_LEFT, matrixY);
            AddNeighbor(matrixX, matrixY + OFFSET_RIGHT);
        }

       
    }

    private void AddNeighbor(int x, int y)
    {
        var PathMgr = PathFindingManager.Instance;

        if (PathMgr.widthSize > x &&
           PathMgr.heightSize > y &&
           0 <= y &&
           0 <= x)
        {
            Node Neighbor = PathMgr.GetMatrixNode(x, y);
            if (true == Neighbor.isWall)
                return;

            myNeighbors.Add(Neighbor);

        }
    }

    
    public void EndPlayerMove()
    {
        if(isspawn == false)
        {
            isspawn = true;

            if (spawnList.Count <= 0)
                return;

            int rnd = Random.Range(0, spawnList.Count);
            targetModel = UnitManager.Instance.GetCreture(spawnList[rnd].id);
            if(targetModel.spawn <= 0)
            {
                Debug.LogError("몬스터 생성 스코어가 잘못되었다 => EndPlayerMove() => targetModel.spawn is zero");
                return;
            }
            tower.gauge.SetMax(targetModel.spawn);
            tower.gauge.SetDraw(true);
        }

        StartCoroutine(CoUpdateSpawnData());
    }

    private IEnumerator CoUpdateSpawnData()
    {
        var slider = tower.gauge;
        slider.SliderFadeOut();


            
        yield return new WaitForSeconds(1f);

       
        float deltime = 0;
        while (deltime < 1)
        {
            deltime += Time.deltaTime;
            slider.curGage += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        slider.SliderFadeIn();
    }


    public void StartPlayerTurn()
    {
        throw new System.NotImplementedException();
    }

    public void TurnAwake()
    {
        throw new System.NotImplementedException();
    }
}
