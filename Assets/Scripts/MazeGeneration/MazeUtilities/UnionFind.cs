namespace ShareefSoftware
{
    public class UnionFind
    {
        private int[] parent;
        private int[] rank;

        public UnionFind(int size) 
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 1;
            }
        } 

        public int Find(int x)
        {
            if (parent[x] != x) 
            {
                parent[x] = Find(parent[x]);
            }
            return parent[x];
        }

        public bool Union(int x, int y) 
        {
            int root_x = Find(x);
            int root_y = Find(y);

            if (root_x != root_y) 
            {
                if (rank[root_x] < rank[root_y])
                {
                    parent[root_x] = root_y;
                    rank[root_y] += rank[root_x];
                } else 
                {
                    parent[root_y] = root_x;
                    rank[root_x] += rank[root_y];
                }
                return true;
            }
            return false;
        }
    }
}
