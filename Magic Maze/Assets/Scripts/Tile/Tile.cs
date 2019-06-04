using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected GameObject upWallObject;
    protected GameObject rightWallObject;
    protected GameObject downWallObject;
    protected GameObject leftWallObject;

    [SerializeField]
    protected bool isWallUp;
    [SerializeField]
    protected bool isWallRight;
    [SerializeField]
    protected bool isWallDown;
    [SerializeField]
    protected bool isWallLeft;

    public bool IsWallUp
    {
        get => isWallUp;
        set
        {
            isWallUp = value;
            Debug.Log("Inside up setter");
            upWallObject?.SetActive(value);
        }
    }
    public bool IsWallRight
    {
        get => isWallRight;
        set
        {
            isWallRight = value;
            Debug.Log("Inside right setter");
            rightWallObject?.SetActive(value);
        }
    }
    public bool IsWallDown
    {
        get => isWallDown;
        set
        {
            isWallDown = value;
            Debug.Log("Inside down setter");
            downWallObject?.SetActive(value);
        }
    }
    public bool IsWallLeft
    {
        get => isWallLeft;
        set
        {
            isWallLeft = value;
            Debug.Log("Inside left setter");
            leftWallObject?.SetActive(value);
        }
    }


}
