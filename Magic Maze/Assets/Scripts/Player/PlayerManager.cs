using System;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Variables

        public List<Player> players = new List<Player>();

        private ItemGenerator itemGenerator;
        private ItemManager itemManager;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            itemGenerator = GetComponent<ItemGenerator>();
            itemManager = GetComponent<ItemManager>();
        }

        #endregion

        public void AssignItemsToCollect()
        {
            foreach (var player in players)
            {
                for (int i = 0; i < itemGenerator.itemsPerPlayer; i++)
                {
                    player.ItemsToCollect.Add(itemManager.UnassignedItems[0]);
                    itemManager.UnassignedItems.RemoveAt(0);
                }
            }
        }
    }
}
