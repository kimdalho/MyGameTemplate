using UnityEngine;

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

    /// <summary>
    ///  G - 시작점 A로부터 현재 사각형까지의 경로를 따라 이동하는데 소요되는 비용입니다.
    /// </summary>
    public int G;
    /// <summary>
    /// H - 현재 사각형에서 목적지 B까지의 예상 이동 비용입니다.사이에 벽, 물 등으로 인해 실제 거리는 알지 못합니다.그들을 무시하고 예상 거리를 산출합니다. 여러 방법이 있지만, 이 포스팅에서는 대각선 이동을 생각하지 않고, 가로 또는 세로로 이동하는 비용만 계산합니다.
    /// </summary>
    public int H;

    /// <summary>
    ///     F - 현재까지 이동하는데 걸린 비용과 예상 비용을 합친 총 비용입니다.
    /// </summary>
    public int F;


    public void SetMatixData(int x, int y)
    {
        this.matrixX = x;
        this.matrixY = y;
    }


    //코드 불필요
    //1. 이웃의 존재는 최종FinalList를 만들어내기위한 수단에 불가하다.
    //2. 지속적으로 가지고있어야할 이유가 없다.
    //3. SetNeighbors 보다 AddOpenList라는 함수가 코드 정리 및 알고리즘의 효율을 높힐수있을듯하다.

    public void PathScoring(int curX, int curY)
    {
        var curNode = PathFindingManager.Instance.GetMatrixNode(curX, curY);
        var targetNode = PathFindingManager.Instance.targetNode;

        if (PathFindingManager.Instance.AgentNode == null)
            return;
        G = curNode.G + 10;
        H = Mathf.Abs((targetNode.matrixX - matrixX)) + Mathf.Abs((targetNode.matrixY - matrixY));
        H *= 10;
        F = G + H;
        this.parent = PathFindingManager.Instance.CurNode;
    }

    public void ScorieClear()
    {
        G = 0;
        H = 0;
        F = 0;
    }
}
