using UnityEngine;

namespace Tile
{
    public static class TileGenerator
    {
        public static void GenerateCornerWalls(global::Tile.Tile tile, Direction upDownDirection, Direction leftRightDirection)
        {
            ActivateWalls(tile, upDownDirection == Direction.Up, leftRightDirection == Direction.Right,
                upDownDirection == Direction.Down, leftRightDirection == Direction.Left);
        }
        public static void GenerateNoWalls(global::Tile.Tile tile)
        {
            ActivateWalls(tile, false, false, false, false);
        }

        public static void GenerateOneWall(global::Tile.Tile tile)
        {
            if (Random.value <= 0.25)
            {
                ActivateWalls(tile, true, false, false, false);
            }
            else if (Random.value <= 0.5)
            {
                ActivateWalls(tile, false, true, false, false);
            }
            else if (Random.value <= 0.75)
            {
                ActivateWalls(tile, false, false, true, false);
            }
            else
            {
                ActivateWalls(tile, false, false, false, true);
            }
        }

        /// <summary>
        /// Случайно генерирует стены для переданной клетки.
        /// </summary>
        /// <param name="tile"></param>
        public static void GenerateRandomWalls(global::Tile.Tile tile)
        {
            if (Random.value <= 0.6)
            {
                GenerateOppositeWalls(tile);
            }
            else
            {
                GenerateCornerWalls(tile);
            }
        }

        private static void GenerateOppositeWalls(global::Tile.Tile tile)
        {
            if (Random.value <= 0.5)
            {
                // Верхнюю и нижнюю
                ActivateWalls(tile, true, false, true, false);
            }
            else
            {
                // Правую и левую
                ActivateWalls(tile, false, true, false, true);
            }
        }

        private static void GenerateCornerWalls(global::Tile.Tile tile)
        {
            if (Random.value <= 0.25)
            {
                // Верхнюю и правую
                ActivateWalls(tile, true, true, false, false);
            }
            else if (Random.value <= 0.5)
            {
                // Правую и нижнюю
                ActivateWalls(tile, false, true, true, false);
            }
            else if (Random.value <= 0.75)
            {
                // Нижнюю и левую
                ActivateWalls(tile, false, false, true, true);
            }
            else
            {
                // Левую и верхнюю
                ActivateWalls(tile, true, false, false, true);
            }
        }

        private static void ActivateWalls(global::Tile.Tile tile, bool up, bool right, bool down, bool left)
        {
            tile.IsWallUp = up;
            tile.IsWallRight = right;
            tile.IsWallDown = down;
            tile.IsWallLeft = left;
        }
    }
}
