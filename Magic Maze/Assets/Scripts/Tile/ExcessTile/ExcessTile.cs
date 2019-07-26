using UnityEngine;


public class ExcessTile : Tile
{
    public int ExtraPosId
    {
        get => extraPosId;
        set
        {
            if (value < 0)
            {
                extraPosId = maze.MovableRows + (value % maze.MovableRows);
            }
            else 
            {
                extraPosId = value % maze.MovableRows;
            }
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
                    throw new System.ArgumentException("Parameter can only be 0-3, " +
                        $"but it's {ExtraPosId / maze.MovableRowsPerSide}");
            }
        }
    }

    [SerializeField] private int extraPosId;
    private Buttons buttons;

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
        bool wasWallUp = IsWallUp;

        IsWallUp = IsWallRight;
        IsWallRight = IsWallDown;
        IsWallDown = IsWallLeft;
        IsWallLeft = wasWallUp;
    }

    public void RotateClockwise()
    {
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
        Vector3 nextPosition = maze.extraPositions[ExtraPosId];
        transform.position = nextPosition;
        if (ExtraPosId % maze.MovableRowsPerSide == 0)
        {
            RotateCounterclockwise();
        }
    }

    /// <summary>
    /// Двигает клетку на предыдущую позицию
    /// </summary>
    public void MoveBackward()
    {
        if ((ExtraPosId % maze.MovableRowsPerSide) == 0)
        {
            RotateClockwise();
        }
        ExtraPosId--;
        Vector3 nextPosition = maze.extraPositions[ExtraPosId];
        transform.position = nextPosition;        
    }

    private void Start()
    {
        TileGenerator.GenerateRandomWalls(this);
    }

    private void OnEnable()
    {
        buttons = maze.GetComponent<Buttons>();
        buttons.moveExcessTileForward.onClick.AddListener(MoveForward);
        buttons.moveExcessTileBackward.onClick.AddListener(MoveBackward);
        buttons.RotateExcessTile.onClick.AddListener(RotateClockwise);
    }

    private void OnDisable()
    {
        buttons.moveExcessTileForward.onClick.RemoveListener(MoveForward);
        buttons.moveExcessTileBackward.onClick.RemoveListener(MoveBackward);
        buttons.RotateExcessTile.onClick.RemoveListener(RotateClockwise);
    }


}
