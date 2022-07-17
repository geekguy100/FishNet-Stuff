using System.Collections;
using KpattGames.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KpattGames.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BlobBehaviour : MonoBehaviour, IInteractable
    {
        private int currentHealth;
        private Rigidbody2D rb;
        private Collider2D col;
        
        [SerializeField] private int health;
        [SerializeField][Min(0)] private float minForce = 1f;
        [SerializeField][Min(0)] private float maxForce = 3f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }

        public void PerformAction()
        {
            ++currentHealth;

            if (currentHealth >= health)
            {
                Duplicate();
                currentHealth = 0;
            }
        }

        private void Duplicate()
        {
            // Instantiating clone.
            GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity);
            clone.name = "Clone"; // Doing this so the hierarchy doesn't get cluttered with (Clone)(Clone)...
            Collider2D cloneCol = clone.GetComponent<Collider2D>();
            
            Physics2D.IgnoreCollision(cloneCol, col, true);
            
            
            // Rotating current transform to random angle for AddForce().
            float angle = Random.value * 360f;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.localRotation = rot;

            // Add a random force in one direction to the initial blob, then
            // add the opposite force to the other.
            Vector2 force = transform.right * Random.Range(minForce, maxForce);
            
            rb.AddForce(force, ForceMode2D.Impulse);
            clone.GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Impulse);
            
            
            // Re-enable collisions after some time.
            StartCoroutine(WaitThenEnableCollision());
            
            IEnumerator WaitThenEnableCollision()
            {
                yield return new WaitForSeconds(1f);
                Physics2D.IgnoreCollision(cloneCol, col, false);
            }
        }
    }   
}
