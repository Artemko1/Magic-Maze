using System;
using UnityEngine;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {
        #region Variables

        public TurnPhase CurrentPhase;

        public Player.Player CurrentPlayer => playerManager.players[playerIndex];

        private int playerIndex;

        private Maze.Maze maze;
        private PlayerManager playerManager;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            var board = GameObject.FindWithTag("Board");
            playerManager = board.GetComponent<PlayerManager>();
            maze = board.GetComponent<Maze.Maze>();
            
            EventManager.TurnSwitch += SwitchTurn;
        }

        #endregion

        public void SwitchTurn()
        {
            if (CurrentPhase == TurnPhase.PlayerMove)
            {
                ToColumnMove();
            }
            else if (CurrentPhase == TurnPhase.ColumnMove)
            {
                print("Move a column");
            }
            else
            {
                throw new Exception("Turn phase not assigned");
            }
        }
        
        public void ToColumnMove()
        {
            CurrentPlayer.actions.PlayerMap.Disable();
            maze.ExcessTile.actions.ExcessTileMap.Enable();
            playerIndex++;
            if (playerIndex == playerManager.players.Count)
            {
                playerIndex = 0;
            }

            CurrentPhase = TurnPhase.ColumnMove;
            print("Column turn");
        }

        public void ToPlayerMove()
        {
            maze.ExcessTile.actions.ExcessTileMap.Disable();
            CurrentPlayer.actions.PlayerMap.Enable();

            CurrentPhase = TurnPhase.PlayerMove;
            print("Player turn");
        }
        
        public void InitializeFirstTurn()
        {
            maze.ExcessTile.actions.Enable();
        }
    }

    
    public enum TurnPhase
    {
        ColumnMove,
        PlayerMove
    }
}
