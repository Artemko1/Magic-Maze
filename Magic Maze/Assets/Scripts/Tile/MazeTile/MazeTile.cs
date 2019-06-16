using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class MazeTile : Tile
{
    public byte zIndex;
    public byte xIndex;
    
    public byte respawnNumber;
    public Player currentPlayer;

    //public GameObject currentItem;


    /// <summary>
    /// Смещение вверх на расстояние одной клетки
    /// </summary>
    public void MoveUp() => transform.Translate(new Vector3(0, 0, 1) * maze.Spacing, Space.World);
    /// <summary>
    /// Смещение вправо на расстояние одной клетки
    /// </summary>
    public void MoveRight() => transform.Translate(new Vector3(1, 0, 0) * maze.Spacing, Space.World);
    /// <summary>
    /// Смещение вниз на расстояние одной клетки
    /// </summary>    
    public void MoveDown() => transform.Translate(new Vector3(0, 0, -1) * maze.Spacing, Space.World);
    /// <summary>
    /// Смещение влево на расстояние одной клетки
    /// </summary>
    public void MoveLeft() => transform.Translate(new Vector3(-1, 0, 0) * maze.Spacing, Space.World);

    public void CreateRespawn(byte number) 
    {
        // 1-4 для респаунов, 0 для нереспаунов
            respawnNumber = number;
    }
}
