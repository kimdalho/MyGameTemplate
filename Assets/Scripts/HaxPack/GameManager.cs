using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex_Package
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject Pick;
        public int MoveScore;

        // Start is called before the first frame update
        void Start()
        {
            TileManager.Instance.Setup();
            var tiles = TileManager.Instance.tileArray;
            TileManager.Instance.Builder(tiles);

            UnitManager.Instance.CreatePlayer(tiles[0,0].transform.position);
            PathFindingManager.Instance.SetNodes(tiles);
            StartGame();
            //TileManager.Instance.BlockCollider();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                TileTurnManager.OnAddCard?.Invoke();
            }
        }

        public void StartGame()
        {
            StartCoroutine(TileTurnManager.Instance.StartGameCo());
        }

        public void CheatGetMoveScore()
        {
            MoveScore += 2;
            UiManager.Instance.RefreshMoveScoreText();
        }

    }

}
