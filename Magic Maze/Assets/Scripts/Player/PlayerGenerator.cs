using Tile.MazeTile;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Maze.Maze))]
    public class PlayerGenerator : MonoBehaviour
    {
        public GameObject playerPrefab;

        private Maze.Maze maze;

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

        private void Awake()
        {
            maze = GetComponent<Maze.Maze>();
        }

        private void GeneratePlayer(byte x, byte z, byte playerNumber)
        {
            MazeTile tile = maze.GetTile(z, x);

            GameObject playerObj = Instantiate(
                playerPrefab,
                tile.transform.position,
                Quaternion.identity,
                transform);
            playerObj.name = ("Player " + playerNumber);
            playerObj.transform.SetAsFirstSibling();
        
            global::Player.Player player = playerObj.GetComponent<global::Player.Player>();
            tile.currentPlayer = player;
            player.ChangeCurrentTile(tile);
            player.AllowMovement();
            //if (playerNumber != 1)
            //{
            //    player.DisallowMovement();
            //}

            player.isIgnoringWalls = true;
        }
    }
}
