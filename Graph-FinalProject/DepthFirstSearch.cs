using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Graph_FinalProject
{
    internal class DepthFirstSearch
    {
        public event Action<int, Color>? NodeVisited;
        public event Action<int, int, Color>? EdgeVisited;

        private static readonly int WHITE = 0;
        private static readonly int GRAY = 1;
        private static readonly int BLACK = 2;

        private int[] discoveryTime;
        private int[] finishTime;
        private int[] predecessor;
        private Graph graph;

        private int time;
        private int[] color;
        public bool isCyclic;
        private List<int> topologicalOrder;


        public DepthFirstSearch(Graph graph)
        {
            this.graph = graph;
            int n = graph.numNodes;
            discoveryTime = new int[n];
            finishTime = new int[n];
            predecessor = new int[n];
            isCyclic = false;
            time = 0;
            color = new int[graph.numNodes];
            topologicalOrder = [];
        }

        public void PerformDFS()
        {
            topologicalOrder = new List<int>();

            for (int u = 0; u < graph.numNodes; u++)
            {
                color[u] = WHITE;
                predecessor[u] = -1;
            }
            for (int u = 0; u < graph.numNodes; u++)
            {
                if (color[u] == WHITE)
                {
                    VisitDFS(u, -1);
                }
            }

            topologicalOrder.Reverse();
        }

        private void VisitDFS(int u, int parent, bool isConnection = false)
        {
            color[u] = GRAY;
            discoveryTime[u] = ++time;
            if (!isConnection)
                NodeVisited?.Invoke(u, Color.Gray);

            for (int v = 0; v < graph.numNodes; v++)
            {
                if (graph.adjMatrix[u, v] > 0)
                {
                    if (color[v] == WHITE)
                    {
                        predecessor[v] = u;
                        if (!isConnection)
                            EdgeVisited?.Invoke(u, v, Color.Red);
                        VisitDFS(v, u, isConnection);
                    }
                    else if (v != parent && color[v] == GRAY)
                    {
                        if (!isConnection)
                            EdgeVisited?.Invoke(u, v, Color.Blue);
                        isCyclic = true;
                    }

                }
            }

            color[u] = BLACK;
            finishTime[u] = ++time;
            if (!isConnection)
                NodeVisited?.Invoke(u, Color.Black);
            topologicalOrder.Add(u);
        }

        public bool IsConnected()
        {
            if (graph.numNodes == 0) return true;

            for (int u = 0; u < graph.numNodes; u++)
            {
                color[u] = WHITE;
                predecessor[u] = -1;
            }
            VisitDFS(0, -1, true);

            for (int u = 0; u < graph.numNodes; u++)
            {
                if (color[u] == WHITE)
                    return false;
            }
            return true;
        }

        public bool IsCyclic()
        {
            PerformDFS();
            return isCyclic;
        }

        public List<int> GetTopologicalOrder()
        {
            if (isCyclic)
                throw new InvalidOperationException("Graph is cyclic and cannot be topologically sorted.");
            return new List<int>(topologicalOrder);
        }

        public List<List<int>> FindStronglyConnectedComponents()
        {
            Stack<int> stack = new();
            for (int u = 0; u < graph.numNodes; u++)
            {
                if (color[u] == WHITE)
                {
                    VisitDFSForStack(u, stack);
                }
            }

            Graph transposedGraph = graph.TransposedGraph();

            for (int u = 0; u < graph.numNodes; u++)
            {
                color[u] = WHITE;
            }

            List<List<int>> stronglyConnectedComponents = [];
            int sccColorIndex = 0;

            while (stack.Count > 0)
            {
                int u = stack.Pop();
                if (color[u] == WHITE)
                {
                    List<int> component = [];
                    VisitDFSForSCC(u, transposedGraph, component, sccColorIndex);
                    stronglyConnectedComponents.Add(component);
                    sccColorIndex++;
                }
            }

            return stronglyConnectedComponents;
        }

        private void VisitDFSForStack(int u, Stack<int> stack)
        {
            color[u] = GRAY;
            for (int v = 0; v < graph.numNodes; v++)
            {
                if (graph.adjMatrix[u, v] > 0 && color[v] == WHITE)
                {
                    VisitDFSForStack(v, stack);
                }
            }
            stack.Push(u);
        }

        private void VisitDFSForSCC(int u, Graph transposedGraph, List<int> component, int sccColorIndex)
        {
            color[u] = GRAY;
            component.Add(u + 1);

            Color sccColor = GetColorByIndex(sccColorIndex);
            NodeVisited?.Invoke(u, sccColor);


            for (int v = 0; v < transposedGraph.numNodes; v++)
            {
                if (transposedGraph.adjMatrix[u, v] > 0)
                {
                    if (color[v] == WHITE)
                    {
                        VisitDFSForSCC(v, transposedGraph, component, sccColorIndex);
                    }
                }
            }
        }

        private Color GetColorByIndex(int index)
        {
            Color[] colors = {
                Color.Red, Color.Green, Color.Yellow, Color.Magenta, Color.Cyan, Color.Aquamarine, Color.Coral,
                Color.Brown, Color.Chartreuse, Color.DarkGoldenrod, Color.DarkOliveGreen, Color.DarkOrchid, Color.DarkSlateGray,
                Color.DeepPink, Color.DeepSkyBlue, Color.ForestGreen, Color.Fuchsia, Color.Gold, Color.Honeydew, Color.Indigo,
                Color.Khaki, Color.Lavender, Color.LightSeaGreen, Color.Lime, Color.MediumOrchid, Color.MediumSpringGreen,
                Color.MidnightBlue, Color.Moccasin, Color.OliveDrab, Color.Orchid
            };
            return colors[index];
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
