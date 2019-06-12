using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerGenerator))]
[RequireComponent(typeof(MazeGenerator))]
public class Maze : MonoBehaviour
{
    private MazeGenerator mazeGenerator;
    private PlayerGenerator playerGenerator;

    private float spacing = 1.5f;

    /// <summary>
    /// Хранит в себе ссылки на все клетки лабиринта.
    /// </summary>
    [SerializeField]
    private MazeTile[] tileArray;
    [SerializeField]
    private ExcessTile excessTile;
    public Vector3[] extraPositions;

    [SerializeField]
    private Button moveExcessTileButton;

    public int BoardSize { get; } = 9;
    /// <summary>
    /// Количество excess позиций вдоль одной стороны лабиринта.
    /// </summary>
    public int MovableRowsPerSide
    {
        get => (BoardSize - 1) / 2;
    }
    /// <summary>
    /// Количество возможных позиций для ExcessTile.
    /// </summary>
    public int MovableRows
    {
        get => (BoardSize - 1) * 2;
    }
    public Button MoveExcessTileButton { get => moveExcessTileButton; set => moveExcessTileButton = value; }
    public float Spacing { get => spacing; set => spacing = value; }

    void Awake()
    {
        mazeGenerator = GetComponent<MazeGenerator>();
        playerGenerator = GetComponent<PlayerGenerator>();
    }

    void Start()
    {
        tileArray = mazeGenerator.GenerateTiles(BoardSize);
        GeneratePlayers();
        mazeGenerator.GenerateExcessPositions();
        excessTile.transform.position = extraPositions[0];
    }

    /// <summary>
    /// Создает всех игроков.
    /// </summary>
    private void GeneratePlayers()
    {
        playerGenerator.GeneratePlayer(0, 0, 1);
        playerGenerator.GeneratePlayer((byte)(BoardSize - 1), 0, 2);
        playerGenerator.GeneratePlayer((byte)(BoardSize - 1), (byte)(BoardSize - 1), 3);
        playerGenerator.GeneratePlayer(0, (byte)(BoardSize - 1), 4);

    }
    public void GenerateNewTiles() // Генерирует новые стенки всем клеткам лабиринта
    {
        for (byte z = 0; z < BoardSize; z++)
        {
            for (byte x = 0; x < BoardSize; x++)
            {
                TileGenerator.GenerateWalls(GetTile(z, x));
            }
        }
    }
    /// <summary>
    /// Смещает ряд клеток.
    /// ExcessTile оказывается с противоположной стороны
    /// </summary>
    public void MoveColumn()
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
        int x = excessTile.GetRowNumber();
        int oldExtraPosId = excessTile.ExtraPosId;
        // Первый тайл становится эксесс
        MazeTile toBecomeExcessTile = GetTile(0, x);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
        toBecomeExcessTile.MoveUp();
        Destroy(toBecomeExcessTile);

        for (int z = 1; z < BoardSize; z++) // От второй до последней, против направления движения ряда
        {
            MazeTile currentTile = GetTile(z, x);
            currentTile.zIndex--;
            SetTile(z - 1, x, currentTile);
            currentTile.MoveUp();
        }

        // Последний тайл становится обычным вместо эксесс
        MazeTile toBeReplacedByExcessTile = GetTile(BoardSize - 1, x);
        MazeTile newTile = excessTile.gameObject.AddComponent<MazeTile>();

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

        SetTile(BoardSize - 1, x, newTile);
        excessTile = newExcessTile;
        excessTile.ExtraPosId = 3 * MovableRowsPerSide - oldExtraPosId - 1;

        MoveExcessTileButton.onClick.RemoveAllListeners();
        MoveExcessTileButton.onClick.AddListener(() => excessTile.MoveForward());
    }
    /// <summary>
    /// Смещает ряд клеток вправо.
    /// </summary>
    private void MoveColumnRight()
    {
        int z = excessTile.GetRowNumber();
        int oldExtraPosId = excessTile.ExtraPosId;
        // Последний тайл становится эксесс
        MazeTile toBecomeExcessTile = GetTile(z, BoardSize - 1);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
        toBecomeExcessTile.MoveRight();
        Destroy(toBecomeExcessTile);

        for (int x = BoardSize - 2; x >= 0; x--) // От предпоследней до первой, против направления движения ряда
        {
            MazeTile currentTile = GetTile(z, x);
            currentTile.xIndex++;
            SetTile(z, x + 1, currentTile);
            currentTile.MoveRight();
        }

        // Первый тайл становится обычным вместо эксесс
        MazeTile toBeReplacedByExcessTile = GetTile(z, 0);
        MazeTile newTile = excessTile.gameObject.AddComponent<MazeTile>();

        newTile.IsWallUp = excessTile.IsWallUp;
        newTile.IsWallRight = excessTile.IsWallRight;
        newTile.IsWallDown = excessTile.IsWallDown;
        newTile.IsWallLeft = excessTile.IsWallLeft;
        Destroy(excessTile);

        newTile.zIndex = toBeReplacedByExcessTile.zIndex;
        newTile.xIndex = toBeReplacedByExcessTile.xIndex;
        newTile.xIndex--;
        newTile.MoveRight();


        SetTile(z, 0, newTile);
        excessTile = newExcessTile;
        excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;

        MoveExcessTileButton.onClick.RemoveAllListeners();
        MoveExcessTileButton.onClick.AddListener(() => excessTile.MoveForward());
    }
    /// <summary>
    /// Смещает ряд клеток вниз.
    /// </summary>
    private void MoveColumnDown()
    {
        int x = excessTile.GetRowNumber();
        int oldExtraPosId = excessTile.ExtraPosId;
        // Последний тайл становится эксесс
        MazeTile toBecomeExcessTile = GetTile(BoardSize - 1, x);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
        toBecomeExcessTile.MoveDown();
        Destroy(toBecomeExcessTile);

        for (int z = BoardSize - 2; z >= 0; z--) // От предпоследней до первой, против направления движения ряда
        {
            MazeTile currentTile = GetTile(z, x);
            currentTile.zIndex++;
            SetTile(z + 1, x, currentTile);
            currentTile.MoveDown();
        }

        // Первый тайл становится обычным вместо эксесс
        MazeTile toBeReplacedByExcessTile = GetTile(0, x);
        MazeTile newTile = excessTile.gameObject.AddComponent<MazeTile>();

        newTile.IsWallUp = excessTile.IsWallUp;
        newTile.IsWallRight = excessTile.IsWallRight;
        newTile.IsWallDown = excessTile.IsWallDown;
        newTile.IsWallLeft = excessTile.IsWallLeft;
        Destroy(excessTile);

        newTile.zIndex = toBeReplacedByExcessTile.zIndex;
        newTile.zIndex--;
        newTile.xIndex = toBeReplacedByExcessTile.xIndex;
        newTile.MoveDown();

        SetTile(0, x, newTile);
        excessTile = newExcessTile;
        excessTile.ExtraPosId = 3 * MovableRowsPerSide - oldExtraPosId - 1;

        MoveExcessTileButton.onClick.RemoveAllListeners();
        MoveExcessTileButton.onClick.AddListener(() => excessTile.MoveForward());
    }
    /// <summary>
    /// Смещает ряд клеток влево.
    /// </summary>
    private void MoveColumnLeft()
    {
        int z = excessTile.GetRowNumber();
        int oldExtraPosId = excessTile.ExtraPosId;
        // Первый тайл становится эксесс
        MazeTile toBecomeExcessTile = GetTile(z, 0);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.IsWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.IsWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.IsWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.IsWallLeft;
        toBecomeExcessTile.MoveLeft();
        Destroy(toBecomeExcessTile);

        for (int x = 1; x < BoardSize; x++) // От второй до последней, против направления движения ряда
        {
            MazeTile currentTile = GetTile(z, x);
            currentTile.xIndex--;
            SetTile(z, x - 1, currentTile);
            currentTile.MoveLeft();
        }

        // Последний тайл становится обычным вместо эксесс
        MazeTile toBeReplacedByExcessTile = GetTile(z, BoardSize - 1);
        MazeTile newTile = excessTile.gameObject.AddComponent<MazeTile>();

        newTile.IsWallUp = excessTile.IsWallUp;
        newTile.IsWallRight = excessTile.IsWallRight;
        newTile.IsWallDown = excessTile.IsWallDown;
        newTile.IsWallLeft = excessTile.IsWallLeft;
        Destroy(excessTile);

        newTile.xIndex = toBeReplacedByExcessTile.xIndex;
        newTile.xIndex++;
        newTile.zIndex = toBeReplacedByExcessTile.zIndex;
        newTile.MoveLeft();

        SetTile(z, BoardSize - 1, newTile);
        excessTile = newExcessTile;
        excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;

        MoveExcessTileButton.onClick.RemoveAllListeners();
        MoveExcessTileButton.onClick.AddListener(() => excessTile.MoveForward());
    }

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
    /// Присваивает переданный tile в переданный массив клеток лабиринта.
    /// </summary>
    /// <param name="z">Номер строки клетки.</param>
    /// <param name="x">Номер столбца клетки.</param>
    /// <param name="mazeTile"></param>
    /// <param name="tileArray">Массив, в который присваивается клетка.</param>
    public void SetTile(int z, int x, MazeTile mazeTile, MazeTile[] tileArray)
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
}


