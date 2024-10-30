using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Graph_FinalProject
{
    internal class BreadthFirstSearch
    {
        public event Action<int, Color>? NodeVisited;
        public event Action<int, int, Color>? EdgeVisited;
        public event Action<Queue<int>>? Queue;
        public event Action? Clear;

        private static readonly int WHITE = 0;
        private static readonly int GRAY = 1;
        private static readonly int BLACK = 2;

        private Graph graph;
        private int[] distance, color, predecessor;

        public BreadthFirstSearch(Graph graph)
        {
            this.graph = graph;
            distance = new int[graph.numNodes];
            predecessor = new int[graph.numNodes];
            color = new int[graph.numNodes];
        }

        public void PerformBFS(int source)
        {
            for (int u = 0; u < graph.numNodes; u++)
            {
                if (u != source)
                {
                    color[u] = WHITE;
                    distance[u] = int.MaxValue;
                    predecessor[u] = -1;
                }
            }

            color[source] = GRAY;
            distance[source] = 0;
            predecessor[source] = -1;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            NodeVisited?.Invoke(source, Color.Gray);

            while (queue.Count > 0)
            {
                Queue?.Invoke(queue);
                int u = queue.Dequeue();

                for (int v = 0; v < graph.numNodes; v++)
                {
                    if (graph.adjMatrix[u, v] != 0 && color[v] == WHITE)
                    {
                        color[v] = GRAY;
                        distance[v] = distance[u] + 1;
                        predecessor[v] = u;
                        queue.Enqueue(v);
                        NodeVisited?.Invoke(v, Color.Gray);
                    }
                }

                color[u] = BLACK;
                NodeVisited?.Invoke(u, Color.Black);
            }
        }

        public List<int> GetPath(int source, int target)
        {
            PerformBFS(source);

            List<int> path = new List<int>();
            BuildPath(source, target, path);

            Clear?.Invoke();

            for (int i = 0; i < path.Count - 1; i++)
            {
                int fromNode = path[i];
                int toNode = path[i + 1];
                EdgeVisited?.Invoke(fromNode, toNode, Color.Orange);
            }

            return path;
        }

        private void BuildPath(int source, int v, List<int> path)
        {
            if (source == v)
                path.Add(source);
            else if (predecessor[v] == -1)
                return;
            else
            {
                BuildPath(source, predecessor[v], path);
                path.Add(v);
            }
        }
    }
}
