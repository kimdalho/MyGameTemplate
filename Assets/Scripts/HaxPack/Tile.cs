using UnityEngine;
using TMPro;
namespace Hex_Package
{
    public class Tile : Node
    {
        [SerializeField] SpriteRenderer render;
        public TMP_Text tmp;

        private void Awake()
        {
            tmp = GetComponentInChildren<TMP_Text>();
        }

        public void TileSetup(TileItem item)
        {
            render.sprite =  item.sprite;
        }

        private void OnMouseExit()
        {

        }

        private void OnMouseUp()
        {

        }

        private void OnMouseDown()
        {
            
        }

    }



}