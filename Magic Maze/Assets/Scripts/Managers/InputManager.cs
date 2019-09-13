using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private static Actions actions;

        public static Actions GetActions()
        {
            return actions ?? (actions = new Actions());
        }
    }
}
