using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_FinalProject
{
    internal class SpecialSubsetsFinder
    {
        private Graph graph;

        public SpecialSubsetsFinder(Graph graph)
        {
            this.graph = graph;
        }

        public bool IsIndependentSet(List<int> vertices)
        {
            int[,] transposedIncidenceMatrix = TransposeIncidenceMatrix();
            int[] characteristicVector = CreateCharacteristicVector(vertices);
            int[] resultVector = MatrixVectorMultiply(transposedIncidenceMatrix, characteristicVector);
            
            foreach (int value in resultVector)
            {
                Console.Write(value);
                if (value > 1)
                    return false; 
            }
            return true; 
        }

        private int[,] TransposeIncidenceMatrix()
        {
            int edgesCount = 0;

            
            for (int i = 0; i < graph.numNodes; i++)
            {
                for (int j = 0; j < graph.numNodes; j++)
                {
                    if (graph.adjMatrix[i, j] != 0)
                        edgesCount++;
                }
            }

            int[,] incidenceMatrix = new int[graph.numNodes, edgesCount];
            int edgeIndex = 0;

            
            for (int i = 0; i < graph.numNodes; i++)
            {
                for (int j = 0; j < graph.numNodes; j++)
                {
                    if (graph.adjMatrix[i, j] != 0)
                    {
                        incidenceMatrix[i, edgeIndex] = 1; 
                        incidenceMatrix[j, edgeIndex] = 1; 
                        edgeIndex++;
                    }
                }
            }
            
            int[,] transposedMatrix = new int[edgesCount, graph.numNodes];
            for (int i = 0; i < edgesCount; i++)
            {
                for (int j = 0; j < graph.numNodes; j++)
                    transposedMatrix[i, j] = incidenceMatrix[j, i];
            }

            return transposedMatrix;
        }

        private int[] CreateCharacteristicVector(List<int> vertices)
        {
            int[] characteristicVector = new int[graph.numNodes];

            foreach (int vertex in vertices)
            {
                if (vertex >= 0 && vertex < graph.numNodes)
                {
                    characteristicVector[vertex] = 1;
                }
            }

            return characteristicVector;
        }

        private int[] MatrixVectorMultiply(int[,] matrix, int[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] result = new int[rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }

        public bool IsDominantSet(List<int> verticesSet)
        {
            bool[] covered = new bool[graph.numNodes];

            foreach (int v in verticesSet)
            {
                covered[v] = true;

                for (int j = 0; j < graph.numNodes; j++)
                {
                    if (graph.adjMatrix[v, j] != 0)
                        covered[j] = true;
                }
            }

            foreach (bool isCovered in covered)
            {
                if (!isCovered)
                    return false;
            }

            return true;
        }

        public bool IsCliqueSet(List<int> verticesSet)
        {
            for (int i = 0; i < verticesSet.Count; i++)
            {
                for (int j = i + 1; j < verticesSet.Count; j++)
                {
                    int v1 = verticesSet[i];
                    int v2 = verticesSet[j];

                    if (graph.adjMatrix[v1, v2] == 0 || graph.adjMatrix[v2, v1] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
