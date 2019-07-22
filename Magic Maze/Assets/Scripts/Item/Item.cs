using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Item : MonoBehaviour
{
    public MazeTile CurrentTile { get; private set; }

    /// <summary>
    /// Убирает текущего игрока у текущей клетки
    /// и добавляет его переданной клетке.
    /// </summary>
    /// <param name="mazeTile"></param>
    public void ChangeCurrentTile(MazeTile mazeTile)
    {
        if (CurrentTile != null)
        {
            CurrentTile.currentItem = null;
        }
        CurrentTile = mazeTile;
        CurrentTile.currentItem = this;
    }
}
