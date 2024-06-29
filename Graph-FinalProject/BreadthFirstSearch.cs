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

        private static readonly int WHITE = 0;
        private static readonly int GRAY = 1;
        private static readonly int BLACK = 2;

        private Graph graph;
        private int[] distance, color, predecessor;
        int distanceLevel;

        public BreadthFirstSearch(Graph graph)
        {
            this.graph = graph;
            distance = new int[graph.numNodes];
            predecessor = new int[graph.numNodes];
            color = new int[graph.numNodes];
            distanceLevel = 0;
        }

        public void PerformBFS()
        {
            for (int u = 0; u < graph.numNodes; u++)
            {
                color[u] = WHITE;
                predecessor[u] = -1;
                distance[u] = distanceLevel;
            }
            for (int u = 0; u < graph.numNodes; u++)
            {
                if (color[u] == WHITE)
                {
                    VisitBFS(u);
                }
            }
        }

        private void VisitBFS(int u)
        {
            color[u] = GRAY;
            distance[u] = ++distanceLevel;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(u);
            NodeVisited?.Invoke(u, Color.Gray);

            while (queue.Count > 0)
            {
                Queue?.Invoke(queue);
                int currentNode = queue.Dequeue();
                for (int v = 0; v < graph.numNodes; v++)
                {
                    if (graph.adjMatrix[currentNode, v] > 0 && color[v] == WHITE)
                    {
                        color[v] = GRAY;
                        distance[v] = distance[currentNode] + 1;
                        predecessor[v] = currentNode;
                        queue.Enqueue(v);
                        NodeVisited?.Invoke(v, Color.Gray);
                    }
                }

                color[currentNode] = BLACK;
                NodeVisited?.Invoke(currentNode, Color.Black);
            }
        }

        public List<int> ObterCaminho(int origem, int destino)
        {
            PerformBFS();

            List<int> caminho = [];

            if (origem > destino && !graph.directedGraph)
            {
                ConstruirCaminho(destino, origem, caminho);
                caminho.Reverse();

                for (int i = 0; i < caminho.Count - 1; i++)
                {
                    int fromNode = caminho[i];
                    int toNode = caminho[i + 1];
                    EdgeVisited?.Invoke(fromNode, toNode, Color.OrangeRed);
                }

                return caminho;
            }

            ConstruirCaminho(origem, destino, caminho);

            for (int i = 0; i < caminho.Count - 1; i++)
            {
                int fromNode = caminho[i];
                int toNode = caminho[i + 1];
                EdgeVisited?.Invoke(fromNode, toNode, Color.OrangeRed);
            }

            return caminho;
        }

        private void ConstruirCaminho(int origem, int v, List<int> caminho)
        {
            if (origem == v)
                caminho.Add(origem);

            else if (predecessor[v] == -1)
                return;

            else
            {
                ConstruirCaminho(origem, predecessor[v], caminho);
                caminho.Add(v);
            }
        }
    }
}
