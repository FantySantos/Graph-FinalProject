using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_FinalProject
{
    internal class EulerianCycle
    {
        public event Action<int, int, Color>? EdgeVisited;
        private Graph graph;
        private bool isConnected;

        public EulerianCycle(Graph graph, bool isConnected)
        {
            this.graph = new(graph);
            this.isConnected = isConnected;
        }

        public bool IsEulerian()
        {
            if (!isConnected)
                return false;

            if (graph.directedGraph)
            {
                for (int i = 0; i < graph.numNodes; i++)
                {
                    var degrees = graph.GetDegree(i);
                    if (degrees.inDegree != degrees.outDegree)
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < graph.numNodes; i++)
                {
                    var degrees = graph.GetDegree(i);
                    if (degrees.inDegree % 2 != 0)
                        return false;
                }
            }

            return true;
        }

        public List<int> FindEulerianCycle()
        {
            Stack<int> currentPath = new Stack<int>();
            List<int> eulerianCycle = new List<int>();
            int currentNode = 0;

            if (!IsEulerian())
                return eulerianCycle;

            currentPath.Push(currentNode);

            while (currentPath.Count > 0)
            {
                if (!graph.AdjListIsEmpty(currentNode))
                {
                    currentPath.Push(currentNode);
                    int nextNode = graph.FirstAdjEdge(currentNode);

                    if (graph.directedGraph)
                    {
                        graph.RemoveEdge(currentNode, nextNode);
                    }
                    else
                    {
                        graph.RemoveEdge(currentNode, nextNode);
                        graph.RemoveEdge(nextNode, currentNode);
                    }

                    currentNode = nextNode;
                }
                else
                {
                    eulerianCycle.Add(currentNode);
                    currentNode = currentPath.Pop();
                }
            }

            eulerianCycle.Reverse();

            for (int i = 0; i < eulerianCycle.Count - 1; i++)
            {
                int fromNode = eulerianCycle[i];
                int toNode = eulerianCycle[i + 1];
                EdgeVisited?.Invoke(fromNode, toNode, Color.Orange);
            }

            return eulerianCycle;
        }
    }
}
