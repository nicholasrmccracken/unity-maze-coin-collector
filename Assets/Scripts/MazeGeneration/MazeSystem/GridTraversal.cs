using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace ShareefSoftware
{
    public class GridTraversal<T>
    {
        public enum AlgorithmType
        {
            Kruskal,
            Prim
        }

        private readonly AlgorithmType algorithmType;
        private readonly IGridGraph<T> grid;

        public GridTraversal(IGridGraph<T> grid, AlgorithmType algorithmType)
        {
            this.grid = grid;
            this.algorithmType = algorithmType;
        }

        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> 
            GenerateMaze(int startRow, int startColumn)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Kruskal:
                    Debug.Log("Now generating maze with Kruskal's...");
                    return GenerateMazeWithKruskal(startRow, startColumn);
                case AlgorithmType.Prim:
                    Debug.Log("Now generating maze with Prim's...");
                    return GenerateMazeWithPrim(startRow, startColumn);
                default:
                throw new System.ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> 
            GenerateMazeWithKruskal(int startRow, int startColumn)
        {            
            int ROWS = grid.NumberOfRows;
            int COLS = grid.NumberOfColumns;
            var edges = new List<((int Row, int Column) From, (int Row, int Column) To)>();
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    foreach (var neighbor in grid.Neighbors(row, col))
                    {
                        edges.Add(((row, col), neighbor));
                    }
                }
            }

            var random = new System.Random();
            edges = edges.OrderBy(_ => random.Next()).ToList();

            UnionFind unionFind = new UnionFind(ROWS * COLS);
            foreach (var (from, to) in edges) {
                int idA = from.Row * COLS + from.Column;
                int idB = to.Row * COLS + to.Column;
                
                if (unionFind.Union(idA, idB)) 
                {
                    yield return (from, to);
                }
            }
        }

        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> 
            GenerateMazeWithPrim(int startRow, int startColumn)
        {
            var visited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            var rand = new System.Random();
            
            var cut = new List<((int Row, int Column) From, (int Row, int Column) To)>();
            foreach(var neighbor in grid.Neighbors(startRow, startColumn))
            {
                cut.Add(((startRow, startColumn), neighbor));
            }

            while (cut.Count() > 0)
            {
                int index = rand.Next(cut.Count());
                var (from, to) = cut[index];
                cut.RemoveAt(index);

                if (visited[to.Row, to.Column]) continue;
                visited[to.Row, to.Column] = true;

                yield return (from, to);

                foreach (var neighbor in grid.Neighbors(to.Row, to.Column))
                {
                    cut.Add((to, neighbor));
                }
            }
        }
    }
}
