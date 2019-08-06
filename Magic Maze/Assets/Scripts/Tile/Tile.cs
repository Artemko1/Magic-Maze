using UnityEngine;

namespace Tile
{
    [SelectionBase]
    public class Tile : MonoBehaviour
    {
        #region Variables

        protected GameObject upWallObject;
        protected GameObject rightWallObject;
        protected GameObject downWallObject;
        protected GameObject leftWallObject;

        [SerializeField]
        protected Maze.Maze maze;

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

        #endregion

        #region Unity Methods

        protected void Awake()
        {
            upWallObject = upWallObject ?? transform.Find("Plate Visual").Find("Wall Up").gameObject;
            rightWallObject = rightWallObject ?? transform.Find("Plate Visual").Find("Wall Right").gameObject;
            downWallObject = downWallObject ?? transform.Find("Plate Visual").Find("Wall Down").gameObject;
            leftWallObject = leftWallObject ?? transform.Find("Plate Visual").Find("Wall Left").gameObject;

            maze = transform.parent.GetComponent<Maze.Maze>();
        }

        private void OnValidate()
        {
            upWallObject = upWallObject ?? transform.Find("Plate Visual").Find("Wall Up").gameObject;
            rightWallObject = rightWallObject ?? transform.Find("Plate Visual").Find("Wall Right").gameObject;
            downWallObject = downWallObject ?? transform.Find("Plate Visual").Find("Wall Down").gameObject;
            leftWallObject = leftWallObject ?? transform.Find("Plate Visual").Find("Wall Left").gameObject;

            upWallObject.SetActive(isWallUp);
            rightWallObject.SetActive(IsWallRight);
            downWallObject.SetActive(isWallDown);
            leftWallObject.SetActive(isWallLeft);
        }

        #endregion
    }
}
