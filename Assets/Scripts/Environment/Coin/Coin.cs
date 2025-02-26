using UnityEngine;

namespace ShareefSoftware
{
    public class Coin : MonoBehaviour
    { 
        [SerializeField] private AudioClip collectCoinSound;

        private Animator animator;
        private Collider coinCollider;

        private void Start()
        {
            animator = GetComponent<Animator>();
            coinCollider = GetComponent<Collider>();
        }

        /* 
         * Called when another collider enters the coin's trigger zone. 
         * If the player collects the coin, it disables the collider, plays a sound, updates the score, 
         * and triggers the coin collection animation.
         */
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player collected " + transform.parent.name + "!");

            if (other.CompareTag("Player"))
            {
                coinCollider.enabled = false;
                animator.SetTrigger("Collect");
                AudioSource.PlayClipAtPoint(collectCoinSound, transform.position);
                ScoreManager.Instance.AddScore(1);
            }
        }

        /* 
         * Destroys the coin object.
         */
        public void DestroyCoin()
        {
            Destroy(gameObject);
        }
    }
}
