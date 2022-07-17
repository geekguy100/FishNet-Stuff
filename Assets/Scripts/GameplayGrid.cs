using System;
using System.Collections.Generic;
using UnityEngine;

namespace KpattGames.Tools
{

    /// <summary>
    /// Represents a node in a grid.
    /// </summary>
    public readonly struct Node
    {
        public GameObject Obj { get; }
        public Vector3 Position => Obj.transform.position;

        public Node(GameObject obj)
        {
            Obj = obj;
        }
    }
    
    /// <summary>
    /// Represents the grid the game takes place on.
    /// </summary>
    public class GameplayGrid : MonoBehaviour
    {
        [SerializeField] private int rows;
        [SerializeField] private int columns;

        [SerializeField] private GameObject pointPrefab;

        public Node[,] Nodes { get; private set; }

        
        
        private void Awake()
        {
            Nodes = new Node[rows, columns];
        }

        private void Start()
        {
            Vector3 localScale = transform.localScale;
            float width = localScale.x;
            float height = localScale.y;

            float rowWidth = height / rows;
            float columnWidth = width / columns;

            float prevX = 0;
            float prevY = 0;

            int rowIndex = 0;
            int columnIndex = 0;
            
            for (float row = rowWidth; row <= rowWidth * rows; row += rowWidth)
            {
                for (float col = columnWidth; col <= columnWidth * columns; col += columnWidth)
                {
                    var pos = new Vector2((row + prevX) / 2, -(col + prevY) / 2);

                    var clone = Instantiate(pointPrefab, pos, Quaternion.identity);
                    clone.transform.parent = transform.GetChild(0);
                    
                    Nodes[rowIndex, columnIndex] = new Node(clone);
                    
                    ++columnIndex;
                    prevY = col;
                }

                columnIndex = 0;
                prevX = row;
                prevY = 0;
                ++rowIndex;
            }

            transform.GetChild(0).localPosition = Vector2.zero;
        }
    }   
}