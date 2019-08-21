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

        private void Start()
        {
            var buttons = GetComponent<Buttons>();
            buttons.switchTurnButton?.onClick.AddListener(TurnSwitch.Invoke);
        }
    }
}