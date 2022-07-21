using System;
using FishNet.Object;
using UnityEngine;

namespace KpattGames.Characters
{
    [RequireComponent(typeof(ServerHostCircleBehaviour))]
    public class ClientCircleBehaviour : CircleBehaviour
    {
        private ServerHostCircleBehaviour serverHostBehaviour;

        private void Awake()
        {
            serverHostBehaviour = GetComponent<ServerHostCircleBehaviour>();
        }

        // Request a color change
        [ServerRpc(RequireOwnership = false)]
        public override void ChangeColor()
        {
            serverHostBehaviour.ChangeColor();
        }

        // Request a duplicate to be spawned.
        [ServerRpc(RequireOwnership = false)]
        public override void SpawnDuplicate()
        {
            serverHostBehaviour.SpawnDuplicate();
        }
    }
}