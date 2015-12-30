﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
// Drawing Polylines and Plygons.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // stores the collection of points for the mutisided shapes
        private PointCollection points = new PointCollection();
        private PointCollection pp = new PointCollection();
        private List<Point> pointList = new List<Point>();
        private Point size, origo;

        // initialize the points of the shapes
        public MainWindow()
        {
            InitializeComponent();

            polyline.Points = points; // assing Plyline points
            polygon.Points = points; // assing Plygon points
            filledPolygon.Points = points; // assing filled Plygon points

            lbxPoints.ItemsSource = points;

            size = new Point(drawCanvas.Width, drawCanvas.Height);
            origo = new Point((int)(size.X / 2), (int)(size.Y / 2));

            DrawCoordinates(origo.X, 0, origo.X, size.Y);
            DrawCoordinates(0, origo.Y, size.X, origo.Y);
            Debug.WriteLine(size.X);
            Debug.WriteLine(size.Y);
        }

        // adds a new point when the user clicks on the canvas
        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //add point to collection
            points.Add(e.GetPosition(drawCanvas));
        }

        // when the clear Button is clicked
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            points.Clear(); // clear the poitns from the collection
        }

        // when the user selects the Polyline
        private void lineRadio_Checked(object sender, RoutedEventArgs e)
        {
            // Polyline is visible, the other two are not
            polyline.Visibility = Visibility.Visible;
            polygon.Visibility = Visibility.Collapsed;
            filledPolygon.Visibility = Visibility.Collapsed;
        }

        // when the user selects the Polygon
        private void polygonRadio_Checked(object sender, RoutedEventArgs e)
        {
            // Polygon is visible, the other two are not
            polyline.Visibility = Visibility.Collapsed;
            polygon.Visibility = Visibility.Visible;
            filledPolygon.Visibility = Visibility.Collapsed;
        }

        // when the user selects the filled Polygon
        private void filledPolygonRadio_Checked(object sender, RoutedEventArgs e)
        {
            // filled Polygon is visible, the other two are not
            polygon.Visibility = Visibility.Collapsed;
            polygon.Visibility = Visibility.Collapsed;
            filledPolygon.Visibility = Visibility.Visible;
        }

        private void DrawCoordinates(double x1, double y1, double x2, double y2)
        {
            line.Visibility = Visibility.Visible;
            Line myLine = new Line();

            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            myLine.X1 = (int)x1;
            myLine.X2 = (int)x2;
            myLine.Y1 = (int)y1;
            myLine.Y2 = (int)y2;

            myLine.StrokeThickness = 1;
            drawCanvas.Children.Add(myLine);
        }

        private void DrawText()
        {
            int noOfDevisionsX = 12;
            int noOfDevisionsY = 10;
            int intervalValX = 1;
            int intervalValY = 100;

            double offsetX = 20;
            for (int i = 0; i <= noOfDevisionsX; i++)
            {
                double x = offsetX + (size.X - offsetX * 2) / noOfDevisionsX * i;
                Debug.WriteLine(x);
                Text(x, size.Y - offsetX, (i * intervalValX).ToString(), Colors.Black);
            }

            double offsetY = 20;
            for (int i = 0; i <= noOfDevisionsY; i++)
            {
                double y = offsetY + (size.Y - offsetY * 2) / noOfDevisionsY * i;
                Debug.WriteLine(y);
                Text(offsetY, size.Y - y, (i * intervalValY).ToString(), Colors.Black);
            }

            DrawCoordinates(offsetX, offsetY, offsetX, size.Y - offsetY);
            DrawCoordinates(offsetX, size.Y - offsetY, size.X - offsetX, size.Y - offsetY);
        }

        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(xCoord.Text) + (int)origo.X;
            int y = -int.Parse(yCoord.Text) + (int)origo.Y;

            Ellipse ellipse = new Ellipse();
            ellipse.Width = ellipse.Height = 5.0;
            ellipse.Stroke = Brushes.Black;
            ellipse.SetValue(Canvas.LeftProperty, x - 3.0);
            ellipse.SetValue(Canvas.TopProperty, y - 3.0);

            polyline.Visibility = Visibility.Visible;
            polyline.SnapsToDevicePixels = true;
            polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            points.Add(new Point(x, y));
            lbxPoints.Items.Refresh();

            drawCanvas.Children.Add(ellipse);
            DrawCoordinates(origo.X, 0, origo.X, size.Y);
            DrawCoordinates(0, origo.Y, size.X, origo.Y);
            DrawText();
        }

        private void Text(double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            drawCanvas.Children.Add(textBlock);
        }
    }
}