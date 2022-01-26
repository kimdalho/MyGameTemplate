using UnityEngine;
using System.Collections.Generic;
/*
 그리드입니다. 공개 너비, 높이 및 셀 프리팹 변수를 사용하여 간단한 구성 요소를 만듭니다. 그런 다음 이 구성 요소가 있는 게임 개체를 장면에 추가합니다.
 */
namespace Hex_Package
{
    public class TileManager : Singleton<TileManager>
    {
        [Header("Grid")]
        public int width = 6;
        public int height = 6;


        [Header("Tile")]
        [SerializeField] TileItemSO itemso;
        public Tile tilePrefab;

        public Tile[] cells;
        private Dictionary<TileItem.eType, TileItem> itembuffer;

        public void Setup()
        {
            cells = new Tile[height * width];

            for (int y = 0, i = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[height + width] = CreateCell(x, y, i++);
                }
            }

            SetupItemBuffer();

            List<Tile> tileList = new List<Tile>();

            for (int i = 0; i < cells.Length; i++)
            {
                tileList.Add(cells[i]);
                cells[i].GetComponent<Order>().SetOrder(-i);
            }
                


            

            SetTileData(tileList, 0, TileItem.eType.Base);
            SetTileData(tileList, tileList.Count - 1, TileItem.eType.Volcano);

            for (int i = 0; i < 3; i++)
            {
                SetTileData(tileList, Random.Range(0, tileList.Count), TileItem.eType.Base);
            }

            for (int i = 0; i < 3; i++)
            {
                SetTileData(tileList, Random.Range(0, tileList.Count), TileItem.eType.Mountain);
            }

            for (int i = 0; i < 2; i++)
            {
                SetTileData(tileList, Random.Range(0, tileList.Count), TileItem.eType.PlainsCastle);
            }
            
            for(int i = 0; i < tileList.Count; i ++)
            {
                SetTileData(tileList,i, TileItem.eType.Forest);
            }


        }

        private void SetTileData(List<Tile> tileList, int index, TileItem.eType type)
        {
            tileList[index].TileSetup(itembuffer[type]);
            tileList.RemoveAt(index);
        }


        private void SetupItemBuffer()
        {
            itembuffer = new Dictionary<TileItem.eType, TileItem>();
            for (int i = 0; i < itemso.items.Length; i++)
            {
                itembuffer.Add(itemso.items[i].type, itemso.items[i]);
            }
        }


        Tile CreateCell(int x, int y, int i)
        {
            Vector3 position;
            /*position.x = x * 2.7f;
			position.y = y * 2.7f;
			position.z = 0f;*/


            position.x = x * (HexMetrics.innerRadius * 2f);
            position.y = y * (HexMetrics.outerRadius * 1.5f); ;
            position.z = 0f;

            position.x = (x + y * 0.5f) * (HexMetrics.innerRadius * 2f);
            position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);



            Tile cell = cells[i] = Instantiate<Tile>(tilePrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.tmp.text = string.Format("{0} , {1} ", x, y);
            return cell;
        }
    }
}