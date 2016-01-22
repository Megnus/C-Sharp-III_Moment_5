using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DiagramGenerator
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms172873.aspx
    /// </summary>

    public class DiagramData
    {
        public PointCollection coordinatePoints;
        public PointCollection canvasPoints;

        private Point lastCanvasPoint;

        private const int offsetX = 35;
        private const int offsetY = 35;

        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }
        public int NumberOfDevisionsX { get; set; }
        public int NumberOfDevisionsY { get; set; }
        public int InterValValueX { get; set; }
        public int InterValValueY { get; set; }

        public DiagramData()
        {
            this.coordinatePoints = new PointCollection();
            this.canvasPoints = new PointCollection();
        }

        public void AddNewPoint(Point point)
        {
            lastCanvasPoint = Transpose(point);
            coordinatePoints.Add(point);
            canvasPoints.Add(lastCanvasPoint);
        }

        public int NumberOfPoints 
        {
            get
            {
                return coordinatePoints.Count;
            }
        }

        public Point LastCanvasPoint
        {
            get
            {
                return lastCanvasPoint;
            }
        }

        public Point GetCanvasPoint(int index)
        {
            return canvasPoints[index];
        }

        public void SortPoints()
        {
            coordinatePoints = new PointCollection(coordinatePoints.OrderBy(p => p.X).ThenByDescending(p => p.Y).ToList());
            canvasPoints = new PointCollection(canvasPoints.OrderBy(p => p.X).ThenByDescending(p => p.Y).ToList());
        }

        private Point Transpose(Point point)
        {
            Point transposedPoint = new Point();
            transposedPoint.X = (int)((CanvasWidth - offsetX * 2) * point.X / (InterValValueX * NumberOfDevisionsX)) + offsetX;
            transposedPoint.Y = (int)CanvasHeight - (int)((CanvasHeight - offsetY * 2) * point.Y / (InterValValueY * NumberOfDevisionsY)) - offsetY;
            return transposedPoint;
        }

        public Point Reverse(Point point)
        {
            Point reversedPoint = new Point();
            //transposedPoint.X = (int)((CanvasWidth - offsetX * 2) * point.X / (InterValValueX * NumberOfDevisionsX)) + offsetX;
            reversedPoint.X = (double)(point.X - offsetX) * (InterValValueX * NumberOfDevisionsX) / ((CanvasWidth - offsetX * 2));

            //transposedPoint.Y = (int)CanvasHeight - (int)((CanvasHeight - offsetY * 2) * point.Y / (InterValValueY * NumberOfDevisionsY)) - offsetY;
            reversedPoint.Y = (double)(-offsetY - point.Y + CanvasHeight) * (InterValValueY * NumberOfDevisionsY) / (CanvasHeight - offsetY * 2);

            return reversedPoint;
        }

        public void ClearPoints()
        {
            coordinatePoints.Clear();
            canvasPoints.Clear();
        }

        public PointCollection GetCanvasPoints()
        {
            return canvasPoints;
        }

        public PointCollection GetCoordinatePoints()
        {
            return coordinatePoints;
        }
    }
}
