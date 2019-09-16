using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ItemManager : MonoBehaviour
    {
        #region Variables

        [SerializeField][Range(0, 9)] public int itemsPerPlayer;
        [HideInInspector] public List<Item.Item> UnassignedItems = new List<Item.Item>();
        public bool loadAssetPreviews;
        #endregion

        #region Unity Methods



        #endregion
    }
}
