using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator
{
    /// <summary>
    /// Случайно генерирует стены для переданной клетки.
    /// </summary>
    /// <param name="tile"></param>
    public static void GenerateWalls(Tile tile)
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
