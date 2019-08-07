using Tile.MazeTile;
using UnityEngine;

namespace Item
{
    [SelectionBase]
    public class Item : MonoBehaviour
    {
        #region Variables

        public int Id
        {
            get => id;
            set
            {
                if (value == 0)
                {
                    id = value;
                }
            }
        }

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
        [SerializeField] private int id;

        #endregion


    }
}
