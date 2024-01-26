using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
/*
 그리드입니다. 공개 너비, 높이 및 셀 프리팹 변수를 사용하여 간단한 구성 요소를 만듭니다. 그런 다음 이 구성 요소가 있는 게임 개체를 장면에 추가합니다.
 */

/// <summary>
/// TileManager의 역할
/// 오로지 맵을 그려주는 역할이다 ab
/// 0.그리드 맵의 행과 열의 사이즈 정의
/// 1.육각타일의 규칙으로 타일 배치
/// 2.랜덤한 속성의 타일 배치 및 셋업
/// 3.y축 기준의 타일 orderlayer의 셋업
/// </summary>

    public class GridManager : Singleton<GridManager>
    {
      
        [Header("TilePrefab")]
        [SerializeField] TileItemSO itemso;

        [Header("Girds")]
        public HexagonGrid hexagonGrid;
        public BattleGrid battleGrid;
        


        public void Setup()
        {
            Debug.Log("call");
            hexagonGrid.CreateGird();
            hexagonGrid.SetTileAttribute();

            //battleGrid는 인게임 로컬맵 구현에 사용계획
            //2024년 1월 24일 임시 주
            //battleGrid.CreateGird();
        }



        public Tile CreateCell(Tile Tileprefab)
        {
            Tile cell = Instantiate<Tile>(Tileprefab);
            return cell;
        }



        //###############Gettter##############################
        /// <summary>
        /// <paramref name="type"/>에 해당하는 모든 타일을 배열타입으로 가져온다
        /// </summary>
        public List<Tile> GetTilesToType(TileItem.eCampType type)
        {
            List<Tile> result = new List<Tile>();

            for (int y = 0; y < GetHexagonArray().GetLength(1); y++)
            {
                for (int x = 0; x < GetHexagonArray().GetLength(0); x++)
                {
                    var curTile = GetHaxgonTile(x, y);
                    if (curTile.GeteType() == type)
                        result.Add(curTile);
                }
            }
            return result;
        }

        public TileItemSO GetTileItemSO()
        {
            return itemso;
        }


        public Tile[,] GetHexagonArray()
        {
            return hexagonGrid.array;
        }

        public Tile GetHaxgonTile(int x, int y)
        {
            return hexagonGrid.array[x, y];
        }

        //###############Gettter##############################


    }

