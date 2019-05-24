using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class ExcessTile : MonoBehaviour {

    public bool isWallUp;
    public bool isWallRight;
    public bool isWallDown;
    public bool isWallLeft;

    public int extraPosId;
    public Direction CurrentDirection
    {
        get
        {
             switch ((extraPosId / Maze.MovableRowsPerSide))
            {
                case 0:
                    return Direction.Down;
                case 1:
                    return Direction.Right;
                case 2:
                    return Direction.Up;
                case 3:
                    return Direction.Left;
                default:
                    throw new System.ArgumentException("Parameter can only be 0-3", " extraPosId/Maze.MovableRowsPerSide");
            }            
        }
    }

    
    /// <summary>
    /// Выводит в консоль положение относительно лабиринта.
    /// </summary>
    public void DisplayCurrentDirection()
    {
        Debug.Log("Current direction is " + CurrentDirection);
    }

    public int GetRowNumber() // Возвращает координату z или x лабиринта, где сейчас находится ExcessTile
    {
        switch (CurrentDirection)
        {
            case Direction.Down:
                return extraPosId * 2 + 1;
            case Direction.Right:
                return Maze.boardSize - 1 - ((extraPosId - Maze.MovableRowsPerSide) * 2 + 1);
            case Direction.Up:
                return Maze.boardSize - 1 - ((extraPosId - 2 * Maze.MovableRowsPerSide) * 2 + 1);
            case Direction.Left:
                return (extraPosId - 3 * Maze.MovableRowsPerSide) * 2 + 1;
            default:
                throw new System.ArgumentException("Wrong direction in CurrentDirection");
        }
        
    }

    /// <summary>
    /// Поворачивает клетку на 90* против часовой стрелки.
    /// </summary>
    public void RotateCounterclockwise()
    {
        transform.Rotate(0,-90f, 0);
        Debug.Log("RotateCounterclockwise called");
        bool wasWallUp = isWallUp;

        isWallUp = isWallRight;
        isWallRight = isWallDown;
        isWallDown = isWallLeft;
        isWallLeft = wasWallUp;
    }

    public void RotateClockwise()
    {
        transform.Rotate(0, 90f, 0);
        Debug.Log("RotateClockwise called");
        bool wasWallUp = isWallUp;

        isWallUp = isWallLeft;
        isWallLeft = isWallDown ;
        isWallDown = isWallRight;
        isWallRight = wasWallUp;
    }

    /// <summary>
    /// Двигает клетку на следующую позицию
    /// </summary>
    public void MoveForward()
    {
        extraPosId++;
        if (extraPosId == Maze.MovableRows)
            extraPosId = 0;
        Vector3 nextPosition = Maze.extraPositions[extraPosId];
        transform.position = nextPosition;
        if (extraPosId % 4 == 0)
            RotateCounterclockwise();
    }

    public void GenerateWalls()
    {
        if (Random.value <= 0.6)
        {
            GenerateOppositeWalls();
        }
        else
        {
            GenerateCornerWalls();
        }
    }

    void GenerateOppositeWalls()
    {
        if (Random.value <= 0.5)
        {
            // Верхнюю и нижнюю
            ActivateWalls(true, false, true, false);
        }
        else
        {
            // Правую и левую
            ActivateWalls(false, true, false, true);
        }
    }

    void GenerateCornerWalls()
    {
        if (Random.value <= 0.25)
        {
            // Верхнюю и правую
            ActivateWalls(true, true, false, false);
        }
        else if (Random.value <= 0.5)
        {
            // Правую и нижнюю
            ActivateWalls(false, true, true, false);
        }
        else if (Random.value <= 0.75)
        {
            // Нижнюю и левую
            ActivateWalls(false, false, true, true);
        }
        else
        {
            // Левую и верхнюю
            ActivateWalls(true, false, false, true);
        }
    }

    void ActivateWalls(bool up, bool right, bool down, bool left)
    {
        isWallUp = up;
        isWallRight = right;
        isWallDown = down;
        isWallLeft = left;

        if (isWallUp)
        {
            SetWallVisual("Wall Up", true);
        }
        else
        {
            SetWallVisual("Wall Up", false);
        }

        if (isWallRight)
        {
            SetWallVisual("Wall Right", true);
        }
        else
        {
            SetWallVisual("Wall Right", false);
        }
        if (isWallDown)
        {
            SetWallVisual("Wall Down", true);
        }
        else
        {
            SetWallVisual("Wall Down", false);
        }
        if (isWallLeft)
        {
            SetWallVisual("Wall Left", true);
        }
        else
        {
            SetWallVisual("Wall Left", false);
        }
    }

    void SetWallVisual(string wallName, bool set)
    {
        transform.Find("Plate Visual").Find(wallName).gameObject.SetActive(set);
    }
}
