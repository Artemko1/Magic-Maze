﻿using System.Threading;
using UnityEngine;
using Player;
using UnityEditor;
using UnityEngine.UI;

namespace UI
{
    public class PlayerGoalGui : MonoBehaviour
    {
        #region Variables

        public Button GlowButton;
        public PlayerManager PlayerManager;
        public Image image;

        public GameObject prefab;
        public Texture2D currentTexture;


        private Texture2D texture;
        private Sprite sprite;

        #endregion

        #region Unity Methods

        private void Awake()
        {
//            EventManager.FirstEverTurn += UpdateImage;
//            EventManager.TurnSwitch += UpdateImage;
            
        }

        private void Update()
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            texture = PlayerManager.CurrentPlayer.ItemsToCollect[0].texture;
            var rect = new Rect(0, 0, texture.width, texture.height);
            sprite = Sprite.Create(texture, rect, Vector2.zero);
            image.sprite = sprite;
        }

//        private void UpdateText()
//        {
//            ItemText.text = PlayerManager.CurrentPlayer.ItemsToCollect[0].ToString();
//        }
        
        private void UpdateImageWithoutCache()
        {
            Texture2D bTexture =  null;
            while(bTexture==null)
            {
                print("texture was null there");     
                bTexture = AssetPreview.GetAssetPreview(PlayerManager.CurrentPlayer.ItemsToCollect[0].texture);
                                                 
                Thread.Sleep(80);
            }
                             
            if(bTexture != null)
            {
                print("Texture is not null!");
                var rect = new Rect(0, 0, bTexture.width, bTexture.height);
                sprite = Sprite.Create(bTexture, rect, Vector2.zero);
                image.sprite = sprite;
                                 
            }    
        }


        #endregion
        
        
    }
}
