using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Managers;
using Tile.MazeTile;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        #region Variables

        public bool isIgnoringWalls;
        public List<Item.Item> ItemsToCollect = new List<Item.Item>();

        public Actions actions;
        
        public MazeTile CurrentTile
        {
            private get => currentTile;
            set
            {
                if (currentTile != null)
                {
                    currentTile.currentPlayer = null;
                }
                currentTile = value;
                currentTile.currentPlayer = this;
            }
        }
        
        private Maze.Maze maze;
        private PlayerManager playerManager;
        private TurnManager turnManager;
        private Buttons buttons;

        private MazeTile currentTile;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            var board = transform.parent;
            maze = board.GetComponent<Maze.Maze>();
            playerManager = board.GetComponent<PlayerManager>();
            var managers = GameObject.FindWithTag("Managers");
            turnManager = managers.GetComponent<TurnManager>();
            actions = new Actions();
            actions.PlayerMap.MoveUp.performed += ctx => Move(Direction.Up);
            actions.PlayerMap.MoveRight.performed += ctx => Move(Direction.Right);
            actions.PlayerMap.MoveDown.performed += ctx => Move(Direction.Down);
            actions.PlayerMap.MoveLeft.performed += ctx => Move(Direction.Left);
            actions.PlayerMap.EndPlayerTurn.performed += ctx => turnManager.SwitchTurn();
        }

        [SuppressMessage("ReSharper", "Unity.NoNullPropagation")]
        private void OnEnable()
        {
            buttons = maze.GetComponent<Buttons>();
            buttons.movePlayerUpButton?.onClick.AddListener(()    =>  Move(Direction.Up));
            buttons.movePlayerRightButton?.onClick.AddListener(() =>  Move(Direction.Right));
            buttons.movePlayerDownButton?.onClick.AddListener(()  =>  Move(Direction.Down));
            buttons.movePlayerLeftButton?.onClick.AddListener(()  =>  Move(Direction.Left));
        }

        [SuppressMessage("ReSharper", "Unity.NoNullPropagation")]
        private void OnDisable()
        {
            buttons.movePlayerUpButton?.onClick.RemoveListener(() => Move(Direction.Up));
            buttons.movePlayerRightButton?.onClick.RemoveListener(() => Move(Direction.Right));
            buttons.movePlayerDownButton?.onClick.RemoveListener(() => Move(Direction.Down));
            buttons.movePlayerLeftButton?.onClick.RemoveListener(() => Move(Direction.Left));
        }

        #endregion

        private void Move(Direction direction)
        {
            MazeTile nextTile = null;

            switch (direction)
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
        
            CurrentTile = nextTile;
            transform.position = CurrentTile.transform.position;
            TryCollectItem();
        }
        
        private void TryCollectItem()
        {
            var item = currentTile.currentItem;
            if (item != null && item == ItemsToCollect[0])
            {
                item.gameObject.SetActive(false);
                ItemsToCollect.RemoveAt(0);
            }
        }
    }
}
