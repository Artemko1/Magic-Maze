using UnityEngine;
[SelectionBase]
public class ExcessTile : Tile
{
    [SerializeField]
    private int extraPosId;

    public int ExtraPosId
    {
        get => extraPosId;
        set
        {
            extraPosId = value;
        }
    }

    public Direction CurrentDirection
    {
        get
        {
            switch ((ExtraPosId / maze.MovableRowsPerSide))
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
                    throw new System.ArgumentException("Parameter can only be 0-3",
                            " extraPosId/Maze.MovableRowsPerSide");
            }
        }
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
                return maze.BoardSize - 1 - ((ExtraPosId - maze.MovableRowsPerSide) * 2 + 1);
            case Direction.Up:
                return maze.BoardSize - 1 - ((ExtraPosId - 2 * maze.MovableRowsPerSide) * 2 + 1);
            case Direction.Left:
                return (ExtraPosId - 3 * maze.MovableRowsPerSide) * 2 + 1;
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
        Debug.Log("RotateCounterclockwise called");
        bool wasWallUp = IsWallUp;

        IsWallUp = IsWallRight;
        IsWallRight = IsWallDown;
        IsWallDown = IsWallLeft;
        IsWallLeft = wasWallUp;
    }

    public void RotateClockwise()
    {
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
        if (ExtraPosId == maze.MovableRows)
            ExtraPosId = 0;
        Vector3 nextPosition = maze.extraPositions[ExtraPosId];
        transform.position = nextPosition;
        if (ExtraPosId % 4 == 0)
            RotateCounterclockwise();
    }
}
