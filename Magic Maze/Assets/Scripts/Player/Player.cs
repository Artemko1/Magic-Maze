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

    public void SetCurrentTile(byte x, byte z)
    {
        currentTile = maze.GetTile(z, x);
        
    }

    public void Move(Direction Direction)
    {
        if (isMovementAllowed == false)
        {
            return;
        }

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
        
        currentTile.currentPlayer = null;
        currentTile = nextTile;
        currentTile.currentPlayer = this;
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
