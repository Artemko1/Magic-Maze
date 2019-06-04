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

    private void Awake()
    {
        upWallObject = transform.Find("Plate Visual").Find("Wall Up").gameObject;
        rightWallObject = transform.Find("Plate Visual").Find("Wall Right").gameObject;
        downWallObject = transform.Find("Plate Visual").Find("Wall Down").gameObject;
        leftWallObject = transform.Find("Plate Visual").Find("Wall Left").gameObject;

        TileGenerator.GenerateWalls(this);
    }

    /// <summary>
    /// Смещение вверх на расстояние одной клетки
    /// </summary>
    public void MoveUp()
    {
        transform.Translate(new Vector3(0, 0, 1) * Maze.spacing, Space.World);
    }
    /// <summary>
    /// Смещение вправо на расстояние одной клетки
    /// </summary>
    public void MoveRight()
    {
        transform.Translate(new Vector3(1, 0, 0) * Maze.spacing, Space.World);
    }
    /// <summary>
    /// Смещение вниз на расстояние одной клетки
    /// </summary>    
    public void MoveDown()
    {
        transform.Translate(new Vector3(0, 0, -1) * Maze.spacing, Space.World);
    }
    /// <summary>
    /// Смещение влево на расстояние одной клетки
    /// </summary>
    public void MoveLeft()
    {
        transform.Translate(new Vector3(-1, 0, 0) * Maze.spacing, Space.World);
    }

    //public void SetCoordinates(byte z, byte x)
    //{
    //    zIndex = z;
    //    xIndex = x;
    //}
    /// <summary>
    /// <c>DoWork</c> is a method in the <c>TestClass</c> class.
    /// </summary>
    /// <param name="number"></param>
    public void CreateRespawn(byte number) 
    {
        // 1-4 для респаунов, 0 для нереспаунов
            respawnNumber = number;
    }
    /// <summary>
    /// Устанавливает, находится ли на данной клетке игрок.
    /// </summary>
    /// <param name="Player"></param>
    public void SetCurrentPlayer(Player Player)
    {
        currentPlayer = Player;
    }
}
