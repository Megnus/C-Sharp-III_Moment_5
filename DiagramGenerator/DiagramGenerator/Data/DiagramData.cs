using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

/*
 * Author:          Magnus Sundström
 * Creation Date:   2015-02-01
 * File:            DiagramData.cs
 */

namespace DiagramGenerator
{
    /// <summary>
    /// This class holds the data of the coordinatesystem and the plot data.
    /// </summary>
    public class DiagramData
    {
        public PointCollection coordinatePoints;
        public PointCollection canvasPoints;
        private Point lastCanvasPoint;
        private const int offsetX = 35;
        private const int offsetY = 35;

        /// <summary>
        /// The constructor of the class. This is where the pointcollection is created 
        /// and the initiate values are set.
        /// </summary>
        public DiagramData()
        {
            this.coordinatePoints = new PointCollection();
            this.canvasPoints = new PointCollection();

            NumberOfDevisionsX = 10;
            NumberOfDevisionsY = 10;
            InterValValueX = 0.1;
            InterValValueY = 0.1;
        }

        /// <summary>
        /// The property of the width of the canvas.
        /// </summary>
        public double CanvasWidth { get; set; }
        
        /// <summary>
        /// The property of the height of the canvas.
        /// </summary>
        public double CanvasHeight { get; set; }
        
        /// <summary>
        /// The property of the number of devisions for the x-axis.
        /// </summary>
        public double NumberOfDevisionsX { get; set; }
        
        /// <summary>
        /// The property of the number of devisions for the y-axis.
        /// </summary>
        public double NumberOfDevisionsY { get; set; }
        
        /// <summary>
        /// The property of the interval value for the x-axis.
        /// </summary>
        public double InterValValueX { get; set; }
        
        /// <summary>
        /// The property of the interval value for the y-axis.
        /// </summary>
        public double InterValValueY { get; set; }

        /// <summary>
        /// The getter for the number of ploted points.
        /// </summary>
        public int NumberOfPoints
        {
            get { return coordinatePoints.Count; }
        }

        /// <summary>
        /// The getter for the last point in the plot series.
        /// </summary>
        public Point LastCanvasPoint
        {
            get { return lastCanvasPoint; }
        }

        /// <summary>
        /// Method for adding a new point to the pointcollection.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        public void AddNewPoint(Point point)
        {
            lastCanvasPoint = Transpose(point);
            coordinatePoints.Add(point);
            canvasPoints.Add(lastCanvasPoint);
        }

        /// <summary>
        /// Returns a point for a specific index in the pointcollection.
        /// </summary>
        /// <param name="index">The index which represent the element in the pointcollection.</param>
        /// <returns>The point in the specific index.</returns>
        public Point GetCanvasPoint(int index)
        {
            return canvasPoints[index];
        }

        /// <summary>
        /// Sorts the points in respect of the x-axis.
        /// </summary>
        public void SortXPoints()
        {
            coordinatePoints = new PointCollection(coordinatePoints.OrderBy(p => p.X).ThenBy(p => p.Y).ToList());
            canvasPoints = new PointCollection(canvasPoints.OrderBy(p => p.X).ThenByDescending(p => p.Y).ToList());
        }

        /// <summary>
        /// Sorts the points in respect of the y-axis.
        /// </summary>
        public void SortYPoints()
        {
            coordinatePoints = new PointCollection(coordinatePoints.OrderBy(p => p.Y).ThenBy(p => p.X).ToList());
            canvasPoints = new PointCollection(canvasPoints.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList());
        }

        /// <summary>
        /// Transposes a point from the coordinate system to the canvas coordinates.
        /// </summary>
        /// <param name="point">The point to transpose.</param>
        /// <returns>The transposed point.</returns>
        private Point Transpose(Point point)
        {
            Point transposedPoint = new Point();
            transposedPoint.X = (int)((CanvasWidth - offsetX * 2) * point.X / (InterValValueX * NumberOfDevisionsX)) + offsetX;
            transposedPoint.Y = (int)CanvasHeight - (int)((CanvasHeight - offsetY * 2) * point.Y / (InterValValueY * NumberOfDevisionsY)) - offsetY;
            return transposedPoint;
        }

        /// <summary>
        /// Reverse a point from the canvas to match the coordinate system.
        /// </summary>
        /// <param name="point">The point to reverse.</param>
        /// <returns>The reversed point.</returns>
        public Point Reverse(Point point)
        {
            Point reversedPoint = new Point();
            reversedPoint.X = (double)(point.X - offsetX) * (InterValValueX * NumberOfDevisionsX) / ((CanvasWidth - offsetX * 2));
            reversedPoint.Y = (double)(-offsetY - point.Y + CanvasHeight) * (InterValValueY * NumberOfDevisionsY) / (CanvasHeight - offsetY * 2);
            return reversedPoint;
        }

        /// <summary>
        /// Clears the plot from all the points in the pointcollections.
        /// </summary>
        public void ClearPoints()
        {
            coordinatePoints.Clear();
            canvasPoints.Clear();
        }

        /// <summary>
        /// Method for returning the pointcollection represented by the canvas.
        /// </summary>
        /// <returns>The pointcollection for the canvas.</returns>
        public PointCollection GetCanvasPoints()
        {
            return canvasPoints;
        }

        /// <summary>
        /// Method for returning the pointcollection represented by the coordinate system. 
        /// </summary>
        /// <returns>The pointcollection for the coordinate system.</returns>
        public PointCollection GetCoordinatePoints()
        {
            return coordinatePoints;
        }
    }
}