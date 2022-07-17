using System;
using KpattGames.Interaction;
using UnityEngine;

namespace KpattGames.Input
{
    /// <summary>
    /// Handles interacting with objects in the game.
    /// </summary>
    public class RaycastInteractor : MonoBehaviour, IInteractor
    {
        /// <summary>
        /// Semi-arbitrary value dictating the distance to raycast.
        /// </summary>
        private const float RAYCAST_DISTANCE = 20f;

        /// <summary>
        /// Reference to the current interactable.
        /// </summary>
        private IInteractable currentInteractable;
        
        [Tooltip("The interactable layer.")]
        [SerializeField] private LayerMask whatIsInteractable;
        

        /// <summary>
        /// Perform a raycast towards the cursor's position
        /// to check for interactables.
        /// </summary>
        /// <param name="origin">The raycast's origin.</param>
        /// <param name="destination">The raycast's destination.</param>
        public void PerformRaycast(Vector3 origin, Vector3 destination)
        {
            Vector3 dir = (destination - origin).normalized;

            var hit = Physics2D.Raycast(origin, dir, RAYCAST_DISTANCE, whatIsInteractable);
            
            if (hit.collider != null)
            {
                OnInteractableNearby(hit.transform.GetComponent<IInteractable>());
            }
        }

        
        public void OnInteractableNearby(IInteractable interactable)
        {
            currentInteractable = interactable;
            Interact();
        }

        public IInteractable OnInteractableLeft()
        {
            IInteractable copy = currentInteractable;
            currentInteractable = null;

            return copy;
        }

        public void Interact()
        {
            currentInteractable?.PerformAction();
            OnInteractableLeft();
        }
    }   
}