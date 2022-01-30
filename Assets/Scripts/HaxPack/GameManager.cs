using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex_Package
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var tiles =  TileManager.Instance.Setup();
            
            UnitManager.Instance.CreatePlayer(tiles[0,0].transform.position);
            PathFindingManager.Instance.SetNodes(tiles);
            StartGame();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartGame()
        {
            StartCoroutine(TileTurnManager.Instance.StartGameCo());
        }
    }

}
