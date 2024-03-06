using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using System;
using Random = UnityEngine.Random;

public abstract class BaseGrid
{ 
    public Tile tilePrefab;
    public Transform parent;
    public Tile[,] array;
    public int width;
    public int hegiht;

    /// <summary>
    /// 값은 현제 요소값 key타입 타일의 캡프를 생성할 갯수를 의미합니.
    /// </summary>
    protected Dictionary<eCampType, int> request = new Dictionary<eCampType, int>();
    [Header("생성 타일 갯수")]
    [SerializeField]
    private int Plains;
    [SerializeField]
    private int Mountain;
    [SerializeField]
    private int Desert;
    [SerializeField]
    private int Jungle;
    [SerializeField]
    private int Ocean;
    [SerializeField]
    private int ForestSnow;
    



    public void CreateGird()
    {

        array = new Tile[width, hegiht];
        foreach (eCampType campType in Enum.GetValues(typeof(eCampType)))
        {
            request.Add(campType, 0);
        }
        request[eCampType.PlainsCastle] = 1;
        request[eCampType.Volcano] = 1;
        request[eCampType.Plains] = Plains;
        request[eCampType.Mountain] = Mountain;
        request[eCampType.Jungle] = Jungle;
        request[eCampType.Ocean] = Ocean;
        request[eCampType.ForestSnow] = ForestSnow;
        request[eCampType.Desert] = Desert;



        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                array[x, y] = CreateTileCell(x, y);
            }
        }
    }

    // 육각형
    public virtual Tile CreateTileCell(int x, int y)
    {
        Vector3 position;

        position.x = x * (HexMetrics.innerRadius * 2f);
        position.y = y * (HexMetrics.outerRadius * 1.5f); ;
        position.z = 0f;

        position.x = (x + y * 0.5f) * (HexMetrics.innerRadius * 2f);
        position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);

        Tile cell = GridManager.Instance.CreateCell(tilePrefab);

        cell.transform.SetParent(parent, false);
        cell.transform.localPosition = position;
        cell.name = string.Format("{0} , {1} ", x, y);
        cell.tmp.text = cell.name;
        return cell;
    }
}
[System.Serializable]
public class HexagonGrid : BaseGrid
{
    public override Tile CreateTileCell(int x, int y)
    {
        Vector3 position;

        position.x = x * (HexMetrics.innerRadius * 2f);
        position.y = y * (HexMetrics.outerRadius * 1.5f); ;
        position.z = 0f;

        position.x = (x + y * 0.5f) * (HexMetrics.innerRadius * 2f);
        position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);

        Tile cell =  GridManager.Instance.CreateCell(tilePrefab);

        cell.transform.SetParent(parent, false);
        cell.transform.localPosition = position;
        cell.SetMatixData(x, y);
        cell.name = string.Format("{0} , {1} ", x, y);
        cell.tmp.text = cell.name;
        return cell;
    }

    public void SetTileAttribute()
    {
        List<Tile> tilebuffer = new List<Tile>();

        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                tilebuffer.Add(array[x, y]);
            }
        }

        tilebuffer.Reverse();
        for (int i = 0; i < tilebuffer.Count; i++)
        {
            tilebuffer[i].GetComponent<Order>().SetOriginOrder(i);
            tilebuffer[i].GetComponent<Order>().SetOrder(i);
        }
        tilebuffer.Reverse();

        var taskBuffer =  GetBuildTaskBuffer();

        Queue<KeyValuePair<KeyValuePair<int, int>, Tile>> data = new Queue<KeyValuePair<KeyValuePair<int, int>, Tile>>();
        //완

       
        DrawTile(tilebuffer,taskBuffer);
    }

    private Queue<KeyValuePair<eCampType, int>> GetBuildTaskBuffer()
    {

        Queue<KeyValuePair<eCampType, int>> campTypeBuffer = new Queue<KeyValuePair<eCampType, int>>();

        foreach (KeyValuePair<eCampType, int> data in request)
        {
            campTypeBuffer.Enqueue(new KeyValuePair<eCampType, int>(data.Key,data.Value));
        }

        return campTypeBuffer;
    } 

    private void DrawTile(List<Tile> tileBuffer, Queue<KeyValuePair<eCampType, int>> cameTypeBuffer)
    {
        var gridMgr = GridManager.Instance.GetTileItemSO();

        while (cameTypeBuffer.Count > 0)
        {
            
            int targetIndex = 0;
            var pair =  cameTypeBuffer.Dequeue();
            
            for(int i= 0; i < pair.Value; i ++)
            {
                Debug.Log(pair.Key);

                switch (pair.Key)
                {
                    case eCampType.PlainsCastle:

                        var hexagonGrid = GridManager.Instance.hexagonGrid;
                        int x = hexagonGrid.width / 2;
                        int y = hexagonGrid.hegiht / 2;
                        targetIndex = tileBuffer.FindIndex(data => data.matrixX == x && data.matrixY == y);
                        Debug.Log($"targetIndex {targetIndex}");
                        break;
                    case eCampType.Volcano:
                        targetIndex = tileBuffer.Count-1;
                        break;
                    case eCampType.Desert:
                    case eCampType.Jungle:
                    case eCampType.Plains:
                    case eCampType.Mountain:
                    case eCampType.ForestSnow:
                    case eCampType.Ocean:
                        targetIndex = Random.Range(0, tileBuffer.Count - 1);
                        break;
                    default:
                        break;
                }

                tileBuffer[targetIndex].TileSetup(gridMgr.camps[pair.Key]);
                tileBuffer.RemoveAt(targetIndex);

            }
        }

        while (tileBuffer.Count > 0)
        {
            tileBuffer[0].TileSetup(GridManager.Instance.GetTileItemSO().camps[eCampType.Forest]);
            tileBuffer.RemoveAt(0);
        }

    }

}
[System.Serializable]
public class BattleGrid : BaseGrid
{
    public override Tile CreateTileCell(int x, int y)
    {
        Vector3 position;

        position.x = x * 16;
        position.y = y * 16;
        position.z = 0f;

        Tile cell = GridManager.Instance.CreateCell(tilePrefab);

        cell.transform.SetParent(parent, false);
        cell.transform.localPosition = position;
        cell.name = string.Format("{0} , {1} ", x, y);
        cell.tmp.text = cell.name;
        return cell;
    }
}