using UnityEngine;

namespace ShareefSoftware
{
    public class Exit : MonoBehaviour
    {
        /* 
         * Detects when the player reaches the exit.
         * Triggers the game over process by calling the ExitManager.
         * @param other - The collider that entered the exit trigger.
         */
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player reached the exit! Reloading scene...");
                ExitManager.Instance.GameOver();
            }
        }
    }
}