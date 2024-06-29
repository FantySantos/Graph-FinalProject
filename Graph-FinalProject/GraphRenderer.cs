using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Graph_FinalProject
{
    public class GraphRenderer
    {
        private Bitmap graphBitmap;
        private List<Point> nodePositions;
        private Dictionary<Tuple<Point, Point>, int> edgesPositions;
        private int numbersNode;
        private bool directedMode = false;

        public GraphRenderer(int width, int height)
        {
            graphBitmap = new Bitmap(width, height);
            nodePositions = [];
            edgesPositions = [];
            numbersNode = 0;
        }

        public void SetDirectedMode(bool directed)
        {
            directedMode = directed;
        }

        public void ResizeBitmap(int width, int height, bool draw)
        {
            graphBitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                g.Clear(Color.White);
                DrawGrid(draw, g);
            }
        }

        public bool IsEmpty()
        {
            return numbersNode != 0;
        }

        public Bitmap GetGraphBitmap()
        {
            return graphBitmap;
        }

        public List<Point> GetNodePositions()
        {
            return nodePositions;
        }

        public Dictionary<Tuple<Point, Point>, int> GetEdgesPositions()
        {
            return edgesPositions;
        }

        public int GetWeight(Point start, Point end)
        {
            var line = new Tuple<Point, Point>(start, end);
            var reverseLine = new Tuple<Point, Point>(end, start);

            if (directedMode && edgesPositions.ContainsKey(line))
                return edgesPositions[line];
            else if (!directedMode && (edgesPositions.ContainsKey(reverseLine) || edgesPositions.ContainsKey(line)))
                return edgesPositions[edgesPositions.ContainsKey(line) ? line : reverseLine];
            else
                return 0;
        }

        public void DrawNode(int x, int y)
        {
            numbersNode++;

            int cellSize = 40;

            int cellX = (x / cellSize) * cellSize + cellSize / 2;
            int cellY = (y / cellSize) * cellSize + cellSize / 2;

            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                DrawSingleNode(g, new Point(cellX, cellY), numbersNode);
            }

            nodePositions.Add(new Point(cellX, cellY));
        }

        public void RemoveLastNode(bool draw)
        {
            if (nodePositions.Count == 0) return;

            Point lastNode = nodePositions.Last();
            nodePositions.RemoveAt(nodePositions.Count - 1);

            var edgesToRemove = edgesPositions
                .Where(edge => edge.Key.Item1 == lastNode || edge.Key.Item2 == lastNode)
                .Select(edge => edge.Key)
                .ToList();

            foreach (var edge in edgesToRemove)
            {
                edgesPositions.Remove(edge);
            }

            numbersNode--;
            DrawGrid(draw);
        }

        public void DrawGrid(bool draw)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                DrawGrid(draw, g);
            }
        }

        private void DrawGrid(bool draw, Graphics g)
        {
            g.Clear(Color.White);

            if (draw)
            {
                int cellSize = 40;
                Pen pen = new(Color.LightGray, 1);

                for (int x = 0; x < graphBitmap.Width; x += cellSize)
                    g.DrawLine(pen, x, 0, x, graphBitmap.Height);

                for (int y = 0; y < graphBitmap.Height; y += cellSize)
                    g.DrawLine(pen, 0, y, graphBitmap.Width, y);
            }

            RedrawPermanentElements(g);
        }

        public void ClearTemporaryLine(bool draw)
        {
            DrawGrid(draw);
        }

        public void DrawTemporaryLine(Point start, Point end, bool draw)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                DrawGrid(draw, g);

                Pen tempLinePen = new(Color.Black, 2) { DashStyle = DashStyle.Dash };
                if (directedMode)
                    tempLinePen.CustomEndCap = new AdjustableArrowCap(6, 6);

                if (start == end)
                {
                    Rectangle rect = new(start.X - 20, start.Y - 35, 40, 40);
                    g.DrawArc(tempLinePen, rect, 160, 230);
                }
                else
                    g.DrawLine(tempLinePen, start, CalculateArrowEndPoint(start, end));

                RedrawPermanentElements(g);
            }
        }

        public void DrawPermanentLine(Point start, Point end, bool draw, int weight = 1)
        {
            var line = new Tuple<Point, Point>(start, end);
            var reverseLine = new Tuple<Point, Point>(end, start);

            if (directedMode)
                edgesPositions[line] = weight;
            else
            {
                if (edgesPositions.ContainsKey(line) || edgesPositions.ContainsKey(reverseLine))
                    edgesPositions[edgesPositions.ContainsKey(line) ? line : reverseLine] = weight;
                else
                    edgesPositions[line] = weight;
            }

            DrawGrid(draw);
        }

        public void RemovePermanentLine(Point start, Point end, bool draw)
        {
            var line = new Tuple<Point, Point>(start, end);
            var reverseLine = new Tuple<Point, Point>(end, start);

            if (directedMode && edgesPositions.ContainsKey(line))
                edgesPositions.Remove(line);
            else
            {
                if (!edgesPositions.Remove(line))
                    edgesPositions.Remove(reverseLine);
            }

            DrawGrid(draw);
        }

        public void Clear()
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                g.Clear(Color.White);
            }
        }

        private static void DrawSingleNode(Graphics g, Point position, int nodeNumber)
        {
            int ray = 20;
            float diameter = 2 * ray;

            RectangleF boundingBox = new(position.X - ray, position.Y - ray, diameter, diameter);

            Pen pen = new(Brushes.Black, 5);
            g.DrawEllipse(pen, boundingBox);
            g.FillEllipse(Brushes.White, boundingBox);

            string text = nodeNumber.ToString();
            Font font = new("Arial", 12, FontStyle.Bold);

            StringFormat stringFormat = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(text, font, Brushes.Black, boundingBox, stringFormat);
        }

        private static Point CalculateArrowEndPoint(Point start, Point end)
        {
            int nodeRadius = 20;
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
            end.X -= (int)(nodeRadius * Math.Cos(angle));
            end.Y -= (int)(nodeRadius * Math.Sin(angle));
            return end;
        }

        public void HighlightNode(Point position, Color color)
        {
            int ray = 20;
            float diameter = 2 * ray;

            RectangleF boundingBox = new(position.X - ray, position.Y - ray, diameter, diameter);

            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                Pen pen = new(Brushes.Black, 5);
                g.DrawEllipse(pen, boundingBox);
                g.FillEllipse(new SolidBrush(color), boundingBox);

                string text = (nodePositions.IndexOf(position) + 1).ToString();
                Font font = new("Arial", 12, FontStyle.Bold);

                StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                Color[] darkColors = { Color.Black, Color.Green, Color.Red, Color.Brown };

                if (darkColors.Contains(color))
                    g.DrawString(text, font, Brushes.White, boundingBox, stringFormat);
                else
                    g.DrawString(text, font, Brushes.Black, boundingBox, stringFormat);
            }
        }

        public void HighlightEdge(Point start, Point end, Color color)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                Pen highlightPen = new(color, 2);
                if (directedMode)
                    highlightPen.CustomEndCap = new AdjustableArrowCap(6, 6);

                if (start == end)
                {
                    Rectangle rect = new(start.X - 20, start.Y - 35, 40, 40);
                    g.DrawArc(highlightPen, rect, 160, 230);

                }
                else
                {
                    Point arrowEnd = CalculateArrowEndPoint(start, end);
                    g.DrawLine(highlightPen, start, arrowEnd);
                }

                DrawSingleNode(g, start, nodePositions.IndexOf(start) + 1);
                DrawSingleNode(g, end, nodePositions.IndexOf(end) + 1);

                if (color == Color.Red)
                    HighlightNode(start, Color.Gray);

                else if (color == Color.Blue)
                {
                    HighlightNode(start, Color.Gray);
                    HighlightNode(end, Color.Gray);
                }

                else if (color == Color.OrangeRed)
                {
                    HighlightNode(start, Color.Black);
                    HighlightNode(end, Color.Black);
                }
            }
        }

        private void RedrawPermanentElements(Graphics g)
        {
            Pen linePen = new(Color.Black, 2);
            if (directedMode)
                linePen.CustomEndCap = new AdjustableArrowCap(6, 6);

            foreach (var edge in edgesPositions)
            {
                Point start = edge.Key.Item1;
                Point end = edge.Key.Item2;
                int weight = edge.Value;

                if (start == end)
                {
                    Rectangle rect = new(start.X - 20, start.Y - 35, 40, 40);
                    g.DrawArc(linePen, rect, 160, 230);
                    if (weight > 1)
                    {
                        Point loopWeightPosition = new(start.X, start.Y - 45);
                        Font font = new("Arial", 10, FontStyle.Bold);
                        StringFormat stringFormat = new()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(weight.ToString(), font, Brushes.Black, loopWeightPosition, stringFormat);
                    }
                }
                else
                {
                    Point arrowEnd = CalculateArrowEndPoint(start, end);
                    g.DrawLine(linePen, start, arrowEnd);

                    if (weight > 1)
                    {
                        Point midPoint = new((start.X + end.X) / 2, ((start.Y + end.Y) / 2) - 15);
                        Font font = new("Arial", 10, FontStyle.Bold);
                        StringFormat stringFormat = new()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(weight.ToString(), font, Brushes.Black, midPoint, stringFormat);
                    }
                }
            }

            foreach (var position in nodePositions)
                DrawSingleNode(g, position, nodePositions.IndexOf(position) + 1);
        }
    }
}
