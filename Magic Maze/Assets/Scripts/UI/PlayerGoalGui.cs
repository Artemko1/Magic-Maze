using System;
using UnityEngine;
using Player;
using UnityEngine.UI;

namespace UI
{
    public class PlayerGoalGui : MonoBehaviour
    {
        #region Variables

        public Text ItemText;
        public Button GlowButton;
        public PlayerManager PlayerManager;

        #endregion

        #region Unity Methods

        private void Start()
        {
            ItemText.text = "Hello World";
        }

        private void Update()
        {
            ItemText.text = PlayerManager.CurrentPlayer.ItemsToCollect[0].ToString();
        }

        #endregion
        
        
    }
}
