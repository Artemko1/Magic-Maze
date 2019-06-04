using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator
{
    /// <summary>
    /// Случайно генерирует стены для переданной клетки.
    /// </summary>
    /// <param name="tile"></param>
    public static void GenerateWalls(Tile excessTile)
    {
        if (Random.value <= 0.6)
        {
            GenerateOppositeWalls(excessTile);
        }
        else
        {
            GenerateCornerWalls(excessTile);
        }
    }

    private static void GenerateOppositeWalls(Tile excessTile)
    {
        if (Random.value <= 0.5)
        {
            // Верхнюю и нижнюю
            ActivateWalls(excessTile, true, false, true, false);
        }
        else
        {
            // Правую и левую
            ActivateWalls(excessTile, false, true, false, true);
        }
    }

    private static void GenerateCornerWalls(Tile excessTile)
    {
        if (Random.value <= 0.25)
        {
            // Верхнюю и правую
            ActivateWalls(excessTile, true, true, false, false);
        }
        else if (Random.value <= 0.5)
        {
            // Правую и нижнюю
            ActivateWalls(excessTile, false, true, true, false);
        }
        else if (Random.value <= 0.75)
        {
            // Нижнюю и левую
            ActivateWalls(excessTile, false, false, true, true);
        }
        else
        {
            // Левую и верхнюю
            ActivateWalls(excessTile, true, false, false, true);
        }
    }

    /// <summary>
    /// Устанавливает булевые переменные и отображает соответствующие стенки.
    /// </summary>
    /// <param name="excessTile"></param>
    /// <param name="up"></param>
    /// <param name="right"></param>
    /// <param name="down"></param>
    /// <param name="left"></param>
    private static void ActivateWalls(Tile excessTile, bool up, bool right, bool down, bool left)
    {
        excessTile.IsWallUp = up;
        excessTile.IsWallRight = right;
        excessTile.IsWallDown = down;
        excessTile.IsWallLeft = left;
    }
}
