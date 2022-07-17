using UnityEngine;

namespace KpattGames.Tools
{
    /// <summary>
    /// Represents the grid the game takes place on.
    /// </summary>
    public class GameplayGrid : MonoBehaviour
    {
        [SerializeField][Min(0)] private int rows;
        [SerializeField][Min(0)] private int columns;

        [SerializeField] private GameObject pointPrefab;

        public Node[,] Nodes { get; private set; }
        private Transform nodeParent;

        
        
        private void Awake()
        {
            Nodes = new Node[rows, columns];
            nodeParent = transform.GetChild(0);
        }

        private void Start()
        {
            CreateGrid();
        }

        [ContextMenu("Reset Grid")]
        public void ResetGrid()
        {
            // Re-initialize Nodes in case we modified rows and cols.
            Nodes = new Node[rows, columns];
            
            // We have to loop through the nodeParent
            // because we may have changed the rows and/or columns
            // fields.
            int childCount = nodeParent.childCount;
            for (int i = 0; i < childCount; ++i)
            {
                Destroy(nodeParent.GetChild(i).gameObject);
            }

            nodeParent.localPosition = new Vector3(0.5f, -0.5f);
            CreateGrid();
        }
        
        private void CreateGrid()
        {
            Vector3 localScale = transform.localScale;
            float width = localScale.x;
            float height = localScale.y;

            float xOffset = width / columns;
            float yOffset = height / rows;
            
            
            
            int rowIndex = 0;
            float prevY = 0;

            // Adding the nodes across each row.
            for (float yPos = yOffset; yPos <= yOffset * rows; yPos += yOffset)
            {
                float prevX = 0;
                int colIndex = 0;
                
                for (float xPos = xOffset; xPos <= xOffset * columns; xPos += xOffset)
                {
                    Vector2 pos = new Vector2((xPos + prevX) / 2, -(yPos + prevY) / 2);

                    var clone = Instantiate(pointPrefab, pos, Quaternion.identity);
                    clone.transform.parent = nodeParent;
                    
                    Nodes[rowIndex, colIndex++] = new Node(clone);
                    
                    prevX = xPos;
                }

                prevY = yPos;
                ++rowIndex;
            }

            nodeParent.localPosition = Vector3.zero;
        }
    }   
}