using System.DirectoryServices;

namespace Graph_FinalProject
{
    public class Graph
    {
        public int numNodes { get; private set; }
        public bool directedGraph;
        public int[,] adjMatrix;
        private int[] positions;
        private List<int> setA;
        private List<int> setB;

        public Graph(int initialNumNodes, bool directedGraph)
        {
            this.numNodes = initialNumNodes;
            this.directedGraph = directedGraph;
            adjMatrix = new int[initialNumNodes, initialNumNodes];
            positions = new int[initialNumNodes];
            setA = new List<int>();
            setB = new List<int>();
            InitializeMatrix();
        }

        public Graph(Graph original)
        {
            this.numNodes = original.numNodes;
            this.directedGraph = original.directedGraph;
            this.adjMatrix = new int[original.numNodes, original.numNodes];
            this.positions = new int[original.numNodes];
            setA = new List<int>();
            setB = new List<int>();

            for (int i = 0; i < original.numNodes; i++)
            {
                for (int j = 0; j < original.numNodes; j++)
                {
                    this.adjMatrix[i, j] = original.adjMatrix[i, j];
                }
                this.positions[i] = original.positions[i];
            }
        }

        private void InitializeMatrix()
        {
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++) adjMatrix[i, j] = 0;
                positions[i] = -1;
            }
        }

        public void AddNode()
        {
            numNodes++;
            int[,] newAdjMatrix = new int[numNodes, numNodes];
            int[] newPositions = new int[numNodes];

            for (int i = 0; i < numNodes - 1; i++)
            {
                for (int j = 0; j < numNodes - 1; j++)
                {
                    newAdjMatrix[i, j] = adjMatrix[i, j];
                }
                newPositions[i] = positions[i];
            }

            adjMatrix = newAdjMatrix;
            positions = newPositions;
            InitializeNewNode(numNodes - 1);
        }

        public void RemoveNode()
        {
            if (numNodes == 0) return;

            numNodes--;
            int[,] newAdjMatrix = new int[numNodes, numNodes];
            int[] newPositions = new int[numNodes];

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    newAdjMatrix[i, j] = adjMatrix[i, j];
                }
                newPositions[i] = positions[i];
            }

            adjMatrix = newAdjMatrix;
            positions = newPositions;
        }

        private void InitializeNewNode(int nodeIndex)
        {
            for (int j = 0; j < numNodes; j++)
            {
                adjMatrix[nodeIndex, j] = 0;
                adjMatrix[j, nodeIndex] = 0;
            }
            positions[nodeIndex] = -1;
        }

        public void AddEdge(int i, int j, int weight = 1)
        {
            adjMatrix[i, j] = weight;
            if (!directedGraph)
                adjMatrix[j, i] = weight;
        }

        public bool AdjListIsEmpty(int i)
        {
            for (int j = 0; j < numNodes; j++)
                if (adjMatrix[i, j] != 0) return false;
            return true;
        }

        public int FirstAdjEdge(int i)
        {
            positions[i] = -1;
            return ProxAdj(i);
        }

        public int ProxAdj(int i)
        {
            positions[i]++;
            while ((positions[i] < numNodes) &&
                   (adjMatrix[i, positions[i]] == 0)) positions[i]++;
            if (positions[i] == numNodes) return -1;
            else return positions[i];
        }

        public int RemoveEdge(int i, int j)
        {
            if (adjMatrix[i, j] == 0) return 0;
            else
            {
                int weight = adjMatrix[i, j];
                adjMatrix[i, j] = 0;
                if (!directedGraph)
                    adjMatrix[j, i] = 0;
                return weight;
            }
        }

        public bool HasEdge(int i, int j)
        {
            if (i < 0 || i >= numNodes || j < 0 || j >= numNodes)
                return false;

            return adjMatrix[i, j] != 0;
        }

        public int GetEdgeWeight(int i, int j)
        {
            return adjMatrix[i, j];
        }

        public Graph TransposedGraph()
        {
            Graph graphT = new(numNodes, directedGraph);
            for (int i = 0; i < numNodes; i++)
                if (!AdjListIsEmpty(i))
                {
                    int adj = FirstAdjEdge(i);
                    while (adj != -1)
                    {
                        graphT.AddEdge(adj, i, adjMatrix[i, adj]);
                        adj = ProxAdj(i);
                    }
                }
            return graphT;
        }

        public (int inDegree, int outDegree) GetDegree(int nodeIndex)
        {
            int inDegree = 0;
            int outDegree = 0;

            if (!directedGraph)
            {
                for (int i = 0; i < numNodes; i++)
                {
                    if (adjMatrix[i, nodeIndex] != 0)
                    {
                        if (i == nodeIndex)
                            inDegree++;
                        inDegree++;
                    }
                }

                outDegree = -1;
            }
            else
            {
                for (int i = 0; i < numNodes; i++)
                {
                    if (adjMatrix[i, nodeIndex] != 0)
                        inDegree++;
                    if (adjMatrix[nodeIndex, i] != 0)
                        outDegree++;
                }
            }

            return (inDegree, outDegree);
        }

        public (List<int>, List<int>) GetAdjacencyList(int nodeIndex)
        {
            List<int> adjacencyListIn = [];
            List<int> adjacencyListOut = [];

            if (!directedGraph)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    if (adjMatrix[j, nodeIndex] != 0 && j != nodeIndex)
                        adjacencyListIn.Add(j + 1);
                }
            }
            else
            {
                for (int j = 0; j < numNodes; j++)
                {
                    if (j != nodeIndex) 
                    {
                        if (adjMatrix[j, nodeIndex] != 0)
                            adjacencyListIn.Add(j + 1);
                        if (adjMatrix[nodeIndex, j] != 0)
                            adjacencyListOut.Add(j + 1);
                    }
                }
            }

            return (adjacencyListIn, adjacencyListOut);
        }

        public bool IsBipartite()
        {
            setA.Clear();
            setB.Clear();

            int[] colors = new int[numNodes];
            for (int i = 0; i < numNodes; i++)
                colors[i] = -1;

            for (int start = 0; start < numNodes; start++)
            {
                if (colors[start] == -1)
                {
                    Queue<int> queue = new Queue<int>();
                    queue.Enqueue(start);
                    colors[start] = 0;
                    setA.Add(start + 1);

                    while (queue.Count > 0)
                    {
                        int node = queue.Dequeue();

                        for (int neighbor = 0; neighbor < numNodes; neighbor++)
                        {
                            if (adjMatrix[node, neighbor] != 0)
                            {
                                if (colors[neighbor] == -1)
                                {
                                    colors[neighbor] = 1 - colors[node];
                                    queue.Enqueue(neighbor);
                                    if (colors[neighbor] == 0)
                                        setA.Add(neighbor + 1);
                                    else
                                        setB.Add(neighbor + 1);
                                }
                                else if (colors[neighbor] == colors[node])
                                {
                                    setA.Clear();
                                    setB.Clear();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public int[,] GetCostMatrix()
        {
            if (!IsBipartite())
                throw new InvalidOperationException("Graph is not bipartite.");

            if (setA == null || setB == null)
                throw new InvalidOperationException("One of the sets is null.");

            int maxSize = Math.Max(setA.Count, setB.Count);
            int[,] costMatrix = new int[maxSize, maxSize];

            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                    costMatrix[i, j] = 0;
            }

            for (int i = 0; i < setA.Count; i++)
            {
                for (int j = 0; j < setB.Count; j++)
                {
                    int aIndex = setA[i] - 1;
                    int bIndex = setB[j] - 1;
                    costMatrix[i, j] = adjMatrix[aIndex, bIndex];
                }
            }

            return costMatrix;
        }


        public bool IsPlanar()
        {
            if (numNodes <= 4) return true;

            int edgeCount = 0;
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                    if (adjMatrix[i, j] != 0) edgeCount++;
            }

            if (!directedGraph) edgeCount /= 2;

            if (IsBipartite())
                return edgeCount <= 2 * numNodes - 4;
            else
                return edgeCount <= 3 * numNodes - 6;
        }

    }
}
