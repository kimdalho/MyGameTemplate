using UnityEngine;
using TMPro;
namespace Hex_Package
{
    public class Tile : Node
    {
        [SerializeField] SpriteRenderer render;
        [SerializeField] TileItem item;
        public TMP_Text tmp;

        private void Awake()
        {
            tmp = GetComponentInChildren<TMP_Text>();
        }

        public void TileSetup(TileItem item)
        {
            this.item = item;

            render.sprite = this.item.sprite;
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

        public TileItem.eType GeteType()
        {
            return item.type;
        }

    }



}