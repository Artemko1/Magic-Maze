using Managers;
using Tile.MazeTile;
using UnityEngine;

namespace Item
{
    [SelectionBase]
    public class Item : MonoBehaviour
    {
        #region Variables

        public Texture2D texture;
        
        public MazeTile CurrentTile
        {
            get => currentTile;
            set
            {
                if (currentTile != null)
                {
                    currentTile.currentItem = null;
                }
                currentTile = value;
                currentTile.currentItem = this;
            }
        }

        [SerializeField] private MazeTile currentTile;
        private ItemManager itemManager;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            var board = transform.parent;
            itemManager = board.GetComponent<ItemManager>();
        }

        #endregion


    }
}
