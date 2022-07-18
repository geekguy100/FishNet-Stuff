using System;
using System.Collections;
using KpattGames.Characters;
using TMPro;
using UnityEngine;

namespace KpattGames.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        private RectTransform rectTransform;
        
        [SerializeField] private float slideSpeed = 1;
        [SerializeField] private TextMeshProUGUI display;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Display(BlobHealth health)
        {
            display.text = health.currentHealth + "/" + health.maxHealth;
            StartCoroutine(SlideUp());
        }

        private IEnumerator SlideUp()
        {
            while (true)
            {
                Vector3 position = rectTransform.localPosition;
                position += Vector3.up * (slideSpeed * Time.deltaTime);
                
                rectTransform.localPosition = position;

                yield return null;
            }
        }
    }
   
}