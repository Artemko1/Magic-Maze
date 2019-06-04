using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Класс генерирует клетки лабиринта.
/// </summary>
public static class TileGenerator
{
    /// <summary>
    /// Случайно генерирует стены для переданной клетки.
    /// </summary>
    /// <param name="tile"></param>
    public static void GenerateWalls(MazeTile tile)
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

    static void GenerateOppositeWalls(MazeTile tile)
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

    static void GenerateCornerWalls(MazeTile tile)
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
    /// <summary>
    /// Устанавливает булевые переменные и отображает соответствующие стенки.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="up"></param>
    /// <param name="right"></param>
    /// <param name="down"></param>
    /// <param name="left"></param>
    static void ActivateWalls(MazeTile tile, bool up, bool right, bool down, bool left)
    {
        tile.IsWallUp = up;
        tile.IsWallRight = right;
        tile.IsWallDown = down;
        tile.IsWallLeft = left;

        if (tile.IsWallUp)
        {
            SetWallVisual(tile, "Wall Up", true);
        }
        else
        {
            SetWallVisual(tile, "Wall Up", false);
        }

        if (tile.IsWallRight)
        {
            SetWallVisual(tile, "Wall Right", true);
        }
        else
        {
            SetWallVisual(tile, "Wall Right", false);
        }
        if (tile.IsWallDown)
        {
            SetWallVisual(tile, "Wall Down", true);
        }
        else
        {
            SetWallVisual(tile, "Wall Down", false);
        }
        if (tile.IsWallLeft)
        {
            SetWallVisual(tile, "Wall Left", true);
        }
        else
        {
            SetWallVisual(tile, "Wall Left", false);
        }
    }

    static void SetWallVisual(MazeTile tile, string wallName, bool set)
    {
        tile.transform.Find("Plate Visual").Find(wallName).gameObject.SetActive(set);
    }
}
