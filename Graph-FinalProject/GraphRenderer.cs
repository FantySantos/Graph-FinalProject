using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_FinalProject
{
    public class GraphRenderer
    {
        private Bitmap graphBitmap;
        private List<Point> nodePositions;
        private List<Tuple<Point, Point>> edgesPositions;
        private List<Point> selfLoops;
        private int numbersNode;
        private bool directedMode = false;

        public GraphRenderer(int width, int height)
        {
            graphBitmap = new Bitmap(width, height);
            nodePositions = new List<Point>();
            edgesPositions = new List<Tuple<Point, Point>>();
            selfLoops = new List<Point>();
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
                DrawGrid(draw);
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

        public List<Tuple<Point, Point>> GetEdgesPositions()
        {
            return edgesPositions;
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

        public void DrawGrid(bool draw)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                g.Clear(Color.White);

                if (draw)
                {
                    int cellSize = 40;
                    Pen pen = new Pen(Color.LightGray, 1);

                    for (int x = 0; x < graphBitmap.Width; x += cellSize)
                        g.DrawLine(pen, x, 0, x, graphBitmap.Height);

                    for (int y = 0; y < graphBitmap.Height; y += cellSize)
                        g.DrawLine(pen, 0, y, graphBitmap.Width, y);
                }

                RedrawPermanentElements(g);
            }
        }

        public void ClearTemporaryLine(bool draw)
        {
            DrawGrid(draw);
        }

        public void DrawTemporaryLine(Point start, Point end, bool draw)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                g.Clear(Color.White);

                if (draw)
                {
                    int cellSize = 40;
                    Pen pen = new Pen(Color.LightGray, 1);

                    for (int x = 0; x < graphBitmap.Width; x += cellSize)
                        g.DrawLine(pen, x, 0, x, graphBitmap.Height);

                    for (int y = 0; y < graphBitmap.Height; y += cellSize)
                        g.DrawLine(pen, 0, y, graphBitmap.Width, y);
                }

                Pen tempLinePen = new Pen(Color.Black, 2);
                tempLinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                if (directedMode)
                    tempLinePen.CustomEndCap = new AdjustableArrowCap(6, 6);

                g.DrawLine(tempLinePen, start, CalculateArrowEndPoint(start, end));

                RedrawPermanentElements(g);
            }
        }

        public void DrawPermanentLine(Point start, Point end, bool draw)
        {
            var line = new Tuple<Point, Point>(start, end);
            var reverseLine = new Tuple<Point, Point>(end, start);

            if (directedMode && !edgesPositions.Contains(line))
            {
                edgesPositions.Add(line);
                DrawGrid(draw);
            }
            else if (!edgesPositions.Contains(line) && !edgesPositions.Contains(reverseLine))
            {
                edgesPositions.Add(line);
                DrawGrid(draw);
            }
        }

        public void RemovePermanentLine(Point start, Point end, bool draw)
        {
            var line = new Tuple<Point, Point>(start, end);
            var reverseLine = new Tuple<Point, Point>(end, start);

            if (directedMode && edgesPositions.Contains(line))
            {
                edgesPositions.Remove(line);
            }
            else
            {
                if (edgesPositions.Contains(line))
                {
                    edgesPositions.Remove(line);
                }
                else if (edgesPositions.Contains(reverseLine))
                {
                    edgesPositions.Remove(reverseLine);
                }
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

        private void DrawSingleNode(Graphics g, Point position, int nodeNumber)
        {
            int ray = 20;
            float diameter = 2 * ray;

            RectangleF boundingBox = new RectangleF(position.X - ray, position.Y - ray, diameter, diameter);

            Pen pen = new Pen(Brushes.Black, 5);
            g.DrawEllipse(pen, boundingBox);
            g.FillEllipse(Brushes.White, boundingBox);

            string text = nodeNumber.ToString();
            Font font = new Font("Arial", 12, FontStyle.Bold);

            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(text, font, Brushes.Black, boundingBox, stringFormat);
        }

        private Point CalculateArrowEndPoint(Point start, Point end)
        {
            int nodeRadius = 20;
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
            end.X -= (int)(nodeRadius * Math.Cos(angle));
            end.Y -= (int)(nodeRadius * Math.Sin(angle));
            return end;
        }

        public void DrawLoop(Point position, bool draw)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                g.Clear(Color.White);

                if (draw)
                {
                    int cellSize = 40;
                    Pen pen = new Pen(Color.LightGray, 1);

                    for (int x = 0; x < graphBitmap.Width; x += cellSize)
                        g.DrawLine(pen, x, 0, x, graphBitmap.Height);

                    for (int y = 0; y < graphBitmap.Height; y += cellSize)
                        g.DrawLine(pen, 0, y, graphBitmap.Width, y);
                }

                Pen tempLinePen = new Pen(Color.Black, 2);

                // Adiciona o loop à lista de loops
                selfLoops.Add(position);

                RedrawPermanentElements(g);
            }
        }

        public void RemoveLoop(Point position, bool draw)
        {
            if (selfLoops.Contains(position))
            {
                selfLoops.Remove(position);
            }

            DrawGrid(draw);
        }

        public void RedrawPermanentElements(Graphics g)
        {
            Pen linePen = new Pen(Color.Black, 2);
            if (directedMode)
                linePen.CustomEndCap = new AdjustableArrowCap(6, 6);

            foreach (var line in edgesPositions)
            {
                Point end = CalculateArrowEndPoint(line.Item1, line.Item2);
                g.DrawLine(linePen, line.Item1, end);
            }

            foreach (var loopPosition in selfLoops)
            {
                if (directedMode)
                {
                    Rectangle rect = new Rectangle(loopPosition.X - 20, loopPosition.Y - 35, 40, 40);
                    g.DrawArc(linePen, rect, 160, 210);
                    
                }
                else
                {
                    Rectangle rect = new Rectangle(loopPosition.X - 20, loopPosition.Y - 35, 40, 40);
                    g.DrawArc(linePen, rect, 160, 230);
                }
            }


            foreach (var position in nodePositions)
            {
                DrawSingleNode(g, position, nodePositions.IndexOf(position) + 1);
            }
        }
    }
}
