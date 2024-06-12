using System;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

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
            graph = new Graph(0, radioButtonDirected.Checked);
        }

        private void PictureGraph_MouseDown(object sender, MouseEventArgs e)
        {
            radioButtonDirected.Enabled = false;
            radioButtonUndirected.Enabled = false;
            bool clickedOnNode = false;

            foreach (var position in graphRenderer.GetNodePositions())
            {
                if (IsPointInsideNode(position, e.Location))
                {
                    startPoint = new Point((int)position.X, (int)position.Y);
                    isDragging = true;
                    clickedOnNode = true;
                    break;
                }
            }

            if (!clickedOnNode)
            {
                graphRenderer.DrawNode(e.X, e.Y);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();

                graph.AddNode();
                numbersNode = graphRenderer.GetNodePositions().Count;

                UpdateMatrixTable();
            }
        }

        private void MatrixGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int currentValue = Convert.ToInt32(matrixGridView[e.ColumnIndex, e.RowIndex].Value);
                graph.AddEdge(e.RowIndex, e.ColumnIndex, currentValue + 1);
            }
            else if (e.Button == MouseButtons.Right)
            {
                int currentValue = Convert.ToInt32(matrixGridView[e.ColumnIndex, e.RowIndex].Value);
                if (currentValue > 0)
                    graph.AddEdge(e.RowIndex, e.ColumnIndex, currentValue - 1);
            }

            UpdateMatrixTable();
            RedrawGraphFromMatrix(e.RowIndex, e.ColumnIndex);
        }

        private void UpdateMatrixTable()
        {
            matrixGridView.Rows.Clear();
            matrixGridView.Columns.Clear();

            for (int i = 0; i < numbersNode; i++)
            {
                matrixGridView.Columns.Add($"{i + 1}", $"{i + 1}");
                matrixGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                matrixGridView.Columns[i].Width = 40;
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

        private void RedrawGraphFromMatrix(int rowIndex, int columnIndex)
        {
            // Obtenha a posição dos nós no gráfico
            Point startNodePosition = graphRenderer.GetNodePositions()[rowIndex];
            Point endNodePosition = graphRenderer.GetNodePositions()[columnIndex];

            // Verifique se o peso da aresta é maior que zero
            if (graph.GetEdgeWeight(rowIndex, columnIndex) > 0)
            {
                // Desenhe a linha permanente se o peso da aresta for maior que zero
                graphRenderer.DrawPermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked, graph.GetEdgeWeight(rowIndex, columnIndex));
            }
            else if (graph.GetEdgeWeight(rowIndex, columnIndex) == 0)
            {
                // Remova a linha permanente se o peso da aresta for zero
                graphRenderer.RemovePermanentLine(startNodePosition, endNodePosition, checkBoxGrid.Checked);
            }

            // Atualize a imagem do gráfico
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void CheckBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void pictureGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && startPoint.HasValue)
            {
                endPoint = e.Location;
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
                graphRenderer.DrawTemporaryLine(startPoint.Value, endPoint.Value, checkBoxGrid.Checked);
            }
        }

        private void pictureGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && startPoint.HasValue)
            {
                bool endInsideNodeRadius = false;
                foreach (var position in graphRenderer.GetNodePositions())
                {
                    if (IsPointInsideNode(position, e.Location))
                    {
                        endPoint = new Point((int)position.X, (int)position.Y);
                        endInsideNodeRadius = true;
                        break;
                    }
                }

                isDragging = false;

                if (endInsideNodeRadius)
                {
                    int weight = graphRenderer.GetWeight(startPoint.Value, endPoint.Value) + 1;

                    if (startPoint == endPoint)
                    {
                        graphRenderer.DrawPermanentLine(startPoint.Value, endPoint.Value, checkBoxGrid.Checked, weight);
                        int nodeIndex = graphRenderer.GetNodePositions().IndexOf(startPoint.Value);
                        if (nodeIndex != -1)
                            graph.AddEdge(nodeIndex, nodeIndex, weight);
                    }
                    else
                    {
                        graphRenderer.DrawPermanentLine(startPoint.Value, endPoint.Value, checkBoxGrid.Checked, weight);
                        int startIndex = graphRenderer.GetNodePositions().IndexOf(startPoint.Value);
                        int endIndex = graphRenderer.GetNodePositions().IndexOf(endPoint.Value);
                        if (startIndex != -1 && endIndex != -1)
                            graph.AddEdge(startIndex, endIndex, weight);
                    }
                    pictureGraph.Image = graphRenderer.GetGraphBitmap();
                }
                else
                {
                    graphRenderer.ClearTemporaryLine(checkBoxGrid.Checked);
                    pictureGraph.Image = graphRenderer.GetGraphBitmap();
                }

                startPoint = null;
                endPoint = null;

                UpdateMatrixTable();
            }
        }

        private bool IsPointInsideNode(Point nodePosition, Point clickPosition)
        {
            int ray = 20;
            float diameter = 2 * ray;
            RectangleF nodeBounds = new RectangleF(nodePosition.X - ray, nodePosition.Y - ray, diameter, diameter);
            return nodeBounds.Contains(clickPosition);
        }

        private void pictureGraph_SizeChanged(object sender, EventArgs e)
        {
            graphRenderer.ResizeBitmap(pictureGraph.Width, pictureGraph.Height, checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
        }

        private void buttonClearGraph_Click(object sender, EventArgs e)
        {
            graphRenderer.Clear();
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            graph = new Graph(0, radioButtonDirected.Checked);
            graphRenderer = new GraphRenderer(pictureGraph.Width, pictureGraph.Height);
            numbersNode = 0;
            UpdateMatrixTable();
            graphRenderer.DrawGrid(checkBoxGrid.Checked);
            pictureGraph.Image = graphRenderer.GetGraphBitmap();
            radioButtonDirected.Enabled = true;
            radioButtonUndirected.Enabled = true;
            if (!radioButtonDirected.Checked)
                radioButtonUndirected.Checked = true;
            graphRenderer.SetDirectedMode(radioButtonDirected.Checked);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonDirected.Checked)
                radioButtonUndirected.Checked = true;

            graphRenderer.SetDirectedMode(radioButtonDirected.Checked);
            graph = new Graph(0, radioButtonDirected.Checked);
        }

        private void matrixGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Point startNodePosition = graphRenderer.GetNodePositions()[e.RowIndex];
                Point endNodePosition = graphRenderer.GetNodePositions()[e.ColumnIndex];

                graphRenderer.DrawTemporaryLine(startNodePosition, endNodePosition, checkBoxGrid.Checked);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
        }

        private void matrixGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                graphRenderer.ClearTemporaryLine(checkBoxGrid.Checked);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox.Text, out int nodeNumber) && nodeNumber > 0 && nodeNumber <= numbersNode)
            {
                // Pinta o nó correspondente de verde
                graphRenderer.HighlightNode(graphRenderer.GetNodePositions()[nodeNumber - 1], Color.Yellow);
                pictureGraph.Image = graphRenderer.GetGraphBitmap();
            }
            else
            {
                MessageBox.Show("Número inválido ou fora do intervalo!");
            }
        }
    }
}