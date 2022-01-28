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

    public Node AgentNode;

    public Node targetNode;

    public Agent player;


    public void SetNodes(Tile[,] tiles)
    {
        matrixNodes = tiles;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                matrixNodes[x, y].SetMatixData(x, y);
            }
        }
        SearchArea();

        //임시 테스트용으로 SetNodes에서 받지만 실질 사용은 OnClickEvent에서 target을 지정받으면 사용한다

        AgentNode = matrixNodes[0, 0];
        targetNode = matrixNodes[7, 5];

        StartingSearch();
    }

    //1. 탑색 영역 둘러보기
    private void SearchArea()
    {
        //메트릭의 존재하는 노드의 '이웃' 초기화한다
        /*######################
             6 1
            5 x 2        
             4 3
         #########################*/

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var node = matrixNodes[x, y];

            }
        }
    }

    public void StartingSearch()
    {
        Node CurNode = null;
        //Start지점을 Add한다
        OpenList = new List<Node>() { AgentNode };
        ClosedList = new List<Node>();
        FinalList = new List<Node>();

        while (OpenList.Count > 0)
        {
            CurNode = OpenList[0];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            if (CurNode == targetNode)
            {
                FinalList.Add(CurNode);
                //버그 발생 parent의 정의가 잘못되었다
                // 1,3 노드와 1,2가 서로 링크드 되어있기에 로직을 빠져나올수없다
                //대처방법
                //1. SetParent 코드 수정
                //2. FinalList Exist 요소값 존재 여부 체크뒤 존재하면 로그를 남겨두로 로직 강제 아웃
                while (CurNode != AgentNode)
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

                return;
            }

            //현제 노드의 이웃을 탐색한다
            //현제 노드가 목적지라면 더이상 이웃을 탐색할 이유가 없다 위 상단에 목적지와 현제노드가 같은지 확인한다

            CurNode.SetMyNeighbors(matrixNodes);

            //현제 노드의 이웃의 스코어를 모두 정의한다
            for (int i = 0; i < CurNode.Neighbors.Count; i++)
            {
                CurNode.Neighbors[i].PathScoring(targetNode);
            }

            //현제 노드의 이웃중 최적의 스코어를 OpenList에 Add한다
            var BestNeighbor = CurNode.Neighbors[0];
            for (int i = 1; i < CurNode.Neighbors.Count; i++)
            {
                if (BestNeighbor.H > CurNode.Neighbors[i].H)
                    BestNeighbor = CurNode.Neighbors[i];
                BestNeighbor.parent = CurNode;
            }
            if (OpenList.Contains(BestNeighbor) == false &&
                ClosedList.Contains(BestNeighbor) == false)
            {
                OpenList.Add(BestNeighbor);
            }
        }
    }

    public IEnumerator Movement()
    {
        while (FinalList.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            if(player == null)
            {
                player = GameObject.Find("Player(Clone)").GetComponent<Agent>();
            }
            player.transform.position = FinalList[0].offsetPos;
            Util.Log(string.Format("Moved Player Pos -> {0}", FinalList[0].name));
            FinalList.RemoveAt(0);
        }
            
    }
}

[System.Serializable]
public class Node : MonoBehaviour
{

    public int matrixX;
    public int matrixY;
    public Node parent;
    public List<Node> Neighbors;

    public Vector3 offsetPos => transform.position + new Vector3(0,8,0);
        

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

    public void SetMyNeighbors(Node[,] list)
    {
        Neighbors = new List<Node>();

        //메트릭의 존재하는 노드의 '이웃' 초기화한다
        /*######################
             6 1
            5 x 2        
             4 3
         #########################*/

        if (matrixY % 2 == 0)
        {
            if (PathFindingManager.Instance.height > matrixY + 1)
            {
                Neighbors.Add(list[matrixX, matrixY + 1]);
            }

            if (PathFindingManager.Instance.width > matrixX + 1)
            {
                Neighbors.Add(list[matrixX + 1, matrixY]);
            }

            if (0 <= matrixY - 1)
            {
                Neighbors.Add(list[matrixX, matrixY - 1]);
            }


            if (0 <= matrixX - 1 &&
                0 <= matrixY - 1)
            {
                Neighbors.Add(list[matrixX - 1, matrixY - 1]);
            }


            if (0 <= matrixX - 1)
            {
                Neighbors.Add(list[matrixX - 1, matrixY]);
            }

            if (0 <= matrixX - 1 &&
                PathFindingManager.Instance.height > matrixY + 1)
            {
                Neighbors.Add(list[matrixX - 1, matrixY + 1]);
            }

        }
        else
        {
            // 조건  list[matrixX + 1]는 indexOutRange 체크
            if (PathFindingManager.Instance.width > matrixX + 1 &&
               PathFindingManager.Instance.height > matrixY + 1)
                Neighbors.Add((list[matrixX + 1, matrixY + 1]));

            if (PathFindingManager.Instance.width > matrixX + 1 &&
               PathFindingManager.Instance.height > matrixY)
                Neighbors.Add((list[matrixX + 1, matrixY]));

            if (PathFindingManager.Instance.width > matrixX + 1 &&
               0 <= matrixY - 1)
                Neighbors.Add((list[matrixX + 1, matrixY - 1]));

            if (0 <= matrixY - 1)
                Neighbors.Add((list[matrixX, matrixY - 1]));

            if (0 <= matrixX - 1)
                Neighbors.Add((list[matrixX - 1, matrixY]));

            if (PathFindingManager.Instance.height > matrixY + 1)
                Neighbors.Add(list[matrixX, matrixY + 1]);
        }
        /*
                foreach (var neighbor in Neighbors) neighbor.parent = this;*/
    }


    public void PathScoring(Node targetTile)
    {
        if (PathFindingManager.Instance.AgentNode == null)
            return;

        H = (targetTile.matrixX - matrixX) + (targetTile.matrixY - matrixY);
    }
}
