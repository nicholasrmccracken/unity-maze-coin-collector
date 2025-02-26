using UnityEngine;

namespace ShareefSoftware
{
    public class CoinSpawner : MonoBehaviour
    {         
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private float yOffset = 3.25f;

        private Maze maze; 
        private float cellWidth;
        private float cellHeight;

        public void Initialize(Maze maze, float cellWidth, float cellHeight)
        {
            this.maze = maze;
            this.cellWidth = cellWidth;
            this.cellHeight = cellHeight;
            SpawnCoins();
        }

        /* 
         * Spawns coins at the dead-end positions of the maze.
         * Each coin is placed in a newly created parent GameObject for organization.
         */
        private void SpawnCoins()
        { 
            var deadEnds = MazeQuery.DeadEnds(maze);

            foreach (var (row, column) in deadEnds)
            { 
                Vector3 position = new Vector3(
                    cellWidth * (column * 2 + 1), 
                    yOffset, 
                    cellHeight * (row * 2 + 1));

                GameObject coinParent = new GameObject($"Coin_{row}_{column}");
                coinParent.transform.position = position;
                coinParent.transform.parent = transform; 

                Instantiate(coinPrefab, coinParent.transform);
            }
        }
    }
}
