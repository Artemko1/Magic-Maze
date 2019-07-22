using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public MazeTile currentTile;

    /// <summary>
    /// Убирает текущего игрока у текущей клетки
    /// и добавляет его переданной клетке.
    /// </summary>
    /// <param name="mazeTile"></param>
    public void ChangeCurrentTile(MazeTile mazeTile)
    {
        currentTile.currentItem = null;
        currentTile = mazeTile;
        currentTile.currentItem = this;
    }
}
