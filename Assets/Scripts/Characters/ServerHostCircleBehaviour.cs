using System;
using FishNet.Object;
using UnityEngine;

namespace KpattGames.Characters
{
    public class ServerHostCircleBehaviour : CircleBehaviour
    {
        private SpriteRenderer spriteRenderer;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        [ObserversRpc(IncludeOwner = true, BufferLast = true)]
        public override void ChangeColor(Color color)
        {
            spriteRenderer.color = color;
        }

        [ObserversRpc(IncludeOwner = true)]
        public override void SpawnDuplicate()
        {
            Debug.Log("Spawn Duplicate");
        }
    }
}