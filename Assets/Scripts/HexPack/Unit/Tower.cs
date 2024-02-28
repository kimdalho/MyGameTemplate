using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using DG.Tweening;
public class Tower :Unit,ITurnSystem
{
    public List<Node> myNeighbors;
    public Node location;
    private int matrixY => location.matrixY;
    private int matrixX => location.matrixX;
    private readonly int OFFSET_LEFT = -1;
    private readonly int OFFSET_RIGHT = 1;
    private UnitHud tower;

    private void Awake()
    {
        myNeighbors = new List<Node>();
        GameManager.Instance.PlayerMoveEnd += EndPlayerMove;
        tower = GetComponent<UnitHud>();  
        tower.gauge.spawn += () => {

            foreach (var data in myNeighbors)
            {
                if (data.isWall == true)
                    continue;

                UnitManager.Instance.CreateCreture(data);
                break;
            }


        };

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
        StartCoroutine(CoUpdateSpawnData());
    }

    private IEnumerator CoUpdateSpawnData()
    {
        var data = GetComponent<UnitHud>().gauge;
        data.gameObject.SetActive(true);
        data.fild_Fade.gameObject.SetActive(true);
        data.fild_Fade.FadeOut(() => { });
        data.fade.FadeOut(() => { });


            
        yield return new WaitForSeconds(1f);

       
        float deltime = 0;
        while (deltime < 1)
        {
            deltime += Time.deltaTime;
            data.curGage += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        data.fade.FadeIn();
        data.fild_Fade.FadeIn();
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
