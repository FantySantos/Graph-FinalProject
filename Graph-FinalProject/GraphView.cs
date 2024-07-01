using System;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Graph_FinalProject
{
    public partial class GraphView : Form
    {
        private int numbersNode = 0;
        private Graph graph;
        private GraphRenderer graphRenderer;
        private Point? startPoint = null;
        private Point? endPoint = null;
        private bool isDragging = false;

        public GraphView()
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
                UpdateMatrixTable();
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

        private void OnQueue(Queue<int> queue)
        {
            richLogs.AppendText("Queue: ");
            foreach (var log in queue)
                richLogs.AppendText((log+1).ToString() + ' ');
            richLogs.AppendText("\n");
        }

        private void PictureGraph_MouseDown(object sender, MouseEventArgs e)
        {
            comboBoxGraphType.Enabled = false;
            if (!TrySelectNode(e.Location))
            {
                AddNewNode(e.X, e.Y);
            }
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
            graphRenderer.DrawNode(x, y);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            graph.AddNode();
            numbersNode = graphRenderer.GetNodePositions().Count;
            UpdateMatrixTable();
        }

        private void MatrixGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int currentValue = Convert.ToInt32(matrixGridView[e.ColumnIndex, e.RowIndex].Value);
                if (e.Button == MouseButtons.Left)
                {
                    graph.AddEdge(e.RowIndex, e.ColumnIndex, currentValue + 1);
                }
                else if (e.Button == MouseButtons.Right && currentValue > 0)
                {
                    graph.AddEdge(e.RowIndex, e.ColumnIndex, currentValue - 1);
                }

                UpdateMatrixTable();
                RedrawGraphFromMatrix(e.RowIndex, e.ColumnIndex);
            }
        }

        private void UpdateMatrixTable()
        {
            matrixGridView.Rows.Clear();
            matrixGridView.Columns.Clear();

            for (int i = 0; i < numbersNode; i++)
            {
                AddMatrixColumn(i);
                matrixGridView.Rows.Add();
                matrixGridView.Rows[i].HeaderCell.Value = $"{i + 1}";
            }

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

            if (graph.GetEdgeWeight(rowIndex, columnIndex) > 0)
                graphRenderer.DrawPermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked, graph.GetEdgeWeight(rowIndex, columnIndex));
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
                graphRenderer.DrawTemporaryLine(startPoint.Value, endPoint.Value, checkBoxGrid.Checked);
            }
        }

        private void PictureGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && startPoint.HasValue)
            {
                if (TryCompleteEdge(e.Location))
                    UpdateMatrixTable();
                else
                {
                    graphRenderer.ClearTemporaryLine(checkBoxGrid.Checked);
                    pictureGraph.Image = graphRenderer.GetGraphBitmap();
                }

                startPoint = null;
                endPoint = null;
                isDragging = false;
            }
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

        private void AddEdge()
        {
            if (startPoint.HasValue && endPoint.HasValue)
            {
                int weight = graphRenderer.GetWeight(startPoint.Value, endPoint.Value) + 1;
                int startIndex = graphRenderer.GetNodePositions().IndexOf(startPoint.Value);
                int endIndex = graphRenderer.GetNodePositions().IndexOf(endPoint.Value);

                if (startIndex != -1 && endIndex != -1)
                {
                    graph.AddEdge(startIndex, endIndex, weight);
                    graphRenderer.DrawPermanentLine(startPoint.Value, endPoint.Value, checkBoxGrid.Checked, weight);
                    pictureGraph.Image = graphRenderer.GetGraphBitmap();
                }
            }
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
            UpdateMatrixTable();
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
            string[] startEnd = textBox.Text.Split('-');
            if (startEnd.Length != 2)
            {
                MessageBox.Show("Please enter in the format 'x-y' where x and y are node numbers.");
                return;
            }

            if (!int.TryParse(startEnd[0], out int startNode) || !int.TryParse(startEnd[1], out int endNode))
            {
                MessageBox.Show("Invalid node numbers.");
                return;
            }

            startNode--;
            endNode--;

            if (startNode < 0 || startNode >= numbersNode || endNode < 0 || endNode >= numbersNode)
            {
                richLogs.AppendText($"Invalid node numbers.\n");
                return;
            }

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
            if (!int.TryParse(textBox.Text, out int nodeNumber) || nodeNumber < 1 || nodeNumber > numbersNode)
            {
                MessageBox.Show("Please enter a valid node number.");
                return;
            }

            (int inDegree, int outDegree) = graph.GetDegree(nodeNumber - 1);
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
            if (!int.TryParse(textBox.Text, out int nodeNumber) || nodeNumber < 1 || nodeNumber > numbersNode)
            {
                MessageBox.Show("Please enter a valid node number.");
                return;
            }

            (List<int> adjacencyIn, List<int> adjacencyOut) = graph.GetAdjacencyList(nodeNumber - 1);

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
            if (graph.numNodes > 0)
            {
                DepthFirstSearch DFS = new(graph);
                DFS.NodeVisited += OnNodeVisited;
                DFS.EdgeVisited += OnEdgeVisited;

                bool resultCheck = checkPredicate(DFS);

                for (int i = 0; i < graph.numNodes; i++)
                    richLogs.AppendText($"Node {i}: {DFS.GetDiscoveryTime(i)}/{DFS.GetFinishTime(i)}\n");

                richLogs.AppendText($"The graph is {(resultCheck ? trueMessage : falseMessage)}.");
            }
            else
            {
                MessageBox.Show("The graph is empty. Please load a valid graph.");
            }
        }

        private void RunSCC()
        {
            DepthFirstSearch DFS = new(graph);
            DFS.NodeVisited += OnNodeVisited;
            DFS.EdgeVisited += OnEdgeVisited;

            List<List<int>> SCC = DFS.FindStronglyConnectedComponents();

            for (int i = 0; i < SCC.Count; i++)
                richLogs.AppendText($"Strongly Connected Components {i+1}: {string.Join(", ", SCC[i])}\n");
        }

        private void RunTopologicalSort()
        {
            if (graph.numNodes > 0)
            {
                try
                {
                    DepthFirstSearch DFS = new(graph);
                    DFS.NodeVisited += OnNodeVisited;
                    DFS.EdgeVisited += OnEdgeVisited;

                    DFS.PerformDFS();
                    List<int> topologicalOrder = DFS.GetTopologicalOrder();

                    for (int i = 0; i < graph.numNodes; i++)
                        richLogs.AppendText($"Node {i}: {DFS.GetDiscoveryTime(i)}/{DFS.GetFinishTime(i)}\n");

                    richLogs.AppendText("Topological Order: " + string.Join(", ", topologicalOrder.Select(x => x + 1)) + "\n");
                }
                catch (InvalidOperationException ex)
                {
                    richLogs.AppendText(ex.Message + "\n");
                }
            }
            else
            {
                MessageBox.Show("The graph is empty. Please load a valid graph.");
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
            string[] startEnd = textBox.Text.Split(", ");
            if (startEnd.Length != 2)
            {
                MessageBox.Show("Please enter in the format 'x, y' where x and y are node numbers.");
                return;
            }

            if (!int.TryParse(startEnd[0], out int startNode) || !int.TryParse(startEnd[1], out int endNode))
            {
                MessageBox.Show("Invalid node numbers.");
                return;
            }

            BreadthFirstSearch BFS = new(graph);
            BFS.NodeVisited += OnNodeVisited;
            BFS.EdgeVisited += OnEdgeVisited;
            BFS.Queue += OnQueue;

            List<int> shortestPath = BFS.GetPath(startNode-1, endNode-1);
            if (shortestPath.Count() > 0)
                richLogs.AppendText($"Shortest path from {startNode} to {endNode}: {string.Join(" - ", shortestPath.Select(x => x + 1))}.\n");
            else
                richLogs.AppendText($"No path exists from {startNode} to {endNode}.\n");
        }

        private void ButtonRun_Click(object sender, EventArgs e)
        {
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            richLogs.Clear();

            if (comboBoxAlgorithms.SelectedIndex == 0)
                CheckEdgeExistence();
            else if (comboBoxAlgorithms.SelectedIndex == 1)
                GetVertexDegree();
            else if (comboBoxAlgorithms.SelectedIndex == 2)
                GetVertexAdjacency();
            else if (comboBoxAlgorithms.SelectedIndex == 3)
                RunGraphCheck(DepthFirstSearch => DepthFirstSearch.IsCyclic(), "cyclic", "acyclic");
            else if (comboBoxAlgorithms.SelectedIndex == 4)
                RunGraphCheck(DepthFirstSearch => DepthFirstSearch.IsConnected(), "connected", "disconnected");
            else if (comboBoxAlgorithms.SelectedIndex == 5)
                RunSCC();
            else if (comboBoxAlgorithms.SelectedIndex == 6)
                RunTopologicalSort();
            else if (comboBoxAlgorithms.SelectedIndex == 7)
                RunEulerianCycle();
            else if (comboBoxAlgorithms.SelectedIndex == 8)
                RunBFS();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ButtonRun_Click(this, new EventArgs());
        }

        private void ComboBoxAlgorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox.Clear();
            int[] comboBoxIndexes = { 3, 4, 5, 6, 7 };

            if (comboBoxIndexes.Contains(comboBoxAlgorithms.SelectedIndex))
                textBox.Enabled = false;
            else
                textBox.Enabled = true;
        }
    }
}