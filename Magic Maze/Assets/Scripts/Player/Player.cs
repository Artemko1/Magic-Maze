using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Maze maze;
    public MazeTile currentTile;

    public bool isMovementAllowed;
    public bool isIgnoringWalls;

    void Awake()
    {
        maze = GameObject.FindGameObjectWithTag("Board").GetComponent<Maze>();
    }

    /// <summary>
    /// Убирает текущего игрока у текущей клетки
    /// и добавляет его переданной клетке.
    /// </summary>
    /// <param name="mazeTile"></param>
    public void ChangeCurrentTile(MazeTile mazeTile)
    {
        currentTile.currentPlayer = null;
        currentTile = mazeTile;
        currentTile.currentPlayer = this;
        
    }

    public void Move(Direction Direction)
    {
        if (!isMovementAllowed) { return; }
        
        MazeTile nextTile = null;

        switch (Direction)
        {
            case Direction.Up:
                Debug.Log(this + " Moving Up");
                if (currentTile.zIndex == 0)
                {
                    Debug.Log(this + " reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(currentTile.zIndex - 1, currentTile.xIndex);

                if (!isIgnoringWalls && (currentTile.IsWallUp || nextTile.IsWallDown))
                {
                    Debug.Log(this + " reached wall up.");
                    return;
                }
                break;
            case Direction.Right:
                Debug.Log(this + " Moving Right");

                if (currentTile.xIndex + 1 == maze.BoardSize)
                {
                    Debug.Log(this + "reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(currentTile.zIndex, currentTile.xIndex + 1);

                if (!isIgnoringWalls && (currentTile.IsWallRight || nextTile.IsWallLeft))
                {
                    Debug.Log(this + " reached wall right.");
                    return;
                }
                break;
            case Direction.Down:
                Debug.Log(this + " Moving Down");

                if (currentTile.zIndex + 1 == maze.BoardSize)
                {
                    Debug.Log(this + "reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(currentTile.zIndex + 1, currentTile.xIndex);

                if (!isIgnoringWalls && (currentTile.IsWallDown || nextTile.IsWallUp))
                {
                    Debug.Log(this + " reached wall down.");
                    return;
                }
                break;
            case Direction.Left:
                Debug.Log(this + " Moving Left");

                if (currentTile.xIndex == 0)
                {
                    Debug.Log(this + "reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(currentTile.zIndex, currentTile.xIndex - 1);

                if (!isIgnoringWalls && (currentTile.IsWallLeft || nextTile.IsWallRight))
                {
                    Debug.Log(this + " reached wall left.");
                    return;
                }
                break;
            default:
                Debug.LogError("Wrong move direction;");
                break;
        }
        
        ChangeCurrentTile(nextTile);
        transform.position = currentTile.transform.position;
    }

    public void AllowMovement()
    {
        isMovementAllowed = true;
    }
    public void DisallowMovement()
    {
        isMovementAllowed = false;
    }
}
