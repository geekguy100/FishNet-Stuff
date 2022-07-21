

using System;
using FishNet.Object;
using UnityEngine;

namespace KpattGames.Characters
{
    public abstract class CircleBehaviour : NetworkBehaviour
    {
        public abstract void ChangeColor();
        public abstract void SpawnDuplicate();
    }
}