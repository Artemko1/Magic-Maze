using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerGenerator))]
public class Maze : MonoBehaviour {
    /// <summary>
    /// Скрипт лабиринта, прикрепленный к board.
    /// </summary>
    //public static Maze maze;
    private MazeGenerator mazeGenerator;
    private PlayerGenerator playerGenerator;

    public static float spacing = 1.5f;
    public static int boardSize = 9;
    /// <summary>
    /// Количество excess позиций вдоль одной стороны лабиринта.
    /// </summary>
    public static int MovableRowsPerSide
    {
        get
        {
            return (boardSize - 1) / 2;
        }
    }

    public static int MovableRows
    {
        get
        {
            return (boardSize - 1) * 2;
        }
    }
    /// <summary>
    /// Хранит в себе ссылки на все клетки лабиринта.
    /// </summary>
    [SerializeField]
    public Tile[] tileArray;
    
    public GameObject tilePrefab;
    private GameObject tileObj;

    public ExcessTile excessTile;
    public static Vector3[] extraPositions;

    public Button MoveExcessTileButton;

    void Awake()
    {
        mazeGenerator = GetComponent<MazeGenerator>();
        playerGenerator = GetComponent<PlayerGenerator>();
    }

    void Start ()
    {
        GenetareMaze();
    }
    /// <summary>
    /// Базовый скрипт для генерации лабиринта.
    /// Вызывается из Awake.
    /// </summary>
    private void GenetareMaze()
    {
        tileArray = mazeGenerator.GenerateTiles(boardSize);
        GeneratePlayers();
        GenerateExcessPositions();
        GenerateExcessTile(extraPositions[0]);
    }
    
    /// <summary>
    /// Создает всех игроков.
    /// </summary>
    private void GeneratePlayers()
    {
        playerGenerator.GeneratePlayer(0, 0, 1);
        playerGenerator.GeneratePlayer((byte)(boardSize - 1), 0, 2);
        playerGenerator.GeneratePlayer((byte)(boardSize - 1), (byte)(boardSize - 1), 3);
        playerGenerator.GeneratePlayer(0, (byte)(boardSize - 1), 4);
        
    }
    
    private void GenerateExcessPositions()
    {
        extraPositions = new Vector3[MovableRows];
        byte n = 0;
        byte z, x;

        x = 1;
        while (x < boardSize - 1)
        {
            SetExtraPosition((byte)(boardSize - 1), x, n, Direction.Down);
            x += 2;
            n++;
        }

        z = (byte)(boardSize - 2);
        while (z > 0 && z < boardSize)
        {
            SetExtraPosition(z, (byte)(boardSize - 1), n, Direction.Right);
            z -= 2;
            n++;
        }

        x = (byte)(boardSize - 2);
        while (x > 0 && x < boardSize)
        {
            SetExtraPosition(0, x, n, Direction.Up);
            x -= 2;
            n++;
        }

        z = 1;
        while (z < boardSize - 1)
        {
            SetExtraPosition(z, 0, n, Direction.Left);
            z += 2;
            n++;
        }
    }

    private void SetExtraPosition(byte z, byte x, byte n, Direction direction) // direction - направление смещения позиций.
    {
        switch (direction)
        {
            //Up
            case Direction.Up:
                extraPositions[n] = GetTile(z, x).transform.position + new Vector3(0, 0, 1) * spacing;
                break;
            //Right
            case Direction.Right:
                extraPositions[n] = GetTile(z, x).transform.position + new Vector3(1, 0, 0) * spacing;
                break;
            //Down
            case Direction.Down:
                extraPositions[n] = GetTile(z, x).transform.position + new Vector3(0, 0, -1) * spacing;
                break;
            //Left
            case Direction.Left:
                extraPositions[n] = GetTile(z, x).transform.position + new Vector3(-1, 0, 0) * spacing;
                break;
        }
    }

    private void GenerateExcessTile(Vector3 pos)
    {
        //excessTile.GenerateWalls();
        ExcessTileGenerator.GenerateWalls(excessTile);
        excessTile.transform.position = pos;
    }
        
    public void GenerateNewTiles() // Генерирует новые стенки всем клеткам лабиринта
    {
        for (byte z = 0; z < boardSize; z++)
        {
            for (byte x = 0; x < boardSize; x++)
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
        Tile toBecomeExcessTile = GetTile(0, x);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.isWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.isWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.isWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.isWallLeft;
        toBecomeExcessTile.MoveUp();
        Destroy(toBecomeExcessTile);

        for (int z = 1; z < boardSize; z++) // От второй до последней, против направления движения ряда
        {
            Tile currentTile = GetTile(z, x);
            currentTile.zIndex--;
            SetTile(z - 1, x, currentTile);
            currentTile.MoveUp();
        }

        // Последний тайл становится обычным вместо эксесс
        Tile toBeReplacedByExcessTile = GetTile(boardSize - 1, x);
        Tile newTile = excessTile.gameObject.AddComponent<Tile>();

        newTile.isWallUp = excessTile.IsWallUp;
        newTile.isWallRight = excessTile.IsWallRight;
        newTile.isWallDown = excessTile.IsWallDown;
        newTile.isWallLeft = excessTile.IsWallLeft;
        Destroy(excessTile);

        // Индекс уменьшался, чтобы перезаписать тайл в другую клетку.
        // Теперь возвращается обратно, т.к. конкретно эта клетка не перезаписывается.
        // Аналогично в других функциях смещения ряда.
        newTile.zIndex = toBeReplacedByExcessTile.zIndex;
        newTile.zIndex++;
        newTile.xIndex = toBeReplacedByExcessTile.xIndex;
        newTile.MoveUp();

        SetTile(boardSize - 1, x, newTile);
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
        Tile toBecomeExcessTile = GetTile(z, boardSize-1);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.isWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.isWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.isWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.isWallLeft;
        toBecomeExcessTile.MoveRight();
        Destroy(toBecomeExcessTile);

        for (int x = boardSize-2; x >= 0; x--) // От предпоследней до первой, против направления движения ряда
        {
            Tile currentTile = GetTile(z, x);
            currentTile.xIndex++;
            SetTile(z, x + 1, currentTile);
            currentTile.MoveRight();
        }

        // Первый тайл становится обычным вместо эксесс
        Tile toBeReplacedByExcessTile = GetTile(z, 0);
        Tile newTile = excessTile.gameObject.AddComponent<Tile>();

        newTile.isWallUp = excessTile.IsWallUp;
        newTile.isWallRight = excessTile.IsWallRight;
        newTile.isWallDown = excessTile.IsWallDown;
        newTile.isWallLeft = excessTile.IsWallLeft;
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
        Tile toBecomeExcessTile = GetTile(boardSize-1, x);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.isWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.isWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.isWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.isWallLeft;
        toBecomeExcessTile.MoveDown();
        Destroy(toBecomeExcessTile);

        for (int z = boardSize-2; z >= 0; z--) // От предпоследней до первой, против направления движения ряда
        {
            Tile currentTile = GetTile(z, x);
            currentTile.zIndex++;
            SetTile(z + 1, x, currentTile);
            currentTile.MoveDown();
        }

        // Первый тайл становится обычным вместо эксесс
        Tile toBeReplacedByExcessTile = GetTile(0, x);
        Tile newTile = excessTile.gameObject.AddComponent<Tile>();

        newTile.isWallUp = excessTile.IsWallUp;
        newTile.isWallRight = excessTile.IsWallRight;
        newTile.isWallDown = excessTile.IsWallDown;
        newTile.isWallLeft = excessTile.IsWallLeft;
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
        Tile toBecomeExcessTile = GetTile(z, 0);
        ExcessTile newExcessTile = toBecomeExcessTile.gameObject.AddComponent<ExcessTile>();

        newExcessTile.IsWallUp = toBecomeExcessTile.isWallUp;
        newExcessTile.IsWallRight = toBecomeExcessTile.isWallRight;
        newExcessTile.IsWallDown = toBecomeExcessTile.isWallDown;
        newExcessTile.IsWallLeft = toBecomeExcessTile.isWallLeft;
        toBecomeExcessTile.MoveLeft();
        Destroy(toBecomeExcessTile);

        for (int x = 1; x < boardSize; x++) // От второй до последней, против направления движения ряда
        {
            Tile currentTile = GetTile(z, x);
            currentTile.xIndex--;
            SetTile(z, x - 1, currentTile);
            currentTile.MoveLeft();
        }

        // Последний тайл становится обычным вместо эксесс
        Tile toBeReplacedByExcessTile = GetTile(z, boardSize - 1);
        Tile newTile = excessTile.gameObject.AddComponent<Tile>();

        newTile.isWallUp = excessTile.IsWallUp;
        newTile.isWallRight = excessTile.IsWallRight;
        newTile.isWallDown = excessTile.IsWallDown;
        newTile.isWallLeft = excessTile.IsWallLeft;
        Destroy(excessTile);

        newTile.xIndex = toBeReplacedByExcessTile.xIndex;
        newTile.xIndex++;
        newTile.zIndex = toBeReplacedByExcessTile.zIndex;
        newTile.MoveLeft();

        SetTile(z, boardSize - 1, newTile);
        excessTile = newExcessTile;
        excessTile.ExtraPosId = 5 * MovableRowsPerSide - oldExtraPosId - 1;

        MoveExcessTileButton.onClick.RemoveAllListeners();
        MoveExcessTileButton.onClick.AddListener(() => excessTile.MoveForward());
    }

    /// <summary>
    /// Присваивает переданный tile в массив класса Maze
    /// </summary>
    /// <param name="z">Номер строки клетки.</param>
    /// <param name="x">Номер столбца клетки.</param>
    /// <param name="tile"></param>
    public void SetTile(int z, int x, Tile tile)
    {
        // 2D representation stored in row-major order.
        tileArray[z * boardSize + x] = tile;
    }
    /// <summary>
    /// Присваивает переданный tile в переданный массив клеток лабиринта.
    /// </summary>
    /// <param name="tileArray">Массив, в который присваивается клетка.</param>
    /// <param name="z">Номер строки клетки.</param>
    /// <param name="x">Номер столбца клетки.</param>
    /// <param name="tile"></param>
    public static void SetTile(Tile[] tileArray, int z, int x, Tile tile)
    {
        // 2D representation stored in row-major order.
        tileArray[z * boardSize + x] = tile;
    }
    /// <summary>
    /// Возвращает клетку из лабиринта по её координатам.
    /// </summary>
    /// <param name="z">Номер строки клетки.</param>
    /// <param name="x">Номер столбца клетки.</param>
    /// <returns></returns>
    public Tile GetTile(int z, int x)
    {
        return tileArray[z * boardSize + x];
    }
}


