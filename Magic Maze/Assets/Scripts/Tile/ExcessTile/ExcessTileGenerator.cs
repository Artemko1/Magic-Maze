using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcessTileGenerator
{
    /// <summary>
    /// Случайно генерирует стены для переданной клетки.
    /// </summary>
    /// <param name="tile"></param>
    public static void GenerateWalls(ExcessTile excessTile)
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

    private static void GenerateOppositeWalls(ExcessTile excessTile)
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

    private static void GenerateCornerWalls(ExcessTile excessTile)
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
    private static void ActivateWalls(ExcessTile excessTile, bool up, bool right, bool down, bool left)
    {
        excessTile.IsWallUp = up;
        excessTile.IsWallRight = right;
        excessTile.IsWallDown = down;
        excessTile.IsWallLeft = left;

        if (excessTile.IsWallUp)
        {
            SetWallVisual(excessTile, "Wall Up", true);
        }
        else
        {
            SetWallVisual(excessTile, "Wall Up", false);
        }

        if (excessTile.IsWallRight)
        {
            SetWallVisual(excessTile, "Wall Right", true);
        }
        else
        {
            SetWallVisual(excessTile, "Wall Right", false);
        }
        if (excessTile.IsWallDown)
        {
            SetWallVisual(excessTile, "Wall Down", true);
        }
        else
        {
            SetWallVisual(excessTile, "Wall Down", false);
        }
        if (excessTile.IsWallLeft)
        {
            SetWallVisual(excessTile, "Wall Left", true);
        }
        else
        {
            SetWallVisual(excessTile, "Wall Left", false);
        }
    }

    private static void SetWallVisual(ExcessTile excessTile, string wallName, bool set)
    {
        excessTile.transform.Find("Plate Visual").Find(wallName).gameObject.SetActive(set);
    }
}
