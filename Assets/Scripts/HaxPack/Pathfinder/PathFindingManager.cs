using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using DG.Tweening;

/// <summary>
/// 1. 탐색 영역 둘러보기
//2.탐색 시작
//3.경로 채점
//4.계속 탐색하기
//5.A * 알고리즘 요약
/// </summary>
public class PathFindingManager : Singleton<PathFindingManager>
{
    Node[,] matrixNodes;
    public int width => matrixNodes.GetLength(0);
    public int height => matrixNodes.GetLength(1);

    [SerializeField] List<Node> OpenList;
    [SerializeField] List<Node> ClosedList;
    [SerializeField] List<Node> FinalList;

    public Agent AgentNode;

    public Node targetNode;
    public Node CurNode;
    public GameObject Pick;

    public void AgentDrag()
    {
        //플러그 필요
        foreach (var hit in Physics2D.RaycastAll(Util.MousePos, Vector3.forward))
        {
            Node node = hit.collider?.GetComponent<Node>();
            if (node == null)
                continue;

            Pick.transform.localPosition = node.transform.localPosition;
            Pick.transform.localPosition += Vector3.up * 20;
            targetNode = node;
        }
    }

    public void CreateNodeList()
    {
        matrixNodes = TileManager.Instance.tileArray;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                matrixNodes[x, y].SetMatixData(x, y);
            }
        }
        //임시 테스트용으로 SetNodes에서 받지만 실질 사용은 OnClickEvent에서 target을 지정받으면 사용한다
        ClosedList = new List<Node>();
        FinalList = new List<Node>();
        OpenList = new List<Node>();
    }

    public bool StartingSearch()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                matrixNodes[x, y].ScorieClear();
            }
        }

        CurNode = null;
        OpenList.Clear();
        ClosedList.Clear();
        FinalList.Clear();
        OpenList.Add(AgentNode.nowNode);
        while (OpenList.Count > 0)
        {
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].F < CurNode.F) 
                    CurNode = OpenList[i];
                else if (OpenList[i].F == CurNode.F) 
                    CurNode = OpenList[i].H < CurNode.H ? OpenList[i] : CurNode;
            }
                

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            if (CurNode == targetNode)
            {
                if (targetNode.isWall == false)
                    FinalList.Add(CurNode);

                while (CurNode != AgentNode.nowNode)
                {
                    CurNode = CurNode.parent;

                    if (FinalList.Contains(CurNode) == true)
                    {
                        Util.LogError("Final리스트에 이미 존재하는 요소를 추가하려는 시도가 확인되었습니다 로직을 종료합니다");
                    }
                    FinalList.Add(CurNode);

                }
                FinalList.Reverse();
                FinalList.RemoveAt(0);
                OpenList.Clear();

                for (int i = 0; i < FinalList.Count; i++)
                {
                    Util.Log(string.Format("FinalList {0}", FinalList[0]));
                }

                return true;
            }


            //AddOpenList로 통합한다
            //1. SetMyNeightobrs
            //2. PathScoring
            //3. CheckBestNeighbor
            AddOpenList(CurNode.matrixX, CurNode.matrixY);
        }
        return false;
    }
    

    public void Move(bool Path)
    {
        if(Path == false)
        {
            if(targetNode.unit != null)
            {
                //모든 매니저가 물려있다 callback으로 처리해볼지 고민중
                targetNode.unit.Evnet();

                //만든다면??
                // Evnet라는 Mono를 상속받지않는 클래스 등장
                // Event()이런식으로 실행
                // 내부는? 
                // 타겟 유저의 존재여부 확인 및 접근
                // UiManager EventView 리셋 및 접근

            }
            Util.Log(string.Format("Path를 종료"));
            return;
            
        }

        if(FinalList.Count == 0)
        {
            targetNode.unit.Evnet();
            //이웃한 노드를 선택했을시 FinalList는 존재하지않기때문에 문제가 발생한다
            //이경우 바로 이벤트가 이러나게끔한다
            return;
        }
        

        AgentNode.transform.DOMove(FinalList[0].offsetPos, 0.3f, true)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
        {
            AgentNode.nowNode = FinalList[0];
            FinalList.RemoveAt(0);
            bool recive = FinalList.Count > 0 ? true : false;
            Move(recive);
        });
    }

    public Node GetMatrixNode(int x, int y)
    {
        return matrixNodes[x, y];
    }

    public void SetNeighbor(int x, int y)
    {
        if (width > x &&
            height > y &&
            0 <= y &&
            0 <= x)
        {
            Node Neighbor = GetMatrixNode(x, y);

            if ((Neighbor.isWall == true && Neighbor != targetNode )||
                ClosedList.Contains(Neighbor) == true ||
                OpenList.Contains(Neighbor) == true)
                return;

                Neighbor.PathScoring(x, y);
                OpenList.Add(Neighbor);

        }
    }

    public void AddOpenList(int matrixX, int matrixY)
    {
        //메트릭의 존재하는 노드의 '이웃' 초기화한다
        /*######################
             6 1
            5 x 2        
             4 3
         #########################*/

        //Y의 좌표가 홀수인가
        if (matrixY % 2 == 0)
        {
            SetNeighbor(matrixX, matrixY + 1);
            SetNeighbor(matrixX + 1, matrixY);
            SetNeighbor(matrixX, matrixY - 1);
            SetNeighbor(matrixX - 1, matrixY - 1);
            SetNeighbor(matrixX - 1, matrixY);
            SetNeighbor(matrixX - 1, matrixY + 1);
        }
        else
        {
            SetNeighbor(matrixX + 1, matrixY + 1);
            SetNeighbor(matrixX + 1, matrixY);
            SetNeighbor(matrixX + 1, matrixY - 1);
            SetNeighbor(matrixX, matrixY - 1);
            SetNeighbor(matrixX - 1, matrixY);
            SetNeighbor(matrixX, matrixY + 1);
        }
    }


}
