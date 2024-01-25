using UnityEngine;
using TMPro;

    public class Tile : Node
    {
        [SerializeField] SpriteRenderer render;
        [SerializeField] TileItem item;
        public bool playerCamp;
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

        public TileItem.eCampType GeteType()
        {
            return item.type;
        }

    }