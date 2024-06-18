using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_FinalProject
{
    internal class DepthFirstSearch
    {
        public event Action<int, int, string>? NodeVisited;
        public event Action<int, int>? EdgeVisited;

        private static readonly int WHITE = 0;
        private static readonly int GRAY = 1;
        private static readonly int BLACK = 2;

        private int[] discoveryTime;
        private int[] finishTime;
        private int[] predecessor;
        private Graph graph;

        public DepthFirstSearch(Graph graph)
        {
            this.graph = graph;
            int n = graph.numNodes;
            discoveryTime = new int[n];
            finishTime = new int[n];
            predecessor = new int[n];
        }

        public void PerformDFS()
        {
            int time = 0;
            int[] color = new int[graph.numNodes];
            for (int u = 0; u < graph.numNodes; u++)
            {
                color[u] = WHITE;
                predecessor[u] = -1;
            }
            for (int u = 0; u < graph.numNodes; u++)
            {
                if (color[u] == WHITE)
                {
                    time = VisitDFS(u, time, color);
                }
            }
        }

        private int VisitDFS(int u, int time, int[] color)
        {
            color[u] = GRAY;
            discoveryTime[u] = ++time;
            NodeVisited?.Invoke(u, time, "start");

            for (int v = 0; v < graph.numNodes; v++)
            {
                if (graph.adjMatrix[u, v] > 0)
                {
                    if (color[v] == WHITE)
                    {
                        predecessor[v] = u;
                        EdgeVisited?.Invoke(u, v);
                        time = VisitDFS(v, time, color);
                    }
                }
            }

            color[u] = BLACK;
            finishTime[u] = ++time;
            NodeVisited?.Invoke(u, time, "finish");

            return time;
        }

        public int GetDiscoveryTime(int v)
        {
            return discoveryTime[v];
        }

        public int GetFinishTime(int v)
        {
            return finishTime[v];
        }

        public int GetPredecessor(int v)
        {
            return predecessor[v];
        }
    }
}
