namespace Graph_FinalProject
{
    public class Graph
    {
        public int numNodes { get; private set; }
        private bool directedGraph;
        public int[,] adjMatrix;
        private int[] positions;

        public Graph(int initialNumNodes, bool directedGraph)
        {
            this.numNodes = initialNumNodes;
            this.directedGraph = directedGraph;
            adjMatrix = new int[initialNumNodes, initialNumNodes];
            positions = new int[initialNumNodes];
            InitializeMatrix();
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
                if (adjMatrix[i, j] > 0) return false;
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

        public string Print()
        {
            string richText = "";
            Console.WriteLine("Adjacency Matrix:");
            richText += "Adjacency Matrix:\n";
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    richText += $"{adjMatrix[i, j]} ";
                    Console.Write(adjMatrix[i, j] + " ");
                }
                Console.WriteLine();
                richText += "\n";
            }

            return richText;
        }

        public (int inDegree, int outDegree) GetDegree(int nodeIndex)
        {
            int inDegree = 0;
            int outDegree = 0;

            if (!directedGraph)
            {
                for (int i = 0; i < numNodes; i++)
                {
                    if (adjMatrix[i, nodeIndex] > 0)
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
                    if (adjMatrix[i, nodeIndex] > 0)
                        inDegree++;
                    if (adjMatrix[nodeIndex, i] > 0)
                        outDegree++;
                }
            }

            return (inDegree, outDegree);
        }

        public List<int> GetAdjacencyList(int nodeIndex)
        {
            List<int> adjacencyList = [];

            for (int j = 0; j < numNodes; j++)
            {
                if (adjMatrix[nodeIndex, j] > 0)
                    adjacencyList.Add(j+1);
                else
                {
                if (!directedGraph && adjMatrix[j, nodeIndex] > 0)
                    adjacencyList.Add(j+1);
                }
            }

            return adjacencyList;
        }
    }
}
