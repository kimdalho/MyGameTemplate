using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;

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
        List<Tile> buffer = new List<Tile>();

        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                buffer.Add(array[x, y]);
            }
        }

        buffer.Reverse();
        for (int i = 0; i < buffer.Count; i++)
        {
            buffer[i].GetComponent<Order>().SetOriginOrder(i);
            buffer[i].GetComponent<Order>().SetOrder(i);
        }
        buffer.Reverse();

        //반복문으로 수정
        SetTile(buffer, TileItem.eType.Base);
        SetTile(buffer, TileItem.eType.Volcano);
        SetTile(buffer, TileItem.eType.Mountain, 3);
        SetTile(buffer, TileItem.eType.PlainsCastle, 2);
        SetTile(buffer, TileItem.eType.Plains, 10);
        while (buffer.Count > 0)
        {
            SetTile(buffer, TileItem.eType.Forest);
        }
    }

    private void SetTile(List<Tile> list, TileItem.eType type, int repeat = 1)
    {
        for (int i = 0; i < repeat; i++)
        {
            int targetIndex = 0;

            int item = (int)type - 1;
            switch (type)
            {
                case TileItem.eType.Base:
                case TileItem.eType.Forest:
                    targetIndex = 0;
                    break;
                case TileItem.eType.Volcano:
                    targetIndex = list.Count - 1;
                    break;
                case TileItem.eType.Plains:
                case TileItem.eType.Mountain:
                case TileItem.eType.PlainsCastle:
                    targetIndex = Random.Range(0, list.Count);
                    break;
            }
            list[targetIndex].TileSetup(GridManager.Instance.GetTileItemSO().items[item]);
            list.RemoveAt(targetIndex);
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