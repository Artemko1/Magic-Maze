using UnityEngine;

namespace Tile.MazeTile
{
    public class MazeTile : Tile
    {
        #region Variables

        public byte zIndex;
        public byte xIndex;
    
        public Player.Player currentPlayer;
        public Item.Item currentItem;

        #endregion

        /// <summary>
        /// Смещение вверх на расстояние одной клетки
        /// </summary>
        public void MoveUp()
        {
            transform.Translate(new Vector3(0, 0, 1) * maze.Spacing, Space.World);
            PlayerAndItemToTilePos();
        }

        /// <summary>
        /// Смещение вправо на расстояние одной клетки
        /// </summary>
        public void MoveRight()
        {
            transform.Translate(new Vector3(1, 0, 0) * maze.Spacing, Space.World);
            PlayerAndItemToTilePos();
        }

        /// <summary>
        /// Смещение вниз на расстояние одной клетки
        /// </summary>    
        public void MoveDown()
        {
            transform.Translate(new Vector3(0, 0, -1) * maze.Spacing, Space.World);
            PlayerAndItemToTilePos();
        }

        /// <summary>
        /// Смещение влево на расстояние одной клетки
        /// </summary>
        public void MoveLeft()
        {
            transform.Translate(new Vector3(-1, 0, 0) * maze.Spacing, Space.World);
            PlayerAndItemToTilePos();
        }

        private void PlayerAndItemToTilePos()
        {
            if (currentPlayer != null)
            {
                currentPlayer.transform.position = transform.position;
            }
            if (currentItem != null)
            {
                currentItem.transform.position = transform.position;
            }
        }
    }
}
