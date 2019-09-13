using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ItemManager : MonoBehaviour
    {
        #region Variables

        [SerializeField][Range(0, 18)] public int itemsPerPlayer;
        [HideInInspector] public List<Item.Item> UnassignedItems = new List<Item.Item>();

        #endregion

        #region Unity Methods

    

        #endregion
    }
}
