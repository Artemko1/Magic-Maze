﻿using System.Collections.Generic;
using Item;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Variables

        public List<Player> players = new List<Player>();

        /// <summary>
        /// Wich player's turn.
        /// </summary>
        public Player CurrentPlayer => players[playerIndex];

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
                for (var i = 0; i < itemGenerator.itemsPerPlayer; i++)
                {
                    player.ItemsToCollect.Add(itemManager.UnassignedItems[0]);
                    itemManager.UnassignedItems.RemoveAt(0);
                }
            }
        }

        private void TurnToNextPlayer()
        {
            playerIndex++;
            if (playerIndex == players.Count)
            {
                playerIndex = 0;
            }
            Debug.Log("Turn switched");
        }
    }
}
