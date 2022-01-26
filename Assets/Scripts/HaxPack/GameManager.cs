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
            TileManager.Instance.Setup();
            UnitManager.Instance.CreatePlayer(TileManager.Instance.cells[0].transform.position);
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
