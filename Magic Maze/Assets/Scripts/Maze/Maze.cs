using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Item;
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
        public float Spacing { get; } = 1.5f;
        /// <summary>
        /// Количество excess позиций вдоль одной стороны лабиринта.
        /// </summary>
        public int MovableRowsPerSide => (BoardSize - 1) / 2;
        /// <summary>
        /// Количество возможных позиций для ExcessTile.
        /// </summary>
        public int MovableRows => (BoardSize - 1) * 2;

        public Vector3[] extraPositions;
        
        public int NumberOfPlayers;
        

        private MazeGenerator mazeGenerator;
        private PlayerGenerator playerGenerator;
        private ItemGenerator itemGenerator;
        private PlayerManager playerManager;

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
            
            var buttons = GetComponent<Buttons>();
            buttons.moveColumn?.onClick.AddListener(MoveColumn);
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
        private void MoveColumn()
        {
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
        }

        /// <summary>
        /// Смещает ряд клеток вверх.
        /// </summary>
        private void MoveColumnUp()
        {
            var x = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Первый тайл становится эксесс
            var toBecomeExcessTile = GetTile(0, x);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
            newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
            newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
            newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
            toBecomeExcessTile.MoveUp();
        

            Destroy(toBecomeExcessTile);

            for (var z = 1; z < BoardSize; z++) // От второй до последней, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.zIndex--;
                SetTile(z - 1, x, currentTile);
                currentTile.MoveUp();
                if (currentTile.currentPlayer != null)
                {
                    currentTile.currentPlayer.transform.position = currentTile.transform.position;
                }
                if (currentTile.currentItem != null)
                {
                    currentTile.currentItem.transform.position = currentTile.transform.position;
                }
            }

            // Последний тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(BoardSize - 1, x);

            newTile.IsWallUp = excessTile.IsWallUp;
            newTile.IsWallRight = excessTile.IsWallRight;
            newTile.IsWallDown = excessTile.IsWallDown;
            newTile.IsWallLeft = excessTile.IsWallLeft;

            Destroy(excessTile);

            // Индекс уменьшался, чтобы перезаписать тайл в другую клетку.
            // Теперь возвращается обратно, т.к. конкретно эта клетка не перезаписывается.
            // Аналогично в других функциях смещения ряда.
            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.zIndex++;
            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.MoveUp();

            if (toBecomeExcessTile.currentPlayer != null)
            {
                toBecomeExcessTile.currentPlayer.CurrentTile = newTile;
                newTile.currentPlayer.transform.position = newTile.transform.position;
            }
            if (toBecomeExcessTile.currentItem != null)
            {
                toBecomeExcessTile.currentItem.CurrentTile = newTile;
                newTile.currentItem.transform.position = newTile.transform.position;
            }

            SetTile(BoardSize - 1, x, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 3 * MovableRowsPerSide - oldExtraPosId - 1;
        }
        /// <summary>
        /// Смещает ряд клеток вправо.
        /// </summary>
        private void MoveColumnRight()
        {
            var z = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Последний тайл становится эксесс
            var toBecomeExcessTile = GetTile(z, BoardSize - 1);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
            newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
            newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
            newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
            toBecomeExcessTile.MoveRight();
            Destroy(toBecomeExcessTile);

            for (var x = BoardSize - 2; x >= 0; x--) // От предпоследней до первой, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.xIndex++;
                SetTile(z, x + 1, currentTile);
                currentTile.MoveRight();
                if (currentTile.currentPlayer != null)
                {
                    currentTile.currentPlayer.transform.position = currentTile.transform.position;
                }
                if (currentTile.currentItem != null)
                {
                    currentTile.currentItem.transform.position = currentTile.transform.position;
                }
            }

            // Первый тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(z, 0);

            newTile.IsWallUp = excessTile.IsWallUp;
            newTile.IsWallRight = excessTile.IsWallRight;
            newTile.IsWallDown = excessTile.IsWallDown;
            newTile.IsWallLeft = excessTile.IsWallLeft;
            Destroy(excessTile);

            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.xIndex--;
            newTile.MoveRight();

            if (toBecomeExcessTile.currentPlayer != null)
            {
                toBecomeExcessTile.currentPlayer.CurrentTile = newTile;
                newTile.currentPlayer.transform.position = newTile.transform.position;
            }
            if (toBecomeExcessTile.currentItem != null)
            {
                toBecomeExcessTile.currentItem.CurrentTile = newTile;
                newTile.currentItem.transform.position = newTile.transform.position;
            }

            SetTile(z, 0, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;
        }
        /// <summary>
        /// Смещает ряд клеток вниз.
        /// </summary>
        private void MoveColumnDown()
        {
            var x = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Последний тайл становится эксесс
            var toBecomeExcessTile = GetTile(BoardSize - 1, x);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
            newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
            newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
            newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
            toBecomeExcessTile.MoveDown();
            Destroy(toBecomeExcessTile);

            for (var z = BoardSize - 2; z >= 0; z--) // От предпоследней до первой, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.zIndex++;
                SetTile(z + 1, x, currentTile);
                currentTile.MoveDown();
                if (currentTile.currentPlayer != null)
                {
                    currentTile.currentPlayer.transform.position = currentTile.transform.position;
                }
                if (currentTile.currentItem != null)
                {
                    currentTile.currentItem.transform.position = currentTile.transform.position;
                }
            }

            // Первый тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(0, x);

            newTile.IsWallUp = excessTile.IsWallUp;
            newTile.IsWallRight = excessTile.IsWallRight;
            newTile.IsWallDown = excessTile.IsWallDown;
            newTile.IsWallLeft = excessTile.IsWallLeft;
            Destroy(excessTile);

            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.zIndex--;
            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.MoveDown();

            if (toBecomeExcessTile.currentPlayer != null)
            {
                toBecomeExcessTile.currentPlayer.CurrentTile = newTile;
                newTile.currentPlayer.transform.position = newTile.transform.position;
            }
            if (toBecomeExcessTile.currentItem != null)
            {
                toBecomeExcessTile.currentItem.CurrentTile = newTile;
                newTile.currentItem.transform.position = newTile.transform.position;
            }

            SetTile(0, x, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 3 * MovableRowsPerSide - oldExtraPosId - 1;
        }
        /// <summary>
        /// Смещает ряд клеток влево.
        /// </summary>
        private void MoveColumnLeft()
        {
            var z = excessTile.GetRowNumber();
            var oldExtraPosId = excessTile.ExtraPosId;
            // Первый тайл становится эксесс
            var toBecomeExcessTile = GetTile(z, 0);
            var newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

            var newTile = excessTile.gameObject.AddComponent<MazeTile>();

            newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
            newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
            newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
            newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
            toBecomeExcessTile.MoveLeft();
            Destroy(toBecomeExcessTile);

            for (var x = 1; x < BoardSize; x++) // От второй до последней, против направления движения ряда
            {
                var currentTile = GetTile(z, x);
                currentTile.xIndex--;
                SetTile(z, x - 1, currentTile);
                currentTile.MoveLeft();
                if (currentTile.currentPlayer != null)
                {
                    currentTile.currentPlayer.transform.position = currentTile.transform.position;
                }
                if (currentTile.currentItem != null)
                {
                    currentTile.currentItem.transform.position = currentTile.transform.position;
                }
            }

            // Последний тайл становится обычным вместо эксесс
            var toBeReplacedByExcessTile = GetTile(z, BoardSize - 1);

            newTile.IsWallUp = excessTile.IsWallUp;
            newTile.IsWallRight = excessTile.IsWallRight;
            newTile.IsWallDown = excessTile.IsWallDown;
            newTile.IsWallLeft = excessTile.IsWallLeft;
            Destroy(excessTile);

            newTile.xIndex = toBeReplacedByExcessTile.xIndex;
            newTile.xIndex++;
            newTile.zIndex = toBeReplacedByExcessTile.zIndex;
            newTile.MoveLeft();

            if (toBecomeExcessTile.currentPlayer != null)
            {
                toBecomeExcessTile.currentPlayer.CurrentTile = newTile;
                newTile.currentPlayer.transform.position = newTile.transform.position;
            }
            if (toBecomeExcessTile.currentItem != null)
            {
                toBecomeExcessTile.currentItem.CurrentTile = newTile;
                newTile.currentItem.transform.position = newTile.transform.position;
            }

            SetTile(z, BoardSize - 1, newTile);
            excessTile = newExcessTile;
            excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;
        }
    }
}


