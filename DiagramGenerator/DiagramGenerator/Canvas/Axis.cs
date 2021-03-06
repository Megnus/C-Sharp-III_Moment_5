﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

/*
 * Author:          Magnus Sundström
 * Creation Date:   2015-02-01
 * File:            Axis.cs
 */

namespace DiagramGenerator
{
    /// <summary>
    /// This class handles the drawing of the axis.
    /// </summary>
    class Axis
    {
        private Canvas canvas;
        private DiagramData diagramData;

        /// <summary>
        /// The constructor of the axis class. Here all the member variables are initiated.
        /// </summary>
        /// <param name="canvas">The canvas which the plot is drawn.</param>
        /// <param name="diagramData">The diagramdata holds the data of the coordinatesystem and the plot data.</param>
        public Axis(Canvas canvas, DiagramData diagramData)
        {
            this.diagramData = diagramData;
            this.canvas = canvas;
            CreateAxis();
        }

        /// <summary>
        /// Method for creating the axis.
        /// </summary>
        private void CreateAxis()
        {
            double offsetY = 35;
            double offsetX = Enumerable.Range(0, (int)diagramData.NumberOfDevisionsY + 1)
                .Select(x => (x * diagramData.InterValValueY).ToString().Length * 10).Max();

            CreateTicks(offsetX, offsetY);
            CreateCoordinate(offsetX, 10, offsetX, diagramData.CanvasHeight - offsetY);
            CreateCoordinate(offsetX, diagramData.CanvasHeight - offsetY, diagramData.CanvasWidth - 10, diagramData.CanvasHeight - offsetY);
        }

        /// <summary>
        /// Method for creating the ticks on the axis.
        /// </summary>
        /// <param name="offsetX">The offset on the x-axis.</param>
        /// <param name="offsetY">The offset on the y-axis.</param>
        private void CreateTicks(double offsetX, double offsetY)
        {
            for (int i = 0; i <= diagramData.NumberOfDevisionsX; i++)
            {
                double x = offsetX + (diagramData.CanvasWidth - offsetX * 2) / diagramData.NumberOfDevisionsX * i;
                CreateText(x, diagramData.CanvasHeight - offsetY, (i * diagramData.InterValValueX).ToString(), Colors.Blue, Double.NaN);
            }

            for (int i = 0; i <= diagramData.NumberOfDevisionsY; i++)
            {
                double y = offsetY + (diagramData.CanvasHeight - offsetY * 2) / diagramData.NumberOfDevisionsY * i + 10;
                CreateText(2, diagramData.CanvasHeight - y, (i * diagramData.InterValValueY).ToString(), Colors.Blue, offsetX - 5);
            }
        }

        /// <summary>
        /// Draws the coordinate and are used to draw both the x-axis and the y-axis.
        /// </summary>
        /// <param name="x1">The x-value for the first point.</param>
        /// <param name="y1">The y-value for the first point.</param>
        /// <param name="x2">The x-value for the second point.</param>
        /// <param name="y2">The y-value for the second point.</param>
        private void CreateCoordinate(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.Blue;
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            line.X1 = (int)x1;
            line.X2 = (int)x2;
            line.Y1 = (int)y1;
            line.Y2 = (int)y2;

            line.StrokeThickness = 1;
            canvas.Children.Add(line);
        }

        /// <summary>
        /// The method for creating the values as text at the ticks on the axis.
        /// </summary>
        /// <param name="x">The x-position of the text.</param>
        /// <param name="y">The y-position of the text.</param>
        /// <param name="text">The text to be drawn.</param>
        /// <param name="color">The color on the text to be drawn.</param>
        /// <param name="width">The width of the text to be drawn.</param>
        private void CreateText(double x, double y, string text, Color color, double width)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.TextAlignment = TextAlignment.Right;
            textBlock.Width = width;
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }
    }
}