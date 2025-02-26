using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShareefSoftware
{
    public class ExitManager : MonoBehaviour
    {
        public static ExitManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private float gameOverDelay = 2.0f;

        /* 
         * Ensures that only one instance of ExitManager exists (Singleton pattern).
         */
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }  
        }

        /* 
         * Triggers the game over sequence by starting a coroutine that displays 
         * the game over text and reloads the scene after a delay.
         */
        public void GameOver()
        {
            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine() 
        {
            gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(gameOverDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
