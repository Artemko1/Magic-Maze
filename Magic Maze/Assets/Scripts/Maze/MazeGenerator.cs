using UnityEngine;
/// <summary>
/// Класс инкапсулирует начальную генерацию лабиринта.
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    private GameObject tileObj;
    private Maze maze;

    //public MazeGenerator(Maze maze)
    //{
    //    this.maze = maze;
    //}

    void Awake()
    {
        maze = GetComponent<Maze>();
    }

    public Tile[] GenerateTiles(int boardSize)
    {
        Tile[] tileArray;
        tileArray = new Tile[boardSize * boardSize];
        for (byte z = 0; z < boardSize; z++)
        {
            for (byte x = 0; x < boardSize; x++)
            {
                // Создается новая клетка tileObj.
                Vector3 pos = new Vector3(x, 0, -z) * Maze.spacing;
                tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, maze.transform);
                tileObj.name = ("Tile " + z + " " + x);

                Tile tile = tileObj.GetComponent<Tile>();
                Maze.SetTile(tileArray, z, x, tile);

                // Изменение свойств клетки
                tile.zIndex = z;
                tile.xIndex = x;
            }
        }
        return tileArray;
    }
}