using Tile.MazeTile;
using UnityEngine;

namespace Item
{
    [SelectionBase]
    public class Item : MonoBehaviour
    {
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
            
        [SerializeField] private MazeTile currentTile;
        [SerializeField] private int id;


        /// <summary>
        /// Убирает текущего игрока у текущей клетки
        /// и добавляет его переданной клетке.
        /// </summary>
        /// <param name="mazeTile"></param>
        public void ChangeCurrentTile(MazeTile mazeTile)
        {
            if (currentTile != null)
            {
                currentTile.currentItem = null;
            }
            currentTile = mazeTile;
            currentTile.currentItem = this;
        }
    }
}
