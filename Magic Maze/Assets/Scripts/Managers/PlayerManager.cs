using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Variables

        public List<Player.Player> players = new List<Player.Player>();

        /// <summary>
        /// Wich player's turn.
        /// </summary>
        public Player.Player CurrentPlayer => players[playerIndex];

        private ItemGenerator itemGenerator;
        private ItemManager itemManager;

        private int playerIndex;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            itemGenerator = GetComponent<ItemGenerator>();
            itemManager = GetComponent<ItemManager>();
            EventManager.TurnSwitch += TurnToNextPlayer;
        }

        #endregion

        public void AssignItemsToCollect()
        {
            foreach (var player in players)
            {
                for (var i = 0; i < itemManager.itemsPerPlayer; i++)
                {
                    player.ItemsToCollect.Add(itemManager.UnassignedItems[0]);
                    itemManager.UnassignedItems.RemoveAt(0);
                }
            }
        }

        private void TurnToNextPlayer()
        {
            CurrentPlayer.actions.Player.Disable();
            playerIndex++;
            if (playerIndex == players.Count)
            {
                playerIndex = 0;
            }
            CurrentPlayer.actions.Player.Enable();
            Debug.Log("Turn switched");
        }

        public void InitializeFirstTurn()
        {
            players[0].actions.Player.Enable();
        }
    }
}
