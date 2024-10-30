using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Drawing.Drawing2D;
using HungarianAlgorithm;

namespace Graph_FinalProject
{
    public partial class Kintiny : Form
    {
        private int numbersNode = 0;
        private Graph graph;
        private GraphRenderer graphRenderer;
        private Point? startPoint = null;
        private Point? endPoint = null;
        private bool isDragging = false;
        private bool rightButtonDragging = false;

        public Kintiny()
        {
            InitializeComponent();
            graphRenderer = new GraphRenderer(pictureGraph.Width, pictureGraph.Height);
            comboBoxGraphType.SelectedIndex = 0;
            comboBoxAlgorithms.SelectedIndex = 0;
            graph = new Graph(0, false);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                RemoveLastNode();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void RemoveLastNode()
        {
            if (numbersNode > 0)
            {
                numbersNode--;
                graph.RemoveNode();
                graphRenderer.RemoveLastNode(checkBoxGrid.Checked);
                matrixGridView.Columns.RemoveAt(graph.numNodes);
                if (graph.numNodes != 0)
                    matrixGridView.Rows.RemoveAt(graph.numNodes);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
        }

        private void OnNodeVisited(int nodeIndex, Color color)
        {
            graphRenderer.HighlightNode(graphRenderer.GetNodePositions()[nodeIndex], color);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();

            if (color == Color.Black || color == Color.Gray)
            {
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
                Application.DoEvents();
                Thread.Sleep(trackBar.Value);
            }
        }

        private void OnEdgeVisited(int startNodeIndex, int endNodeIndex, Color color)
        {
            graphRenderer.HighlightEdge(graphRenderer.GetNodePositions()[startNodeIndex], graphRenderer.GetNodePositions()[endNodeIndex], color);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            Application.DoEvents();
            Thread.Sleep(trackBar.Value);
        }

        private void OnClear()
        {
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            Application.DoEvents();
            Thread.Sleep(350);
        }

        private void OnQueue(Queue<int> queue)
        {
            richLogs.AppendText("Queue: ");
            foreach (var log in queue)
                richLogs.AppendText((log + 1).ToString() + ' ');
            richLogs.AppendText("\n");
        }

        private void OnPrintDistPred(int[] dist, int[] predecessor)
        {
            richLogs.Clear();
            for (int i = 0; i < graph.numNodes; i++)
                richLogs.AppendText($"Node {i + 1} - {{ Predecessor: {predecessor[i] + 1}, Cost: {dist[i]} }}\n");
        }

        private void PictureGraph_MouseDown(object sender, MouseEventArgs e)
        {
            comboBoxGraphType.Enabled = false;

            bool isNodeSelected = TrySelectNode(e.Location);

            if (e.Button == MouseButtons.Right)
                rightButtonDragging = true;

            if (checkBoxMoveNode.Checked && isNodeSelected)
                return;

            if (!isNodeSelected)
                AddNewNode(e.X, e.Y);

        }

        private bool TrySelectNode(Point location)
        {
            foreach (var position in graphRenderer.GetNodePositions())
            {
                if (IsPointInsideNode(position, location))
                {
                    startPoint = new Point((int)position.X, (int)position.Y);
                    isDragging = true;
                    return true;
                }
            }
            return false;
        }

        private void AddNewNode(int x, int y)
        {
            graph.AddNode();
            graphRenderer.DrawNode(x, y);
            numbersNode = graphRenderer.GetNodePositions().Count;
            UpdateMatrixTable();
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void MatrixGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int currentValue = Convert.ToInt32(matrixGridView[e.ColumnIndex, e.RowIndex].Value);

                if (e.Button == MouseButtons.Left)
                    graph.AddEdge(e.RowIndex, e.ColumnIndex, currentValue + 1);
                else if (e.Button == MouseButtons.Right)
                    graph.AddEdge(e.RowIndex, e.ColumnIndex, currentValue - 1);
            }

            UpdateCellsMatrixTable();
        }

        private void UpdateMatrixTable()
        {
            int index = graph.numNodes -1;
            AddMatrixColumn(index);
            matrixGridView.Rows.Add();
            matrixGridView.Rows[index].HeaderCell.Value = $"{index + 1}";
            matrixGridView.Rows[index].Cells[index].Value = 0;

            for (int i = 0; i < matrixGridView.Columns.Count; i++)
            {
                matrixGridView[index, i].Value = 0;
                matrixGridView[i, index].Value = 0;
            }
        }

        private void UpdateCellsMatrixTable()
        {
            for (int i = 0; i < numbersNode; i++)
            {
                for (int j = 0; j < numbersNode; j++)
                {
                    matrixGridView[j, i].Value = graph.GetEdgeWeight(i, j);
                }
            }
        }

        private void AddMatrixColumn(int index)
        {
            matrixGridView.Columns.Add($"{index + 1}", $"{index + 1}");
            matrixGridView.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
            matrixGridView.Columns[index].Width = 40;
        }

        private void RedrawGraphFromMatrix(int rowIndex, int columnIndex)
        {
            Point startNodePosition = graphRenderer.GetNodePositions()[rowIndex];
            Point endNodePosition = graphRenderer.GetNodePositions()[columnIndex];
            int weight = graph.GetEdgeWeight(rowIndex, columnIndex);

            if (weight != 0)
                graphRenderer.DrawPermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked, weight);
            else
                graphRenderer.RemovePermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked);

            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void CheckBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void PictureGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && startPoint.HasValue)
            {
                endPoint = e.Location;
                pictureGraph.Image = graphRenderer.GetGraphBitmap();

                if (checkBoxMoveNode.Checked)
                    graphRenderer.DrawTemporaryNode(startPoint.Value, endPoint.Value, checkBoxGrid.Checked);
                else
                    graphRenderer.DrawTemporaryLine(startPoint.Value, endPoint.Value, checkBoxGrid.Checked);
            }
        }

        private void PictureGraph_MouseUp(object sender, MouseEventArgs e)
        {

            if (!isDragging || !startPoint.HasValue)
                return;

            if (checkBoxMoveNode.Checked)
            {
                int nodeIndex = graphRenderer.GetNodePositions().IndexOf(startPoint.Value);
                if (nodeIndex != -1)
                {
                    graphRenderer.UpdateNodePosition(nodeIndex, e.X, e.Y);
                    graphRenderer.DrawGrid(checkBoxGrid.Checked);
                }
            }

            else if (!TryCompleteEdge(e.Location))
                graphRenderer.ClearTemporaryLine(checkBoxGrid.Checked);

            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            startPoint = null;
            endPoint = null;
            isDragging = false;
        }

        private bool TryCompleteEdge(Point location)
        {
            foreach (var position in graphRenderer.GetNodePositions())
            {
                if (IsPointInsideNode(position, location))
                {
                    endPoint = new Point((int)position.X, (int)position.Y);
                    AddEdge();
                    return true;
                }
            }

            return false;
        }

        private static bool IsPointInsideNode(Point nodePosition, Point clickPosition)
        {
            int ray = 20;
            float diameter = 2 * ray;
            RectangleF nodeBounds = new RectangleF(nodePosition.X - ray, nodePosition.Y - ray, diameter, diameter);
            return nodeBounds.Contains(clickPosition);
        }

        private void PictureGraph_SizeChanged(object sender, EventArgs e)
        {
            if (pictureGraph == null || graphRenderer == null) return;

            graphRenderer.ResizeBitmap(pictureGraph.Width, pictureGraph.Height, checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void ButtonClearGraph_Click(object sender, EventArgs e)
        {
            ClearGraph();
        }

        private void ClearGraph()
        {
            graphRenderer.Clear();
            bool typeGraph = GetSelectedGraphType();
            graph = new Graph(0, typeGraph);
            graphRenderer = new GraphRenderer(pictureGraph.Width, pictureGraph.Height);
            graphRenderer.SetDirectedMode(typeGraph);
            numbersNode = 0;
            matrixGridView.Rows.Clear();
            matrixGridView.Columns.Clear();
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            comboBoxGraphType.Enabled = true;
            richLogs.Clear();
        }

        private bool GetSelectedGraphType()
        {
            return comboBoxGraphType.SelectedIndex == 0 ? false : true;
        }

        private void ComboBoxGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool directedGraph = GetSelectedGraphType();
            graphRenderer.SetDirectedMode(directedGraph);
            graph = new Graph(0, directedGraph);
        }

        private void MatrixGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                DrawTemporaryLineForCell(e.RowIndex, e.ColumnIndex);
        }

        private void DrawTemporaryLineForCell(int rowIndex, int columnIndex)
        {
            Point startNodePosition = graphRenderer.GetNodePositions()[rowIndex];
            Point endNodePosition = graphRenderer.GetNodePositions()[columnIndex];
            graphRenderer.DrawTemporaryLine(startNodePosition, endNodePosition, checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void MatrixGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                graphRenderer.ClearTemporaryLine(checkBoxGrid.Checked);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
        }

        private void CheckEdgeExistence()
        {
            if (!TryParseEdgeInput(textBox.Text, out int startNode, out int endNode)) return;

            bool exists = graph.HasEdge(startNode, endNode);
            richLogs.AppendText($"Edge from {startNode + 1} to {endNode + 1} exists: {exists}\n");

            if (exists)
            {
                graphRenderer.HighlightEdge(graphRenderer.GetNodePositions()[startNode], graphRenderer.GetNodePositions()[endNode], Color.Orange);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
        }

        private void GetVertexDegree()
        {
            if (!int.TryParse(textBox.Text, out int nodeNumber))
            {
                DisplayError("Please enter a valid node number.");
                return;
            }

            int actualNode = nodeNumber - 1;
            if (!IsNodeInRange(actualNode)) return;

            (int inDegree, int outDegree) = graph.GetDegree(actualNode);
            if (outDegree == -1)
                richLogs.AppendText($"Degree of node {nodeNumber}: {inDegree}\n");
            else
            {
                richLogs.AppendText($"In-Degree of node {nodeNumber}: {inDegree}\n");
                richLogs.AppendText($"Out-Degree of node {nodeNumber}: {outDegree}\n");
            }
        }

        private void GetVertexAdjacency()
        {
            if (!int.TryParse(textBox.Text, out int nodeNumber))
            {
                DisplayError("Please enter a valid node number.");
                return;
            }

            int actualNode = nodeNumber - 1;
            if (!IsNodeInRange(actualNode)) return;

            (List<int> adjacencyIn, List<int> adjacencyOut) = graph.GetAdjacencyList(actualNode);

            if (adjacencyIn == null || adjacencyOut == null)
            {
                DisplayError($"Node {nodeNumber} has no adjacency list.");
                return;
            }

            if (adjacencyOut.Count == 0)
                richLogs.AppendText($"Adjacency of node {nodeNumber}: {string.Join(", ", adjacencyIn)}\n");
            else
            {
                richLogs.AppendText($"In-Adjacency of node {nodeNumber}: {string.Join(", ", adjacencyIn)}\n");
                richLogs.AppendText($"Out-Adjacency of node {nodeNumber}: {string.Join(", ", adjacencyOut)}\n");
            }

            if (adjacencyIn.Count != 0)
            {
                foreach (int node in adjacencyIn)
                    graphRenderer.HighlightNode(graphRenderer.GetNodePositions()[node - 1], Color.Orange);
            }
            if (adjacencyOut.Count != 0)
            {
                foreach (int node in adjacencyOut)
                    graphRenderer.HighlightNode(graphRenderer.GetNodePositions()[node - 1], Color.Yellow);
            }

            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }


        private void RunGraphCheck(Func<DepthFirstSearch, bool> checkPredicate, string trueMessage, string falseMessage)
        {
            DepthFirstSearch DFS = new(graph);
            DFS.NodeVisited += OnNodeVisited;
            DFS.EdgeVisited += OnEdgeVisited;

            bool resultCheck = checkPredicate(DFS);

            for (int i = 0; i < graph.numNodes; i++)
                richLogs.AppendText($"Node {i + 1}: {DFS.GetDiscoveryTime(i)}/{DFS.GetFinishTime(i)}\n");

            richLogs.AppendText($"The graph is {(resultCheck ? trueMessage : falseMessage)}.");
        }

        private void RunSCC()
        {
            DepthFirstSearch DFS = new(graph);
            DFS.NodeVisited += OnNodeVisited;
            DFS.EdgeVisited += OnEdgeVisited;

            List<List<int>> SCC = DFS.FindStronglyConnectedComponents();

            for (int i = 0; i < SCC.Count; i++)
                richLogs.AppendText($"Strongly Connected Components {i + 1}: {string.Join(", ", SCC[i])}\n");
        }

        private void RunTopologicalSort()
        {
            try
            {
                DepthFirstSearch DFS = new(graph);
                DFS.NodeVisited += OnNodeVisited;
                DFS.EdgeVisited += OnEdgeVisited;

                DFS.PerformDFS();
                List<int> topologicalOrder = DFS.GetTopologicalOrder();

                for (int i = 0; i < graph.numNodes; i++)
                    richLogs.AppendText($"Node {i + 1}: {DFS.GetDiscoveryTime(i)}/{DFS.GetFinishTime(i)}\n");

                richLogs.AppendText("Topological Order: " + string.Join(", ", topologicalOrder.Select(x => x + 1)) + "\n");
            }
            catch (InvalidOperationException ex)
            {
                richLogs.AppendText(ex.Message + "\n");
            }
        }

        private void RunEulerianCycle()
        {
            DepthFirstSearch DFS = new(graph);
            DFS.NodeVisited += OnNodeVisited;
            DFS.EdgeVisited += OnEdgeVisited;

            EulerianCycle fleury = new(graph, DFS.IsConnected());
            fleury.EdgeVisited += OnEdgeVisited;

            List<int> eulerianCycle = fleury.FindEulerianCycle();

            if (eulerianCycle.Count() == 0)
            {
                richLogs.AppendText("No Eulerian cycle found.\n");
                return;
            }

            richLogs.AppendText($"Eulerian cycle: {string.Join(" - ", eulerianCycle.Select(x => x + 1))}.\n");
        }

        private void RunBFS()
        {
            if (!TryParseVertexInput(textBox.Text, out int startNode, out int endNode)) return;

            BreadthFirstSearch BFS = new(graph);
            BFS.NodeVisited += OnNodeVisited;
            BFS.EdgeVisited += OnEdgeVisited;
            BFS.Queue += OnQueue;
            BFS.Clear += OnClear;

            List<int> shortestPath = BFS.GetPath(startNode, endNode);
            if (shortestPath.Count() > 0)
                richLogs.AppendText($"Shortest path from {startNode + 1} to {endNode + 1}: {string.Join(" - ", shortestPath.Select(x => x + 1))}.\n");
            else
                richLogs.AppendText($"No path exists from {startNode + 1} to {endNode + 1}.\n");
        }

        private void RunBellmanFord()
        {
            if (!TryParseVertexInput(textBox.Text, out int startNode, out int endNode)) return;

            try
            {
                BellmanFord bellmanFord = new(graph);
                bellmanFord.NodeVisited += OnNodeVisited;
                bellmanFord.EdgeVisited += OnEdgeVisited;
                bellmanFord.Clear += OnClear;
                bellmanFord.Print += OnPrintDistPred;

                (List<int> path, int cost) = bellmanFord.GetLeastCostPath(startNode, endNode);

                richLogs.AppendText($"\nThe least cost path from {startNode + 1} to {endNode + 1}: {string.Join(" - ", path.Select(x => x + 1))}.\nCost: {cost}.\n");
            }

            catch (InvalidOperationException ex)
            {
                richLogs.AppendText(ex.Message + "\n");
            }  
        }

        private void RunIndependenceChecker()
        {
            if (!TryParseVertexList(textBox.Text, out List<int> setVertices))
                return;

            SpecialSubsetsFinder specialSubsets = new SpecialSubsetsFinder(graph);

            string vertexSet = string.Join(", ", setVertices.Select(x => x + 1));

            if (specialSubsets.IsIndependentSet(setVertices))
                richLogs.AppendText($"The set of vertices {{ {vertexSet} }} is independent.\n");
            else
                richLogs.AppendText($"The set of vertices {{ {vertexSet} }} is not independent.\n");

        }

        private void RunDominantChecker()
        {
            if (!TryParseVertexList(textBox.Text, out List<int> setVertices))
                return;

            SpecialSubsetsFinder specialSubsets = new SpecialSubsetsFinder(graph);

            string vertexSet = string.Join(", ", setVertices.Select(x => x + 1));

            if (specialSubsets.IsDominantSet(setVertices))
                richLogs.AppendText($"The set of vertices {{ {vertexSet} }} is dominant.\n");
            else
                richLogs.AppendText($"The set of vertices {{ {vertexSet} }} is not dominant.\n");
        }

        private void RunCliqueChecker()
        {
            if (!TryParseVertexList(textBox.Text, out List<int> setVertices))
                return;

            SpecialSubsetsFinder specialSubsets = new SpecialSubsetsFinder(graph);
            string vertexSet = string.Join(", ", setVertices.Select(x => x + 1));

            if (specialSubsets.IsCliqueSet(setVertices))
                richLogs.AppendText($"The set of vertices {{ {vertexSet} }} is clique.\n");
            else
                richLogs.AppendText($"The set of vertices {{ {vertexSet} }} is not clique.\n");
        }

        private void RunIsPlanar()
        {
            if (graph.IsPlanar())
                richLogs.AppendText("The graph is planar.\n");
            else
                richLogs.AppendText("The graph is not planar.\n");
        }

        private void RunPrim()
        {
            Prim prim = new Prim(graph);
            prim.NodeVisited += OnNodeVisited;
            prim.EdgeVisited += OnEdgeVisited;
            prim.Clear += OnClear;
            prim.Print += OnPrintDistPred;

            List <(int from, int to)> mstEdges = prim.GetMinimumSpanningTreeEdges();

            richLogs.AppendText("\nMinimum spanning tree: { ");

            for (int i = 0; i < mstEdges.Count; i++)
            {
                var edge = mstEdges[i];
                richLogs.AppendText($"({edge.from + 1}, {edge.to + 1})");

                if (i < mstEdges.Count - 1)
                    richLogs.AppendText(", ");
            }

            richLogs.AppendText(" }");
        }

        private void RunHungarian()
        {
            try
            {
                richLogs.Clear();
                int[,] costMatrix = graph.GetCostMatrix();
                int[,] OldCostMatrix = graph.GetCostMatrix();

                int[] result = HungarianAlgorithm.HungarianAlgorithm.FindAssignments(OldCostMatrix);

                PrintAlignedMatrix(costMatrix, result);
                richLogs.AppendText("\n");
                PrintAlignedMatrix(OldCostMatrix, result);

                richLogs.AppendText($"\nTotal cost: ");

                int custoTotal = 0;
                for (int i = 0; i < result.Length; i++)
                {
                    custoTotal += costMatrix[i, result[i]];
                    richLogs.AppendText($"{costMatrix[i, result[i]]}");
                    if (i < result.Length - 1) richLogs.AppendText(" + ");
                }
                    richLogs.AppendText($" = {custoTotal}");
            }

            catch (InvalidOperationException ex)
            {
                richLogs.AppendText(ex.Message + "\n");
            }
        }

        private void PrintAlignedMatrix(int[,] matrix, int[] assignments)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int maxElement = matrix.Cast<int>().Max();
            int maxElementLength = maxElement.ToString().Length;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string formattedValue = matrix[i, j].ToString().PadLeft(maxElementLength, '0').PadRight(maxElementLength+3, ' ');

                    if (assignments[i] == j)
                    {
                        if (matrix[i, j] == 0)
                            richLogs.SelectionColor = Color.Red;
                        richLogs.AppendText(formattedValue);
                        richLogs.SelectionColor = Color.Black;
                    }
                    else
                    {
                        richLogs.AppendText(formattedValue);
                    }
                }
                richLogs.AppendText(Environment.NewLine);
            }

            richLogs.SelectionColor = Color.Black;
        }


        private void ButtonRun_Click(object sender, EventArgs e)
        {
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            richLogs.Clear();

            if (graph.numNodes <= 0)
            {
                DisplayError("The graph is empty. Please load a valid graph.");
                return;
            }

            switch (comboBoxAlgorithms.SelectedIndex)
            {
                case 0: CheckEdgeExistence(); break;
                case 1: GetVertexDegree(); break;
                case 2: GetVertexAdjacency(); break;
                case 3: RunGraphCheck(DFS => DFS.IsCyclic(), "cyclic", "acyclic"); break;
                case 4: RunGraphCheck(DFS => DFS.IsConnected(), "connected", "disconnected"); break;
                case 5: RunSCC(); break;
                case 6: RunTopologicalSort(); break;
                case 7: RunEulerianCycle(); break;
                case 8: RunBFS(); break;
                case 9: RunBellmanFord(); break;
                case 10: RunIndependenceChecker(); break;
                case 11: RunDominantChecker(); break;
                case 12: RunCliqueChecker(); break;
                case 13: RunIsPlanar(); break;
                case 14: RunPrim(); break;
                case 15: RunHungarian(); break;
            }
        }

        private bool TryParseEdgeInput(string input, out int startNode, out int endNode)
        {
            string[] nodes = input.Split('-');
            if (nodes.Length == 2 && int.TryParse(nodes[0], out startNode) && int.TryParse(nodes[1], out endNode))
            {
                startNode--; endNode--;
                if (IsNodeInRange(startNode) && IsNodeInRange(endNode))
                    return true;
            }
            else
                DisplayError("Please enter in the format 'x-y' where x and y are node numbers.");
            startNode = endNode = -1;
            return false;
        }

        private bool TryParseVertexInput(string input, out int startNode, out int endNode)
        {
            string[] nodes = input.Split(',');
            if (nodes.Length == 2 && int.TryParse(nodes[0], out startNode) && int.TryParse(nodes[1], out endNode))
            {
                startNode--; endNode--;
                if (IsNodeInRange(startNode) && IsNodeInRange(endNode))
                    return true;
            }
            else
                DisplayError("Please enter in the format 'x, y' where x and y are node numbers.");
            startNode = endNode = -1;
            return false;
        }

        private bool TryParseVertexList(string input, out List<int> setVertices)
        {
            setVertices = new List<int>();

            string[] items = input.Split(",");

            foreach (var item in items)
            {
                if (int.TryParse(item, out int vertex))
                {
                    vertex--;
                    if (IsNodeInRange(vertex))
                        setVertices.Add(vertex);
                    else
                        return false;
                }
                else
                {
                    DisplayError($"Invalid input '{item}'. Please enter only numbers separated by ', '.");
                    return false;
                }
            }

            return true;
        }

        private bool IsNodeInRange(int node)
        {
            if (node < 0 || node >= graph.numNodes)
            {
                DisplayError($"Node {node + 1} is out of range. Please enter a valid node number.");
                return false;
            }
            return true;
        }

        private void DisplayError(string message)
        {
            MessageBox.Show(message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ButtonRun_Click(this, new EventArgs());
        }

        private void ComboBoxAlgorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox.Clear();
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();

            int[] comboBoxIndexes = { 3, 4, 5, 6, 7, 13, 14, 15};

            if (comboBoxIndexes.Contains(comboBoxAlgorithms.SelectedIndex))
                textBox.Enabled = false;
            else
                textBox.Enabled = true;
        }

        private void CircularLayout_Click(object sender, EventArgs e)
        {
            graphRenderer.CircularLayout(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void AddEdge()
        {
            if (startPoint.HasValue && endPoint.HasValue)
            {
                int startIndex = graphRenderer.GetNodePositions().IndexOf(startPoint.Value);
                int endIndex = graphRenderer.GetNodePositions().IndexOf(endPoint.Value);
                int weight = graph.GetEdgeWeight(startIndex, endIndex);

                if (startIndex != -1 && endIndex != -1)
                {
                    if (!rightButtonDragging)
                        graph.AddEdge(startIndex, endIndex, weight + 1);
                    else
                        graph.AddEdge(startIndex, endIndex, weight - 1);
                }

                rightButtonDragging = false;
                UpdateCellsMatrixTable();
            }
        }

        private void matrixGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Point startNodePosition = graphRenderer.GetNodePositions()[e.RowIndex];
                Point endNodePosition = graphRenderer.GetNodePositions()[e.ColumnIndex];
                int weight = graph.GetEdgeWeight(e.RowIndex, e.ColumnIndex);

                if (weight != 0)
                    graphRenderer.DrawPermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked, weight);
                else
                    graphRenderer.RemovePermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked);

                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
        }

        private void matrixGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    int weight = Convert.ToInt32(matrixGridView[e.ColumnIndex, e.RowIndex].Value);

                    if (weight != 0)
                        graph.AddEdge(e.RowIndex, e.ColumnIndex, weight);
                    else
                        graph.RemoveEdge(e.RowIndex, e.ColumnIndex);

                    UpdateCellsMatrixTable();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                matrixGridView[e.ColumnIndex, e.RowIndex].Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}