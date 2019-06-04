using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class ExcessTile : MonoBehaviour
{
    private GameObject upWallObject;
    private GameObject rightWallObject;
    private GameObject downWallObject;
    private GameObject leftWallObject;

    [SerializeField]
    private bool isWallUp;
    [SerializeField]
    private bool isWallRight;
    [SerializeField]
    private bool isWallDown;
    [SerializeField]
    private bool isWallLeft;
    [SerializeField]
    private int extraPosId;

    public bool IsWallUp
    {
        get => isWallUp;
        set
        {
            isWallUp = value;
            Debug.Log("Inside up setter");
            upWallObject?.SetActive(value);
        }
    }
    public bool IsWallRight
    {
        get => isWallRight;
        set
        {
            isWallRight = value;
            Debug.Log("Inside right setter");
            rightWallObject?.SetActive(value);
        }
    }
    public bool IsWallDown
    {
        get => isWallDown;
        set
        {
            isWallDown = value;
            Debug.Log("Inside down setter");
            downWallObject?.SetActive(value);
        }
    }
    public bool IsWallLeft
    {
        get => isWallLeft;
        set
        {
            isWallLeft = value;
            Debug.Log("Inside left setter");
            leftWallObject?.SetActive(value);
        }
    }

    public int ExtraPosId { get => extraPosId; set => extraPosId = value; }
    public Direction CurrentDirection
    {
        get
        {
            switch ((ExtraPosId / Maze.MovableRowsPerSide))
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

    private void Awake()
    {
        upWallObject    = transform.Find("Plate Visual").Find("Wall Up").gameObject;
        rightWallObject = transform.Find("Plate Visual").Find("Wall Right").gameObject;
        downWallObject  = transform.Find("Plate Visual").Find("Wall Down").gameObject;
        leftWallObject = transform.Find("Plate Visual").Find("Wall Left").gameObject;
    }

    /// <summary>
    /// Выводит в консоль положение относительно лабиринта.
    /// </summary>
    public void DisplayCurrentDirection()
    {
        Debug.Log("Current direction is " + CurrentDirection);
    }

    /// <summary>
    /// Возвращает координату z или x лабиринта, где сейчас находится ExcessTile
    /// </summary>
    /// <returns></returns>
    public int GetRowNumber()
    {
        switch (CurrentDirection)
        {
            case Direction.Down:
                return ExtraPosId * 2 + 1;
            case Direction.Right:
                return Maze.boardSize - 1 - ((ExtraPosId - Maze.MovableRowsPerSide) * 2 + 1);
            case Direction.Up:
                return Maze.boardSize - 1 - ((ExtraPosId - 2 * Maze.MovableRowsPerSide) * 2 + 1);
            case Direction.Left:
                return (ExtraPosId - 3 * Maze.MovableRowsPerSide) * 2 + 1;
            default:
                throw new System.ArgumentException("Wrong direction in CurrentDirection");
        }

    }

    /// <summary>
    /// Поворачивает клетку на 90* против часовой стрелки.
    /// Вызывается при движении клетки против часовой стрелки вокруг лабиринта.
    /// </summary>
    public void RotateCounterclockwise()
    {
        //transform.Rotate(0, -90f, 0);
        Debug.Log("RotateCounterclockwise called");
        bool wasWallUp = IsWallUp;

        IsWallUp = IsWallRight;
        IsWallRight = IsWallDown;
        IsWallDown = IsWallLeft;
        IsWallLeft = wasWallUp;
    }

    public void RotateClockwise()
    {
        //transform.Rotate(0, 90f, 0);
        Debug.Log("RotateClockwise called");
        bool wasWallUp = IsWallUp;

        IsWallUp = IsWallLeft;
        IsWallLeft = IsWallDown;
        IsWallDown = IsWallRight;
        IsWallRight = wasWallUp;
    }

    /// <summary>
    /// Двигает клетку на следующую позицию
    /// </summary>
    public void MoveForward()
    {
        ExtraPosId++;
        if (ExtraPosId == Maze.MovableRows)
            ExtraPosId = 0;
        Vector3 nextPosition = Maze.extraPositions[ExtraPosId];
        transform.position = nextPosition;
        if (ExtraPosId % 4 == 0)
            RotateCounterclockwise();
    }
}
