using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public Maze maze;
    [field: SerializeField]
    public MazeTile CurrentTile { get; private set; }

    public bool isMovementAllowed;
    public bool isIgnoringWalls;

    private Buttons buttons;

    /// <summary>
    /// Убирает текущего игрока у текущей клетки
    /// и добавляет его переданной клетке.
    /// </summary>
    /// <param name="mazeTile"></param>
    public void ChangeCurrentTile(MazeTile mazeTile)
    {
        if (CurrentTile != null)
        {
            CurrentTile.currentPlayer = null;
        }
        CurrentTile = mazeTile;
        CurrentTile.currentPlayer = this;        
    }

    public void Move(Direction Direction)
    {
        if (!isMovementAllowed) { return; }
        
        MazeTile nextTile = null;

        switch (Direction)
        {
            case Direction.Up:
                //Debug.Log(this + " Moving Up");
                if (CurrentTile.zIndex == 0)
                {
                    Debug.Log(this + " reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(CurrentTile.zIndex - 1, CurrentTile.xIndex);

                if (!isIgnoringWalls && (CurrentTile.IsWallUp || nextTile.IsWallDown))
                {
                    Debug.Log(this + " reached wall up.");
                    return;
                }
                break;
            case Direction.Right:
                //Debug.Log(this + " Moving Right");

                if (CurrentTile.xIndex + 1 == maze.BoardSize)
                {
                    Debug.Log(this + "reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(CurrentTile.zIndex, CurrentTile.xIndex + 1);

                if (!isIgnoringWalls && (CurrentTile.IsWallRight || nextTile.IsWallLeft))
                {
                    Debug.Log(this + " reached wall right.");
                    return;
                }
                break;
            case Direction.Down:
                //Debug.Log(this + " Moving Down");

                if (CurrentTile.zIndex + 1 == maze.BoardSize)
                {
                    Debug.Log(this + "reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(CurrentTile.zIndex + 1, CurrentTile.xIndex);

                if (!isIgnoringWalls && (CurrentTile.IsWallDown || nextTile.IsWallUp))
                {
                    Debug.Log(this + " reached wall down.");
                    return;
                }
                break;
            case Direction.Left:
                //Debug.Log(this + " Moving Left");

                if (CurrentTile.xIndex == 0)
                {
                    Debug.Log(this + "reached board boundary.");
                    return;
                }

                nextTile = maze.GetTile(CurrentTile.zIndex, CurrentTile.xIndex - 1);

                if (!isIgnoringWalls && (CurrentTile.IsWallLeft || nextTile.IsWallRight))
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
        transform.position = CurrentTile.transform.position;
    }

    public void AllowMovement()
    {
        isMovementAllowed = true;
    }
    public void DisallowMovement()
    {
        isMovementAllowed = false;
    }

    private void Awake()
    {
        maze = transform.parent.GetComponent<Maze>();
    }

    private void OnEnable()
    {
        buttons = maze.GetComponent<Buttons>();
        buttons.movePlayerUpButton?.onClick.AddListener(()    =>  Move(Direction.Up));
        buttons.movePlayerRightButton?.onClick.AddListener(() =>  Move(Direction.Right));
        buttons.movePlayerDownButton?.onClick.AddListener(()  =>  Move(Direction.Down));
        buttons.movePlayerLeftButton?.onClick.AddListener(()  =>  Move(Direction.Left));
    }

    private void OnDisable()
    {
        buttons.movePlayerUpButton?.onClick.RemoveListener(() => Move(Direction.Up));
        buttons.movePlayerRightButton?.onClick.RemoveListener(() => Move(Direction.Right));
        buttons.movePlayerDownButton?.onClick.RemoveListener(() => Move(Direction.Down));
        buttons.movePlayerLeftButton?.onClick.RemoveListener(() => Move(Direction.Left));
    }

}
