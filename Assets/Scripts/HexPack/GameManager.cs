using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {
        // Start is called before the first frame update
        void Start()
        {
            GridManager.Instance.Setup();
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
            UserData.Instance.move += 2;
            UiManager.Instance.RefreshTopUi();
        }

    }

