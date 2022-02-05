using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
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
        public int width ;
        public int height;
        

        [Header("Tile")]
        [SerializeField] TileItemSO itemso;
        public Tile tilePrefab;

        public Tile[,] tileArray;



        public void Builder(Tile[,] tileArray)
        {
            List<Tile> buffer = new List<Tile>();

            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                for (int x = 0; x < tileArray.GetLength(0); x++)
                {
                    buffer.Add(tileArray[x, y]);
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
            SetTile(buffer, TileItem.eType.Mountain,3);
            SetTile(buffer, TileItem.eType.PlainsCastle,2);
            SetTile(buffer, TileItem.eType.Plains,10);
            while(buffer.Count > 0)
            {
                SetTile(buffer, TileItem.eType.Forest);
            }
        }

        public void Setup()
        {
            tileArray = new Tile[width,height];

            for (int y = 0, i = 0; y < tileArray.GetLength(1); y++)
            {
                for (int x = 0; x < tileArray.GetLength(0); x++)
                {
                    tileArray[x,y] = CreateCell(x, y, i++);
                }
            }

            List<Tile> tileList = new List<Tile>();

            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                for (int x = 0; x < tileArray.GetLength(0); x++)
                {
                    tileList.Add(tileArray[x,y]);
                }
            }
            
        }

        Tile CreateCell(int x, int y, int i)
        {
            Vector3 position;

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

        private void SetTile(List<Tile> list, TileItem.eType type ,int repeat = 1)
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
                list[targetIndex].TileSetup(itemso.items[item]);
                list.RemoveAt(targetIndex);
            }
        }


        public void BlockCollider()
        {
            for (int y = 0, i = 0; y < tileArray.GetLength(1); y++)
            {
                for (int x = 0; x < tileArray.GetLength(0); x++)
                {
                    tileArray[x, y].GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }


    }
}