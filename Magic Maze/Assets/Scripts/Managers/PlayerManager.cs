using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Variables

        public List<Player.Player> players = new List<Player.Player>();

        private ItemManager itemManager;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            itemManager = GetComponent<ItemManager>();
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
    }
}
