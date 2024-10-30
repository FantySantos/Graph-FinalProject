using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Graph_FinalProject
{
    internal class Prim
    {
        public event Action<int, Color>? NodeVisited;
        public event Action<int, int, Color>? EdgeVisited;
        public event Action? Clear;
        public event Action<int[], int[]>? Print;

        private Graph graph;
        private int[] dist;
        private int[] predecessor;
        private PriorityQueue<int, int> queue;
        private HashSet<int> visited;


        public Prim(Graph graph)
        {
            this.graph = graph;
            dist = new int[graph.numNodes];
            predecessor = new int[graph.numNodes];
            queue = new PriorityQueue<int, int>();
            visited = new HashSet<int>();
        }

        public void InitializeSingleSource(int source)
        {
            for (int i = 0; i < graph.numNodes; i++)
            {
                dist[i] = int.MaxValue;
                predecessor[i] = -1;
            }

            dist[source] = 0;
            queue.Enqueue(source, 0);
        }

        public void Relax(int u, int v, int weight)
        {
            if (dist[u] != int.MaxValue && dist[v] > weight)
            {
                dist[v] = weight;
                queue.Enqueue(v, dist[v]);
                predecessor[v] = u;
                Print?.Invoke(dist, predecessor);
            }
        }

        public void PerformPrim(int source = 0)
        {
            InitializeSingleSource(source);
            Print?.Invoke(dist, predecessor);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                if (visited.Contains(u))
                    continue;

                visited.Add(u);
                NodeVisited?.Invoke(u, Color.Gray);

                for (int j = 0; j < graph.numNodes; j++)
                {
                    if (graph.adjMatrix[u, j] != 0 && !visited.Contains(j))
                    {
                        EdgeVisited?.Invoke(u, j, Color.Blue);
                        Relax(u, j, graph.adjMatrix[u, j]);
                    }
                }

                Clear?.Invoke();
            }

            Print?.Invoke(dist, predecessor);
        }

        public List<(int from, int to)> GetMinimumSpanningTreeEdges()
        {
            PerformPrim();
            List<(int from, int to)> mstEdges = new List<(int from, int to)>();

            for (int v = 1; v < graph.numNodes; v++)
            {
                int u = predecessor[v];
                if (u != -1)
                    mstEdges.Add((u, v));
            }

            Clear?.Invoke();
            foreach (var edge in mstEdges)
                EdgeVisited?.Invoke(edge.from, edge.to, Color.Orange);

            return mstEdges;
        }

        public int[] GetDistances()
        {
            return dist;
        }

        public int[] GetPredecessors()
        {
            return predecessor;
        }
    }
}
