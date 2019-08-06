using Tile;
using Tile.MazeTile;
using UnityEngine;

namespace Maze
{
    [RequireComponent(typeof(Maze))]
    public class MazeGenerator : MonoBehaviour
    {
        #region Variables

        public GameObject tilePrefab;

        private Maze maze;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            maze = GetComponent<Maze>();
        }

        #endregion

        public void GenerateTiles(MazeTile[] tileArray)
        {
            for (byte z = 0; z < maze.BoardSize; z++)
            {
                for (byte x = 0; x < maze.BoardSize; x++)
                {
                    var tile = CreateTile(z, x);

                    if (IsCorner(z, x, out var upDownDirection, out var leftRightDirection))
                    {
                        TileGenerator.GenerateCornerWalls(tile, upDownDirection, leftRightDirection);
                    }
                    else if (!IsMovable(z, x))
                    {
                        TileGenerator.GenerateNoWalls(tile);
                    }
                    else
                    {
                        TileGenerator.GenerateRandomWalls(tile);
                    }

                    maze.SetTile(z, x, tile);
                    tile.zIndex = z;
                    tile.xIndex = x;
                }
            }
        }

        public void GenerateNewTiles() // Генерирует новые стенки всем клеткам лабиринта
        {
            for (byte z = 0; z < maze.BoardSize; z++)
            {
                for (byte x = 0; x < maze.BoardSize; x++)
                {
                    TileGenerator.GenerateRandomWalls(maze.GetTile(z, x));
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

        private bool IsCorner(byte z, byte x, out Direction upDownDirection, out Direction leftRightDirection)
        {
            if (z == 0 && x == 0)
            {
                upDownDirection = Direction.Up;
                leftRightDirection = Direction.Left;
            }
            else if (z == 0 && x == maze.BoardSize - 1)
            {
                upDownDirection = Direction.Up;
                leftRightDirection = Direction.Right;
            }
            else if (z == maze.BoardSize - 1 && x == 0)
            {
                upDownDirection = Direction.Down;
                leftRightDirection = Direction.Left;
            }
            else if (z == maze.BoardSize - 1 && x == maze.BoardSize - 1)
            {
                upDownDirection = Direction.Down;
                leftRightDirection = Direction.Right;
            }
            else
            {
                upDownDirection = (Direction)5;
                leftRightDirection = (Direction)5;
            }

            return (z == 0 || z == maze.BoardSize - 1) && (x == 0 || x == maze.BoardSize - 1);
        }

        private bool IsMovable(byte z, byte x)
        {
            if (z % 2 != 0 || x % 2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private MazeTile CreateTile(byte z, byte x)
        {
            var pos = new Vector3(x, 0, -z) * maze.Spacing;
            var tileObj = Instantiate(
                tilePrefab,
                maze.transform.position + pos,
                Quaternion.identity,
                maze.transform);
            tileObj.name = $"MazeTile {z} {x}";

            var tile = tileObj.GetComponent<MazeTile>();

            return tile;
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
}