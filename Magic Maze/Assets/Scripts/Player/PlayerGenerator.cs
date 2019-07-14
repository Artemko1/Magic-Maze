using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGenerator : MonoBehaviour
{
    public Maze maze;
    public GameObject playerPrefab;

    private GameObject playerObj;

    /// <summary>
    /// Создает всех игроков.
    /// </summary>
    public void GeneratePlayers()
    {
        GeneratePlayer(0, 0, 1);
        GeneratePlayer((byte)(maze.BoardSize - 1), 0, 2);
        GeneratePlayer((byte)(maze.BoardSize - 1), (byte)(maze.BoardSize - 1), 3);
        GeneratePlayer(0, (byte)(maze.BoardSize - 1), 4);
    }

    private void GeneratePlayer(byte x, byte z, byte playerNumber)
    {
        MazeTile tile = maze.GetTile(z, x);
        
        playerObj = Instantiate(playerPrefab, tile.transform.position, Quaternion.identity, transform);
        playerObj.name = ("Player " + playerNumber);
        playerObj.transform.SetAsFirstSibling();
        
        tile.CreateRespawn(playerNumber);
        Player player = playerObj.GetComponent<Player>();
        tile.currentPlayer = player;
        player.currentTile = tile;
        player.AllowMovement();
        //if (playerNumber != 1)
        //{
        //    player.DisallowMovement();
        //}

        player.isIgnoringWalls = true;
    }
}
