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

    public void CreateGird()
    {
        array = new Tile[width, hegiht];

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
                if (x == (array.GetLength(0) / 2) && y == (array.GetLength(1) / 2))
                {
                    array[x, y].playerCamp = true;
                    int PlainsCastleItem = (int)TileItem.eCampType.PlainsCastle;
                    array[x, y].TileSetup(GridManager.Instance.GetTileItemSO().items[PlainsCastleItem]);
                    continue;
                }

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

        var campbuffer =  CreateCampCount();

        DrawTile(tilebuffer, campbuffer);

    }

    private Queue<KeyValuePair<TileItem.eCampType, int>> CreateCampCount()
    {
        Queue<KeyValuePair<TileItem.eCampType, int>> campTypeBuffer = new Queue<KeyValuePair<TileItem.eCampType, int>>();
        campTypeBuffer.Enqueue(new KeyValuePair<TileItem.eCampType, int>(TileItem.eCampType.Volcano, 1));
        campTypeBuffer.Enqueue(new KeyValuePair<TileItem.eCampType, int>(TileItem.eCampType.Mountain, 3));
        campTypeBuffer.Enqueue(new KeyValuePair<TileItem.eCampType, int>(TileItem.eCampType.Plains, 10));
        return campTypeBuffer;
    }

    private void DrawTile(List<Tile> tileBuffer, Queue<KeyValuePair<TileItem.eCampType, int>> cameTypeBuffer)
    {
        var gridMgr = GridManager.Instance.GetTileItemSO();

        while (cameTypeBuffer.Count > 0)
        {
            int targetIndex = 0;
            var pair =  cameTypeBuffer.Dequeue();
            int item = (int)pair.Key;

            
            for(int i= 0; i < pair.Value; i ++)
            {
                switch (pair.Key)
                {
                    case TileItem.eCampType.Volcano:
                        targetIndex = tileBuffer.Count-1;
                        break;
                    case TileItem.eCampType.Mountain:
                        targetIndex = 1;

                        break;
                    case TileItem.eCampType.Plains:
                        targetIndex = 2;
                        break;
                    default:
                        Debug.Log($"DrawTile Not Found {pair.Key}");
                        break;
                }

                tileBuffer[targetIndex].TileSetup(gridMgr.items[item]);
                tileBuffer.RemoveAt(targetIndex);

            }
        }

        while (tileBuffer.Count > 0)
        {
            int forestItem = (int)TileItem.eCampType.Forest;
            tileBuffer[0].TileSetup(GridManager.Instance.GetTileItemSO().items[forestItem]);
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