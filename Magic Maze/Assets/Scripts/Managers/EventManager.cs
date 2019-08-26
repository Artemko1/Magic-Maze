using System;
using Player;
using UI;
using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        public delegate void ActionDelegate();

        public static event ActionDelegate TurnSwitch;
        
        public void AddButtonListeners()
        {
            var buttons = GetComponent<Buttons>();
            if (TurnSwitch == null)
            {
                Debug.LogError("TurnSwitch == null");
            }
            else
            {
                buttons.switchTurnButton?.onClick.AddListener(TurnSwitch.Invoke);
            }
        }
    }
}