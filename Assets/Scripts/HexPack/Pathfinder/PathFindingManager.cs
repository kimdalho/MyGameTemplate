using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

//기능 개발 이유
//유닛이 육각타일에서의 규칙 이동을 구현하기위해 Unity Navigation을 사용하지않고 직접 A*를 구현했습니다.


//<summary>
//1.탐색 영역 둘러보기
//2.탐색 시작
//3.경로 채점
//4.계속 탐색하기
//5.만들어진 Final Path는 DotTween과 코루틴를 활용해이동한다
/// </summary>
public class PathFindingManager : Singleton<PathFindingManager>
{
    private readonly int OFFSET_LEFT = -1;
    private readonly int OFFSET_RIGHT = 1;
    private readonly float MOVING_DURATION = 0.3f;
    private readonly float MOVING_SLOW_DURATION = 0.6f;

    private Node[,] matrixNodes;
    public int widthSize => matrixNodes.GetLength(0);
    public int heightSize => matrixNodes.GetLength(1);

    //인스펙터에서 디버깅하기위해 직렬화
    [Header("PathFind System")]
    [SerializeField] private List<Node> OpenList;
    [SerializeField] private List<Node> ClosedList;
    [SerializeField] private List<Node> FinalList;


    public Agent agent { get; set; }
    public Node targetNode { get; set; }
    public Node curNode { get; private set; }
    
     
    private void SetNeighbor(int x, int y)
    {
        if (widthSize > x &&
            heightSize > y &&
            0 <= y &&
            0 <= x)
        {
            Node Neighbor = GetMatrixNode(x, y);

            if ((Neighbor.isWall == true
                && Neighbor != targetNode) ||
                ClosedList.Contains(Neighbor) == true ||
                OpenList.Contains(Neighbor) == true)
                return;

            Neighbor.PathScoring(x, y);
            OpenList.Add(Neighbor);

        }
    }

    private void AddOpenList(int matrixX, int matrixY)
    {
        //매트릭스 탐색순서
        //메트릭스의 존재하는 노드의 '이웃' 초기화한다
        /*######################
             6 1
            5 x 2        
             4 3
         #########################*/

        //Y의 좌표가 홀수인가
        //육각타일인 매트릭스는 홀수이냐 정수이냐에 따라서 오프셋이 존재한다.
        if (matrixY % 2 == 0)
        {
            SetNeighbor(matrixX, matrixY + OFFSET_RIGHT);
            SetNeighbor(matrixX + OFFSET_RIGHT, matrixY);
            SetNeighbor(matrixX, matrixY + OFFSET_LEFT);
            SetNeighbor(matrixX + OFFSET_LEFT, matrixY + OFFSET_LEFT);
            SetNeighbor(matrixX + OFFSET_LEFT, matrixY);
            SetNeighbor(matrixX + OFFSET_LEFT, matrixY + OFFSET_RIGHT);
        }
        else
        {
            SetNeighbor(matrixX + OFFSET_RIGHT, matrixY + OFFSET_RIGHT);
            SetNeighbor(matrixX + OFFSET_RIGHT, matrixY);
            SetNeighbor(matrixX + OFFSET_RIGHT, matrixY + OFFSET_LEFT);
            SetNeighbor(matrixX, matrixY + OFFSET_LEFT);
            SetNeighbor(matrixX + OFFSET_LEFT, matrixY);
            SetNeighbor(matrixX, matrixY + OFFSET_RIGHT);
        }
    }

    

    public void Move(bool there_is_move)
    {
        if (there_is_move == false)
        {
            if (targetNode.unit != null && targetNode.unit.onLive)
            {
                UnitManager.Instance.ActiveUnit(targetNode.unit);
            }
            else
            {
                //Finish Move
                GameManager.Instance.SetStatus(eTurnType.PlayerMoveEnd);
            }
            return;

        }

        if (FinalList.Count == 0)
        {
            UnitManager.Instance.ActiveUnit(targetNode.unit);
            return;
        }


        agent.transform.DOMove(FinalList[0].offsetPos, MOVING_DURATION, true)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                agent.nowNode.isWall = false;
                agent.nowNode = FinalList[0];
                agent.nowNode.isWall = true;
                FinalList.RemoveAt(0);
                bool recive = FinalList.Count > 0 ? true : false;
                Move(recive);
            });
    }

    public void BattleEndMove(Node lastNode)
    {
            agent.transform.DOMove(lastNode.offsetPos, MOVING_SLOW_DURATION, true)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
        {
            agent.nowNode.isWall = false;
            agent.nowNode = lastNode;
            agent.nowNode.isWall = true;
            GameManager.Instance.SetStatus(eTurnType.PlayerMoveEnd);
        });
    }


    public void CreateNodeList()
    {
        matrixNodes = GridManager.Instance.GetHexagonArray();
        for (int y = 0; y < heightSize; y++)
        {
            for (int x = 0; x < widthSize; x++)
            {
                matrixNodes[x, y].SetMatixData(x, y);
            }
        }
        ClosedList = new List<Node>();
        FinalList = new List<Node>();
        OpenList = new List<Node>();
    }

    public bool PathFinding()
    {
        if (targetNode == agent.nowNode)
            return false;

        for (int y = 0; y < heightSize; y++)
        {
            for (int x = 0; x < widthSize; x++)
            {
                matrixNodes[x, y].ScorieClear();
            }
        }

        curNode = null;
        OpenList.Clear();
        ClosedList.Clear();
        FinalList.Clear();
        OpenList.Add(agent.nowNode);
        while (OpenList.Count > 0)
        {
            curNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].F < curNode.F) 
                    curNode = OpenList[i];
                else if (OpenList[i].F == curNode.F) 
                    curNode = OpenList[i].H < curNode.H ? OpenList[i] : curNode;
            }
                

            OpenList.Remove(curNode);
            ClosedList.Add(curNode);

            if (curNode == targetNode)
            {
                if (targetNode.isWall == false)
                    FinalList.Add(curNode);

                while (curNode != agent.nowNode)
                {
                    curNode = curNode.parent;

                    if (FinalList.Contains(curNode) == true)
                    {
                        Util.LogError("Final리스트에 이미 존재하는 요소를 추가하려는 시도가 확인되었습니다 로직을 종료합니다");
                    }
                    FinalList.Add(curNode);

                }
                FinalList.Reverse();
                FinalList.RemoveAt(0);
                OpenList.Clear();

                for (int i = 0; i < FinalList.Count; i++)
                {
                    Util.Log(string.Format("FinalList {0}", FinalList[0]));
                }
                Agent.onDrag = false;
                return true;
            }


            //AddOpenList로 통합한다
            //1. SetMyNeightobrs
            //2. PathScoring
            //3. CheckBestNeighbor
            AddOpenList(curNode.matrixX, curNode.matrixY);
        }
        return false;
    }

    public Node GetMatrixNode(int x, int y)
    {
        return matrixNodes[x, y];
    }
   
}
