using UnityEngine;
[SelectionBase]
public class Tile : MonoBehaviour
{
    [SerializeField] protected GameObject upWallObject;
    [SerializeField] protected GameObject rightWallObject;
    [SerializeField] protected GameObject downWallObject;
    [SerializeField] protected GameObject leftWallObject;

    [SerializeField]
    protected Maze maze;

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
            upWallObject.SetActive(value);
        }
    }
    public bool IsWallRight
    {
        get => isWallRight;
        set
        {
            isWallRight = value;
            rightWallObject.SetActive(value);
        }
    }
    public bool IsWallDown
    {
        get => isWallDown;
        set
        {
            isWallDown = value;
            downWallObject.SetActive(value);
        }
    }
    public bool IsWallLeft
    {
        get => isWallLeft;
        set
        {
            isWallLeft = value;
            leftWallObject.SetActive(value);
        }
    }
    protected void Awake()
    {
        maze = transform.parent.GetComponent<Maze>();
    }

    private void OnValidate()
    {
        upWallObject.SetActive(isWallUp);
        rightWallObject.SetActive(IsWallRight);
        downWallObject.SetActive(isWallDown);
        leftWallObject.SetActive(isWallLeft);
    }
}
