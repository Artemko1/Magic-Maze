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

    public void GenerateNewTiles() // Генерирует новые стенки всем клеткам лабиринта
    {
        for (byte z = 0; z < maze.BoardSize; z++)
        {
            for (byte x = 0; x < maze.BoardSize; x++)
            {
                TileGenerator.GenerateWalls(maze.GetTile(z, x));
            }
        }
    }

    public void GenerateExcessPositions()
    {
        maze.extraPositions = new Vector3[maze.MovableRows];
        byte n = 0;
        byte z, x;

        x = 1;
        while (x < maze.BoardSize - 1)
        {
            SetExtraPosition((byte)(maze.BoardSize - 1), x, n, Direction.Down);
            x += 2;
            n++;
        }

        z = (byte)(maze.BoardSize - 2);
        while (z > 0 && z < maze.BoardSize)
        {
            SetExtraPosition(z, (byte)(maze.BoardSize - 1), n, Direction.Right);
            z -= 2;
            n++;
        }

        x = (byte)(maze.BoardSize - 2);
        while (x > 0 && x < maze.BoardSize)
        {
            SetExtraPosition(0, x, n, Direction.Up);
            x -= 2;
            n++;
        }

        z = 1;
        while (z < maze.BoardSize - 1)
        {
            SetExtraPosition(z, 0, n, Direction.Left);
            z += 2;
            n++;
        }
    }

    private void SetExtraPosition(byte z, byte x, byte n, Direction direction) // direction - направление смещения позиций.
    {
        switch (direction)
        {
            //Up
            case Direction.Up:
                maze.extraPositions[n] = maze.GetTile(z, x).transform.position + new Vector3(0, 0, 1) * maze.Spacing;
                break;
            //Right
            case Direction.Right:
                maze.extraPositions[n] = maze.GetTile(z, x).transform.position + new Vector3(1, 0, 0) * maze.Spacing;
                break;
            //Down
            case Direction.Down:
                maze.extraPositions[n] = maze.GetTile(z, x).transform.position + new Vector3(0, 0, -1) * maze.Spacing;
                break;
            //Left
            case Direction.Left:
                maze.extraPositions[n] = maze.GetTile(z, x).transform.position + new Vector3(-1, 0, 0) * maze.Spacing;
                break;
        }
    }
}