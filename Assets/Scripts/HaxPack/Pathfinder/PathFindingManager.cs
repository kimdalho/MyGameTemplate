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
    List<Node> Neighbors = new List<Node>();
    public int width => matrixNodes.GetLength(0);
    public int height => matrixNodes.GetLength(1);

    [SerializeField] List<Node> OpenList;
    [SerializeField] List<Node> ClosedList;
    [SerializeField] List<Node> FinalList;

    public Agent AgentNode;

    public Node targetNode;

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
        Node CurNode = null;
        //Start지점을 Add한다
        OpenList.Clear();
        OpenList.Add(AgentNode.nowNode);
        ClosedList.Clear();
        FinalList.Clear();
        while (OpenList.Count > 0)
        {
            CurNode = OpenList[0];

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
            Neighbors.Clear();
            var BestNeighbor = GetBestNeighbor(CurNode.matrixX, CurNode.matrixY);

            //경로를 만들 이웃을 찾지못한다면 목적지까지 이동가능한 경로가 없는것으로 확인 거짓반환
            if (BestNeighbor == null)
                return false;


            BestNeighbor.parent = CurNode;

            if (OpenList.Contains(BestNeighbor) == false &&
                ClosedList.Contains(BestNeighbor) == false)
            {
                OpenList.Add(BestNeighbor);
            }

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
            Node CurNode = GetMatrixNode(x, y);

            if ((CurNode.isWall == true && 
                CurNode != targetNode )||
                ClosedList.Contains(CurNode) == true)
                return;

                Neighbors.Add(CurNode);

        }
    }

    public Node GetBestNeighbor(int matrixX, int matrixY)
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

        for (int i = 0; i < Neighbors.Count; i++)
        {
            Neighbors[i].PathScoring();
        }

        Node BestNeighbor = null;
        //현제 노드의 이웃중 최적의 스코어를 OpenList에 Add한다
        //Open에 넣을수있는 이웃이 하나도 존재하지않는다면 Neighbors는 비어있을수있다.
        if (Neighbors.Count > 0)
        {
             BestNeighbor = Neighbors[0];

            for (int i = 1; i < Neighbors.Count; i++)
            {
                if (BestNeighbor.H > Neighbors[i].H)
                    BestNeighbor = Neighbors[i];
            }
        }
        return BestNeighbor;
    }
}

[System.Serializable]
public class Node : MonoBehaviour
{

    public int matrixX;
    public int matrixY;
    public Node parent;
    public Unit unit;
    /// <summary>
    /// 유닛이 배치된 타일은 isWall로 표시된다
    /// 단 배치된 유닛이 파괴되면 Wall은 거짓으로 전환된다
    /// </summary>
    public bool isWall = false;
    public Vector3 offsetPos => transform.position + new Vector3(0, 8, 0);


    //시작점 부터 현재 경로 비용
    //G의 사용의 이유는 사각형 그리드에서 생기는
    //사선과 직선 이동비용 두 종류의 이웃 간의 차이는 합병증의 보안을 위해 존재한다
    //그렇다면 육각타일의 경우 이를 보안해야할이유가 존재하는가? 휴리스틱스의 값이 바로 이동에 필요한 최소한의 행동비용 아닌가?
    // 휴리스틱스 현재 사각형에서 목적지 비용 (Manhattan) 맨하탄 추정 방식 적용
    public int H;

    public void SetMatixData(int x, int y)
    {
        this.matrixX = x;
        this.matrixY = y;
    }


    //코드 불필요
    //1. 이웃의 존재는 최종FinalList를 만들어내기위한 수단에 불가하다.
    //2. 지속적으로 가지고있어야할 이유가 없다.
    //3. SetNeighbors 보다 AddOpenList라는 함수가 코드 정리 및 알고리즘의 효율을 높힐수있을듯하다.

    public void PathScoring()
    {
        var targetTile = PathFindingManager.Instance.targetNode;
        if (PathFindingManager.Instance.AgentNode == null)
            return;
        //목표지점 X - 
        H = Mathf.Abs((targetTile.matrixX - matrixX)) + Mathf.Abs((targetTile.matrixY - matrixY));
    }
}
