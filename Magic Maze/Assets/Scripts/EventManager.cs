using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Button ColumnMoveButton;

    public delegate void ClickAction();
    public static event ClickAction OnColumnMove;

    private void OnGUI()
    {
            if (OnColumnMove != null)
            {
                OnColumnMove();
            }
            Debug.Log("");
    }
}
