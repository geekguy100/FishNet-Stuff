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
}