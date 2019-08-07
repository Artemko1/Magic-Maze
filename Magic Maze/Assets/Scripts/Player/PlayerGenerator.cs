using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Maze.Maze))]
    public class PlayerGenerator : MonoBehaviour
    {
        #region Variables

        public GameObject playerPrefab;

        private Maze.Maze maze;
        private PlayerManager playerManager;
        
        
        private readonly (int, int)[] spawnPositions = new (int, int)[4];

        #endregion

        #region Unity Methods

        private void Awake()
        {
            maze = GetComponent<Maze.Maze>();
            playerManager = GetComponent<PlayerManager>();
            
            spawnPositions[0] = (0, 0);
            spawnPositions[1] = (0, maze.BoardSize - 1);
            spawnPositions[2] = (maze.BoardSize - 1, 0);
            spawnPositions[3] = (maze.BoardSize - 1, maze.BoardSize - 1);
        }

        #endregion

        /// <summary>
        /// Создает всех игроков.
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        public void GeneratePlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (i == spawnPositions.Length)
                {
                    break;
                }
                CreatePlayer(spawnPositions[i], "Player "+i);
            }
        }

        private void CreatePlayer((int,int) p, string playerName = "Player")
        {
            var (z, x) = p;
            var tile = maze.GetTile(z, x);

            var playerObj = Instantiate(
                playerPrefab,
                tile.transform.position,
                Quaternion.identity,
                transform);
            playerObj.name = playerName;
            playerObj.transform.SetAsFirstSibling();
        
            var player = playerObj.GetComponent<Player>();
            tile.currentPlayer = player;
            player.CurrentTile = tile;
            player.AllowMovement();
            playerManager.players.Add(player);

            player.isIgnoringWalls = true;
        }
    }
}
