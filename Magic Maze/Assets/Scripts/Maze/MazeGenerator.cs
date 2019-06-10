using UnityEngine;
/// <summary>
/// Класс инкапсулирует начальную генерацию лабиринта.
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    private GameObject tileObj;
    private Maze maze;

    void Awake()
    {
        maze = GetComponent<Maze>();
    }

    public MazeTile[] GenerateTiles(int boardSize)
    {
        MazeTile[] tileArray;
        tileArray = new MazeTile[boardSize * boardSize];
        for (byte z = 0; z < boardSize; z++)
        {
            for (byte x = 0; x < boardSize; x++)
            {
                // Создается новая клетка tileObj.
                Vector3 pos = new Vector3(x, 0, -z) * maze.Spacing;
                tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, maze.transform);
                tileObj.name = ("MazeTile " + z + " " + x);

                MazeTile tile = tileObj.GetComponent<MazeTile>();
                maze.SetTile(z, x, tile, tileArray);

                // Изменение свойств клетки
                tile.zIndex = z;
                tile.xIndex = x;
            }
        }
        return tileArray;
    }
}