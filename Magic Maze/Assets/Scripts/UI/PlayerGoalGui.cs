using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerGoalGui : MonoBehaviour
    {
        #region Variables

        public Button GlowButton;
        public TurnManager TurnManager;
//        public PlayerManager PlayerManager;
        public Image image;

        #endregion

        #region Unity Methods
        
        private void Update()
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            var texture = TurnManager.CurrentPlayer.ItemsToCollect[0].texture;
            if (texture == null) return;
            var rect = new Rect(0, 0, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, Vector2.zero);
            image.sprite = sprite;
        }

//        private void UpdateText()
//        {
//            ItemText.text = PlayerManager.CurrentPlayer.ItemsToCollect[0].ToString();
//        }

        #endregion
        
        
    }
}
