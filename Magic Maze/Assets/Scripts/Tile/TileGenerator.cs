using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileGenerator
{
    public static void GenerateCornerWalls(Tile tile, Direction upDownDirection, Direction leftRightDirection)
    {
        ActivateWalls(tile, upDownDirection == Direction.Up, leftRightDirection == Direction.Right,
          leftRightDirection == Direction.Down, leftRightDirection == Direction.Left);
    }
    public static void GenerateNoWalls(Tile tile)
    {
        ActivateWalls(tile, false, false, false, false);
    }

    public static void GenerateOneWall(Tile tile)
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
    public static void GenerateRandomWalls(Tile tile)
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

    private static void GenerateOppositeWalls(Tile tile)
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

    private static void GenerateCornerWalls(Tile tile)
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

    private static void ActivateWalls(Tile tile, bool up, bool right, bool down, bool left)
    {
        tile.IsWallUp = up;
        tile.IsWallRight = right;
        tile.IsWallDown = down;
        tile.IsWallLeft = left;
    }
}
