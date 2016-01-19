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
            coordinatePoints.Add(point);
            canvasPoints.Add(Transpose(point));
        }

        public void SortPoints()
        {
            coordinatePoints = new PointCollection(coordinatePoints.OrderByDescending(p => p.X).ThenBy(p => p.Y).ToList());
            canvasPoints = new PointCollection(canvasPoints.OrderByDescending(p => p.X).ThenBy(p => p.Y).ToList());
        }

        private Point Transpose(Point point)
        {
            Point newPoint = new Point();
            newPoint.X = (int)((CanvasWidth - offsetX * 2) * point.X / (InterValValueX * NumberOfDevisionsX)) + offsetX;
            newPoint.Y = (int)CanvasHeight - (int)((CanvasHeight - offsetY * 2) * point.Y / (InterValValueY * NumberOfDevisionsY)) - offsetY;
            return newPoint;
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
