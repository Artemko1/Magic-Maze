using Tile.MazeTile;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        public Maze.Maze maze;
        [FormerlySerializedAs("CurrentTile")] [field: SerializeField] private MazeTile currentTile;

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
            if (currentTile != null)
            {
                currentTile.currentPlayer = null;
            }
            currentTile = mazeTile;
            currentTile.currentPlayer = this;        
        }

        public void Move(Direction direction)
        {
            if (!isMovementAllowed) { return; }
        
            MazeTile nextTile = null;

            switch (direction)
            {
                case Direction.Up:
                    //Debug.Log(this + " Moving Up");
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
                    //Debug.Log(this + " Moving Right");

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
                    //Debug.Log(this + " Moving Down");

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
                    //Debug.Log(this + " Moving Left");

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

        private void Awake()
        {
            maze = transform.parent.GetComponent<Maze.Maze>();
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
}
