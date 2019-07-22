using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Maze))]
public class ItemGenerator : MonoBehaviour
{
    public GameObject ItemPrefab;

    private Maze maze;

    public void GenerateItems(int numberOfItems)
    {
        // Кортеж координат (0,0), (0,1), (0,2) и т.д.
        List<(int, int)> tileList = new List<(int, int)>();
        for (int i = 0; i < maze.BoardSize; i++)
        {
            for (int j = 0; j < maze.BoardSize; j++)
            {
                tileList.Add((i, j));
            }
        }


        for (int i = 0; i < numberOfItems; i++)
        {
            int index = Random.Range(0, tileList.Count);
            MazeTile tile = maze.GetTile(tileList[index]);
            if (tile.currentItem != null || tile.currentPlayer != null)
            {
                i--;
            }
            else
            {
                GameObject itemObj = Instantiate(
                    ItemPrefab,
                    tile.transform.position,
                    Quaternion.identity,
                    maze.transform);
                itemObj.name = $"Item {tileList[index]}";
                tileList.RemoveAt(index);
                Item item = itemObj.GetComponent<Item>();
                tile.currentItem = item;
                item.ChangeCurrentTile(tile);
            }
        }
    }

    private void Awake()
    {
        maze = GetComponent<Maze>();
    }
}
