using System.Threading;
using Tile.MazeTile;
using UnityEditor;
using UnityEngine;

namespace Item
{
    [SelectionBase]
    public class Item : MonoBehaviour
    {
        #region Variables

        public Texture2D texture;

        public MazeTile CurrentTile
        {
            get => currentTile;
            set
            {
                if (currentTile != null)
                {
                    currentTile.currentItem = null;
                }
                currentTile = value;
                currentTile.currentItem = this;
            }
        }

        [SerializeField] private MazeTile currentTile;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            texture =  null;
            return;
            while(texture==null)
            {
                //                print("texture was null there");     
                texture = AssetPreview.GetAssetPreview(gameObject);
                                                     
                Thread.Sleep(80);
            }
        }

        #endregion


    }
}
