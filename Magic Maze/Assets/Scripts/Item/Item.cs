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

        public bool loadPreview = false;
        public Texture2D texture;

        public int Id
        {
            get => id;
            set
            {
                if (value == 0)
                {
                    id = value;
                }
            }
        }

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
        [SerializeField] private int id;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            texture =  null;
            if (loadPreview == false) return;
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
