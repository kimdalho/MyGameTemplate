using UnityEngine;
using System.Collections.Generic;
/*
 그리드입니다. 공개 너비, 높이 및 셀 프리팹 변수를 사용하여 간단한 구성 요소를 만듭니다. 그런 다음 이 구성 요소가 있는 게임 개체를 장면에 추가합니다.
 */

/// <summary>
/// TileManager의 역할
/// 오로지 맵을 그려주는 역할이다
/// 0.그리드 맵의 행과 열의 사이즈 정의
/// 1.육각타일의 규칙으로 타일 배치
/// 2.랜덤한 속성의 타일 배치 및 셋업
/// 3.y축 기준의 타일 orderlayer의 셋업
/// </summary>
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

        private Dictionary<TileItem.eType, TileItem> itembuffer;

        public Tile[,] Setup()
        {
            Tile[,] result = new Tile[width,height];

            for (int y = 0, i = 0; y < result.GetLength(1); y++)
            {
                for (int x = 0; x < result.GetLength(0); x++)
                {
                    result[x,y] = CreateCell(x, y, i++);
                }
            }

            SetupItemBuffer();

            List<Tile> tileList = new List<Tile>();

            for (int y = 0; y < result.GetLength(1); y++)
            {
                for (int x = 0; x < result.GetLength(0); x++)
                {
                    tileList.Add(result[x,y]);
                }
            }

            tileList.Reverse();

            for (int i = 0; i < tileList.Count; i++)
            {
                tileList[i].GetComponent<Order>().SetOrder(i);
            }

            tileList.Reverse();

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

            return result;
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

            Tile cell  = Instantiate<Tile>(tilePrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.name = string.Format("{0} , {1} ", x, y);
            cell.tmp.text = cell.name;
            return cell;
        }
    }
}