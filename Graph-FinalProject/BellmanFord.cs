using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Drawing;

namespace Graph_FinalProject
{
    internal class BellmanFord
    {
        public event Action<int, Color>? NodeVisited;
        public event Action<int, int, Color>? EdgeVisited;
        public event Action? Clear;
        public event Action<int[], int[]>? Print;

        private Graph graph;
        private int[] dist;
        private int[] predecessor;

        public BellmanFord(Graph graph)
        {
            this.graph = graph;
            dist = new int[graph.numNodes];
            predecessor = new int[graph.numNodes];
        }

        public void InitializeSingleSource(int source)
        {
            for (int i = 0; i < graph.numNodes; i++)
            {
                dist[i] = int.MaxValue;
                predecessor[i] = -1;
            }
            dist[source] = 0;
        }

        public void Relax(int u, int v, int weight)
        {
            EdgeVisited?.Invoke(u, v, Color.Blue);

            if (dist[u] != int.MaxValue && dist[v] > dist[u] + weight)
            {
                dist[v] = dist[u] + weight;
                predecessor[v] = u;
                Print?.Invoke(dist, predecessor);
            }
        }

        public void PerformBellmanFord(int source)
        {
            InitializeSingleSource(source);
            Print?.Invoke(dist, predecessor);

            for (int i = 0; i < graph.numNodes - 1; i++)
            {
                Clear?.Invoke();
                bool relaxed = false;

                for (int u = 0; u < graph.numNodes; u++)
                {
                    NodeVisited?.Invoke(u, Color.Gray);

                    for (int v = 0; v < graph.numNodes; v++)
                    {
                        if (graph.adjMatrix[u, v] != 0)
                        {
                            int oldDist = dist[v];
                            Relax(u, v, graph.adjMatrix[u, v]);
                            if (dist[v] != oldDist)
                                relaxed = true;
                        }
                    }
                }

                if (!relaxed)
                    break;
            }

            for (int u = 0; u < graph.numNodes; u++)
            {
                for (int v = 0; v < graph.numNodes; v++)
                {
                    if (graph.adjMatrix[u, v] != 0 && dist[v] > dist[u] + graph.adjMatrix[u, v])
                    {
                        throw new InvalidOperationException("Graph contains a negative-weight cycle.");
                    }
                }
            }
        }

        public (List<int> path, int cost) GetLeastCostPath(int source, int destination)
        {
            PerformBellmanFord(source);

            if (dist[destination] == int.MaxValue)
                throw new InvalidOperationException($"No path exists from {source} to {destination}");

            List<int> path = new List<int>();
            int current = destination;

            while (current != -1)
            {
                path.Add(current);
                current = predecessor[current];
            }

            path.Reverse();

            int cost = dist[destination];

            Clear?.Invoke();

            for (int i = 0; i < path.Count - 1; i++)
            {
                int fromNode = path[i];
                int toNode = path[i + 1];
                
                EdgeVisited?.Invoke(fromNode, toNode, Color.Orange);
            }

            return (path, cost);
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

