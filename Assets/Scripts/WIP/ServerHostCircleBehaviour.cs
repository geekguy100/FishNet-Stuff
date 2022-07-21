using System;
using FishNet.Object;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KpattGames.Characters
{
    public class ServerHostCircleBehaviour : CircleBehaviour
    {
        private SpriteRenderer spriteRenderer;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // Tell all clients to update sprite color of this object.
        [ObserversRpc(IncludeOwner = true, BufferLast = true)]
        public override void ChangeColor()
        {
            Debug.Log("Called change color.");
            Color color = Random.ColorHSV();
            color.a = 1f;
            
            spriteRenderer.color = color;
        }
        
        // Spawn in a clone for all clients.
        public override void SpawnDuplicate()
        {
            Debug.Log("Called SpawnDup");
            var pos = Random.insideUnitCircle * Random.Range(-5f, 5f);
            var clone = Instantiate(gameObject, pos, Quaternion.identity);
            
            base.Spawn(clone, base.Owner);
        }
    }
}