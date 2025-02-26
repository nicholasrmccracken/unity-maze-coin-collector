using TMPro;
using UnityEngine;

namespace ShareefSoftware
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI scoreText;
        private int score;

        /* 
         * Ensures that only one instance of ScoreManager exists (Singleton pattern).
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
         * Increases the player's score by a specified value and updates the UI.
         * @param value - The amount to add to the current score.
         */
        public void AddScore(int value)
        {
            score += value;
            scoreText.text = "Dabloons: " + score;
        }
    }
}
