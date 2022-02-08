using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex_Package
{
    public class GameManager : Singleton<GameManager>
    {
        public int MoveScore;

        // Start is called before the first frame update
        void Start()
        {
            TileManager.Instance.Setup();
            TileManager.Instance.Builder(); 
            UnitManager.Instance.UnitGenerator();
            UnitManager.Instance.CreatePlayer();
            PathFindingManager.Instance.CreateNodeList();

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
