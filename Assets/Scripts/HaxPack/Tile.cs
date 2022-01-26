using UnityEngine;
using TMPro;
namespace Hex_Package
{
    public class Tile : MonoBehaviour
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
    }
}