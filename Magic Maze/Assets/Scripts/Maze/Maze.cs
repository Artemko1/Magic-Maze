using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Item;
using Managers;
using Player;
using Tile;
using Tile.ExcessTile;
using Tile.MazeTile;
using UI;
using UnityEngine;

namespace Maze
{
    [RequireComponent(typeof(Buttons))]
    [SuppressMessage("ReSharper", "Unity.NoNullPropagation")]
    public class Maze : MonoBehaviour
    {
        #region Variables

        public int BoardSize { get; } = 9;
        public float Spacing { get; } = 2f;
        /// <summary>
        /// Количество excess позиций вдоль одной стороны лабиринта.
        /// </summary>
        public int MovableRowsPerSide => (BoardSize - 1) / 2;
        /// <summary>
        /// Количество возможных позиций для ExcessTile.
        /// </summary>
        public int MovableRows => (BoardSize - 1) * 2;

        public Vector3[] extraPositions;

        public ExcessTile ExcessTile => excessTile;

        public int NumberOfPlayers;

        public Actions actions;

        private MazeGenerator mazeGenerator;
        private PlayerGenerator playerGenerator;
        private ItemGenerator itemGenerator;
        private PlayerManager playerManager;
        private TurnManager turnManager;

        [SerializeField] private MazeTile[] tileArray;
        [SerializeField] private ExcessTile excessTile;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            mazeGenerator = GetComponent<MazeGenerator>();
            playerGenerator = GetComponent<PlayerGenerator>();
            itemGenerator = GetComponent<ItemGenerator>();
            playerManager = GetComponent<PlayerManager>();
            var managers = GameObject.FindWithTag("Managers");
            turnManager = managers.GetComponent<TurnManager>();
            
            var buttons = GetComponent<Buttons>();
            buttons.moveColumn?.onClick.AddListener(MoveColumn);
            
            actions = new Actions();
        }

        private void Start()
        {
            tileArray = new MazeTile[BoardSize * BoardSize];
            mazeGenerator?.GenerateTiles(tileArray);

            mazeGenerator?.GenerateExcessPositions();
            excessTile.transform.position = extraPositions[0];
            TileGenerator.GenerateRandomWalls(excessTile);

            playerGenerator?.GeneratePlayers(NumberOfPlayers);

            itemGenerator?.GenerateItems();

            playerManager.AssignItemsToCollect();
            turnManager.InitializeFirstTurn();
        }

        #endregion

        /// <summary>
        /// Присваивает переданный tile в массив объекта.
        /// </summary>
        /// <param name="z">Номер строки клетки.</param>
        /// <param name="x">Номер столбца клетки.</param>
        /// <param name="mazeTile"></param>
        public void SetTile(int z, int x, MazeTile mazeTile)
        {
            // 2D representation stored in row-major order.
            tileArray[z * BoardSize + x] = mazeTile;
        }
        /// <summary>
        /// Возвращает клетку из лабиринта по её координатам.
        /// </summary>
        /// <param name="z">Номер строки клетки.</param>
        /// <param name="x">Номер столбца клетки.</param>
        /// <returns></returns>
        public MazeTile GetTile(int z, int x)
        {
            return tileArray[z * BoardSize + x];
        }
        public MazeTile GetTile((int, int) p)
        {
            var (z, x) = p;
            return tileArray[z * BoardSize + x];
        }


        /// <summary>
        /// Смещает ряд клеток.
        /// ExcessTile оказывается с противоположной стороны
        /// </summary>
        public void MoveColumn()
        {
            if (turnManager.CurrentPhase != TurnPhase.ColumnMove)
            {
                Debug.LogError("Column move should not be called in not it's turn.'");
                return;
            }
            switch (excessTile.CurrentDirection)
            {
                case Direction.Down:
                    MoveColumnUp();
                    break;
                case Direction.Right:
                    MoveColumnLeft();
                    break;
                case Direction.Up:
                    MoveColumnDown();
                    break;
                case Direction.Left:
                    MoveColumnRight();
                    break;
            }
            turnManager.SwitchTurn();
        }

        private void MoveColumnUp()
        {
            var x = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Первый тайл становится эксесс
            var toBecomeExcessTile = GetTile(0, x);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            CopyTileWalls(toBecomeExcessTile, newExcessTile);
            
            toBecomeExcessTile.MoveUp();

            Destroy(toBecomeExcessTile);

            for (var z = 1; z < BoardSize; z++) // От второй до последней, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.zIndex--;
                SetTile(z - 1, x, currentTile);
                currentTile.MoveUp();
            }

            // Последний тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(BoardSize - 1, x);
            
            CopyTileWalls(excessTile, newTile);

            Destroy(excessTile);

            // Индекс уменьшался, чтобы перезаписать тайл в другую клетку.
            // Теперь возвращается обратно, т.к. конкретно эта клетка не перезаписывается.
            // Аналогично в других функциях смещения ряда.
            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.zIndex++;
            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.MoveUp();

            AlsoMovePlayerAndItem(toBecomeExcessTile, newTile);
            

            SetTile(BoardSize - 1, x, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 3 * MovableRowsPerSide - oldExtraPosId - 1;
        }

        private void MoveColumnRight()
        {
            var z = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Последний тайл становится эксесс
            var toBecomeExcessTile = GetTile(z, BoardSize - 1);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            CopyTileWalls(toBecomeExcessTile, newExcessTile);

            toBecomeExcessTile.MoveRight();
            
            Destroy(toBecomeExcessTile);

            for (var x = BoardSize - 2; x >= 0; x--) // От предпоследней до первой, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.xIndex++;
                SetTile(z, x + 1, currentTile);
                currentTile.MoveRight();
            }

            // Первый тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(z, 0);

            CopyTileWalls(excessTile, newTile);
            
            Destroy(excessTile);

            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.xIndex--;
            newTile.MoveRight();

            AlsoMovePlayerAndItem(toBecomeExcessTile, newTile);

            SetTile(z, 0, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;
        }

        private void MoveColumnDown()
        {
            var x = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Последний тайл становится эксесс
            var toBecomeExcessTile = GetTile(BoardSize - 1, x);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            CopyTileWalls(toBecomeExcessTile, newExcessTile);            

            toBecomeExcessTile.MoveDown();
            
            Destroy(toBecomeExcessTile);

            for (var z = BoardSize - 2; z >= 0; z--) // От предпоследней до первой, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.zIndex++;
                SetTile(z + 1, x, currentTile);
                currentTile.MoveDown();
            }

            // Первый тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(0, x);

            CopyTileWalls(excessTile, newTile);
            
            Destroy(excessTile);

            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.zIndex--;
            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.MoveDown();

            AlsoMovePlayerAndItem(toBecomeExcessTile, newTile);

            SetTile(0, x, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 3 * MovableRowsPerSide - oldExtraPosId - 1;
        }

        private void MoveColumnLeft()
        {
            var z = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Первый тайл становится эксесс
            var toBecomeExcessTile = GetTile(z, 0);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            CopyTileWalls(toBecomeExcessTile, newExcessTile);
            
            toBecomeExcessTile.MoveLeft();
            
            Destroy(toBecomeExcessTile);

            for (var x = 1; x < BoardSize; x++) // От второй до последней, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.xIndex--;
                SetTile(z, x - 1, currentTile);
                currentTile.MoveLeft();
            }

            // Последний тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(z, BoardSize - 1);

            CopyTileWalls(excessTile, newTile);
            
            Destroy(excessTile);

            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.xIndex++;
            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.MoveLeft();

            AlsoMovePlayerAndItem(toBecomeExcessTile, newTile);

            SetTile(z, BoardSize - 1, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;
        }


       
        /// <summary>
        /// Для перемещения когда mazeTile выдвигается из лабиринта.
        /// </summary>
        /// <param name="fromTile"></param>
        /// <param name="toTile"></param>
        private static void AlsoMovePlayerAndItem(MazeTile fromTile, MazeTile toTile)
        {
            if (fromTile.currentPlayer != null)
            {
                fromTile.currentPlayer.CurrentTile = toTile;
                toTile.currentPlayer.transform.position = toTile.transform.position;
            }
            if (fromTile.currentItem != null)
            {
                fromTile.currentItem.CurrentTile = toTile;
                toTile.currentItem.transform.position = toTile.transform.position;
            }
        }


        private static void CopyTileWalls(Tile.Tile oldTile, Tile.Tile newTile)
        {
            newTile.IsWallUp = oldTile.IsWallUp;
            newTile.IsWallRight = oldTile.IsWallRight;
            newTile.IsWallDown = oldTile.IsWallDown;
            newTile.IsWallLeft = oldTile.IsWallLeft;
        }
    }
}


