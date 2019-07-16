using UnityEngine;

public class Tile : MonoBehaviour
{
    protected GameObject upWallObject;
    protected GameObject rightWallObject;
    protected GameObject downWallObject;
    protected GameObject leftWallObject;

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
        upWallObject = transform.Find("Plate Visual").Find("Wall Up").gameObject;
        rightWallObject = transform.Find("Plate Visual").Find("Wall Right").gameObject;
        downWallObject = transform.Find("Plate Visual").Find("Wall Down").gameObject;
        leftWallObject = transform.Find("Plate Visual").Find("Wall Left").gameObject;

        maze = transform.parent.GetComponent<Maze>();

        TileGenerator.GenerateWalls(this);
    }

    private void OnValidate()
    {
        if (maze != null)
        {
            upWallObject.SetActive(isWallUp);
            rightWallObject.SetActive(IsWallRight);
            downWallObject.SetActive(isWallDown);
            leftWallObject.SetActive(isWallLeft);
        }
    }
}
