using UnityEngine;

namespace ShareefSoftware
{
    public class ExitSpawner : MonoBehaviour
    {         
        [SerializeField] private GameObject exitPrefab;

        private float cellWidth;
        private float cellHeight;
        private int numberOfRows;
        private int numberOfColumns;

        public void Initialize(int numberOfRows, int numberOfColumns, float cellWidth, float cellHeight)
        {
            this.cellWidth = cellWidth;
            this.cellHeight = cellHeight;
            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            SpawnExit();
        }

        /* 
         * Spawns the exit at the far end of the maze based on the calculated row and column position.
         * Adjusts the scale to ensure it is large enough for collision detection.
         */
        private void SpawnExit()
        {
            float endRow = numberOfRows * 2;
            float endColumn = numberOfColumns * 2 - 1;
            Vector3 exitPosition = new Vector3(cellWidth * endColumn, 0f, cellHeight * endRow);

            GameObject exitTrigger = Instantiate(exitPrefab, exitPosition, Quaternion.identity);
            
            exitTrigger.name = "ExitTrigger";
            exitTrigger.transform.parent = transform; 
            exitTrigger.transform.localScale = new Vector3(cellWidth, 500f, cellHeight);
        }
    }
}
