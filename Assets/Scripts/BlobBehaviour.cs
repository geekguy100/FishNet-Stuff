using KpattGames.Interaction;
using UnityEngine;

namespace KpattGames.Characters
{
    public class BlobBehaviour : MonoBehaviour, IInteractable
    {
        public void PerformAction()
        {
            Debug.Log("Hit blob!");
        }
    }   
}
