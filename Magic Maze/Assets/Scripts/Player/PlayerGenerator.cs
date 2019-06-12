using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGenerator : MonoBehaviour
{
    public Maze maze;
    public GameObject playerPrefab;
    GameObject playerObj;

    public Button moveUpButton;
    public Button moveRightButton;
    public Button moveDownButton;
    public Button moveLeftButton;

    public void GeneratePlayer(byte x, byte z, byte playerNumber)
    {
        MazeTile tile = maze.GetTile(z, x);
        
        playerObj = Instantiate(playerPrefab, tile.transform.position, Quaternion.identity, transform);
        playerObj.name = ("Player " + playerNumber);
        playerObj.transform.SetAsFirstSibling();
        
        tile.CreateRespawn(playerNumber);
        Player player = playerObj.GetComponent<Player>();
        tile.SetCurrentPlayer(player);
        player.currentTile = tile;
        InitializeMovement(player);
        if (playerNumber != 1)
        {
            player.DisallowMovement();
        }

        player.isIgnoringWalls = true;
    }
    /// <summary>
    /// Привязывает функции передвижения к кнопкам GUI.
    /// </summary>
    /// <param name="player">Игрок, для которого делается привязка.</param>
    public void InitializeMovement(Player player)
    {
        moveUpButton.onClick.AddListener(() => player.Move(Direction.Up));
        moveRightButton.onClick.AddListener(() => player.Move(Direction.Right));
        moveDownButton.onClick.AddListener(() => player.Move(Direction.Down));
        moveLeftButton.onClick.AddListener(() => player.Move(Direction.Left));
        player.AllowMovement();
        //Debug.Log("Movement initialized for " + player);
    }
}
