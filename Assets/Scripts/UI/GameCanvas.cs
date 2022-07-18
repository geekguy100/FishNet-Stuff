using System;
using KpattGames.Channels;
using KpattGames.Characters;
using UnityEngine;

namespace KpattGames.UI
{
    public class GameCanvas : MonoBehaviour
    {
        private Camera uiCam;
        
        [SerializeField] private HealthDisplay healthDisplayPrefab;
        [SerializeField] private BlobBehaviourChannel blobInteractionChannel;

        #region Event Handling
        private void OnEnable()
        {
            blobInteractionChannel.OnEventRaised += DisplayHealth;
        }

        private void OnDisable()
        {
            blobInteractionChannel.OnEventRaised -= DisplayHealth;
        }
        #endregion

        private void Start()
        {
            uiCam = Camera.main;
        }

        private void DisplayHealth(BlobBehaviour blob)
        {
            HealthDisplay clone = Instantiate(healthDisplayPrefab, transform);
            
            Vector3 position = uiCam.WorldToScreenPoint(blob.transform.position);
            position.z = 0;
            clone.transform.position = position;
            
            clone.Display(blob.GetHealth());
        }
    }   
}
