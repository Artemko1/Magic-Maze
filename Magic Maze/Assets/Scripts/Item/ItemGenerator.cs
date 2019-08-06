using System.Collections.Generic;
using Tile.MazeTile;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Maze.Maze))]
    public class ItemGenerator : MonoBehaviour
    {
        
        public Item[] ItemPrefabs;
        
        private Maze.Maze maze;


        #region Unity Methods

        private void Awake()
        {
            maze = GetComponent<Maze.Maze>();
        }

        #endregion

        public void GenerateItems(int itemsPerPlayer)
        {
            if (itemsPerPlayer == 0)
            {
                return;
            }
            
            // Кортеж координат (0,0), (0,1), (0,2) и т.д.
            var tileList = new List<(int, int)>();
            for (var i = 0; i < maze.BoardSize; i++)
            {
                for (var j = 0; j < maze.BoardSize; j++)
                {
                    tileList.Add((i, j));
                }
            }

            var numberOfItems = itemsPerPlayer * maze.NumberOfPlayers;

            for (var i = 0; i < numberOfItems; i++)
            {
                var index = Random.Range(0, tileList.Count);
                var tile = maze.GetTile(tileList[index]);
                if (tile.currentItem != null || tile.currentPlayer != null)
                {
                    i--;
                }
                else
                {
                    CreateItem(tile, i);
                }
                tileList.RemoveAt(index);
                if (tileList.Count == 0)
                {
                    break;
                }
                Debug.Log(maze.Items[i]);
            }
        }


        private void CreateItem(MazeTile tile, int itemId)
        {
            var itemObj = Instantiate(
                ItemPrefabs[itemId],
                tile.transform.position,
                Quaternion.identity,
                maze.transform);
            
            
            var item = itemObj.GetComponent<Item>();
            tile.currentItem = item;
            item.ChangeCurrentTile(tile);
            item.Id = itemId;
            maze.Items.Add(item);
        }
    }
}
