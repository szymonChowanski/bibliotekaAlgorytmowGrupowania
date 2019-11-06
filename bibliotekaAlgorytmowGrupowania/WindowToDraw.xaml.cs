using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace ProjektInżynierski
{
    /// <summary>
    /// Interaction logic for WindowToDraw.xaml
    /// </summary>
    public partial class WindowToDraw : Window
    {
        DrawingGroup drawingGroup = new DrawingGroup();
        GeometryDrawing coordinatesDrawing = new GeometryDrawing();
        //List<Cluster> clusters;
        DataClustering dataClustering;
        int iteration = 0;

        public WindowToDraw(DataClustering clustering)
        {
            InitializeComponent();
            dataClustering = clustering;
            SetTitle();
            drawStart();
            drawPoints();
            drawFinish();

        }

        Point toChartPoint(double oldX, double oldY)
        {
            double newX = oldX + 20;
            double newY = 600 - oldY;
            //double newY = oldY + 20;
            Point result = new Point(newX, newY);
            return result;
        }
        Point toChartPoint(DataPoint oldPoint)
        {
            //Point newPoint = new Point(oldPoint.Coordinates[0] + 20, 600 - oldPoint.Coordinates[1]);
            Point newPoint = new Point(oldPoint.Coordinates[0] + 20, oldPoint.Coordinates[1]);
            return newPoint;
        }
        void drawStart()
        {
            MyColors.Counter = 0;
            
            GeometryGroup coordinates = new GeometryGroup();
            coordinates.Children.Add(new LineGeometry(new Point(0, 600), new Point(620, 600)));
            coordinates.Children.Add(new LineGeometry(new Point(20, 0), new Point(20, 620)));
            coordinates.Children.Add(new LineGeometry(new Point(620, 600), new Point(615, 595)));
            coordinates.Children.Add(new LineGeometry(new Point(620, 600), new Point(615, 605)));
            coordinates.Children.Add(new LineGeometry(new Point(20, 0), new Point(25, 5)));
            coordinates.Children.Add(new LineGeometry(new Point(20, 0), new Point(15, 5)));
            Point from, to;
            for (int i = 0; i < 600; i += 25)
            {
                from = toChartPoint(-5, i);
                to = toChartPoint(0, i);
                coordinates.Children.Add(new LineGeometry(from, to));
                from = toChartPoint(i, 0);
                to = toChartPoint(i, -5);
                coordinates.Children.Add(new LineGeometry(from, to));

            }
            coordinatesDrawing.Geometry = coordinates;
            coordinatesDrawing.Brush = new SolidColorBrush(Colors.Black);
            coordinatesDrawing.Pen = new Pen(Brushes.Black, 1);
            coordinatesDrawing.Freeze();
            drawingGroup.Children.Add(coordinatesDrawing);


        }

        void drawPoints()
        {
            List<GeometryDrawing> pointsDrawingList = new List<GeometryDrawing>();
            List<GeometryGroup> pointsGeometryList = new List<GeometryGroup>();
            foreach (Cluster cluster in dataClustering.Clusters)
            {
                if(cluster.ColorOnChart==Colors.White)
                    cluster.SetColor();
                foreach (DataPoint point in cluster.Points)
                {
                    point.ChartPoint = toChartPoint(point);
                }
            }

            int listcounter = 0;
            foreach (Cluster cluster in dataClustering.Clusters)
            {
                pointsDrawingList.Add(new GeometryDrawing());
                pointsGeometryList.Add(new GeometryGroup());
                foreach (DataPoint point in cluster.Points)
                {
                    pointsGeometryList[listcounter].Children.Add(new EllipseGeometry(point.ChartPoint, 3, 3));
                }
                pointsDrawingList[listcounter].Geometry = pointsGeometryList[listcounter];
                pointsDrawingList[listcounter].Brush = new SolidColorBrush(cluster.ColorOnChart);
                pointsDrawingList[listcounter].Pen = new Pen(Brushes.Black, 1);
                pointsDrawingList[listcounter].Freeze();
                drawingGroup.Children.Add(pointsDrawingList[listcounter]);
                listcounter++;
            }

            //centroidy
            DataClustering tmp = dataClustering as KMeans;
            if (tmp!=null)
            {
                pointsDrawingList.Add(new GeometryDrawing());
                pointsGeometryList.Add(new GeometryGroup());
                foreach (Cluster cluster in dataClustering.Clusters)
                {
                    cluster.Centroid.ChartPoint = toChartPoint(cluster.Centroid);
                    pointsGeometryList[listcounter].Children.Add(new LineGeometry(new Point(cluster.Centroid.ChartPoint.X - 5, cluster.Centroid.ChartPoint.Y - 5), new Point(cluster.Centroid.ChartPoint.X + 5, cluster.Centroid.ChartPoint.Y + 5)));
                    pointsGeometryList[listcounter].Children.Add(new LineGeometry(new Point(cluster.Centroid.ChartPoint.X - 5, cluster.Centroid.ChartPoint.Y + 5), new Point(cluster.Centroid.ChartPoint.X + 5, cluster.Centroid.ChartPoint.Y - 5)));

                }
                pointsDrawingList[listcounter].Geometry = pointsGeometryList[listcounter];
                pointsDrawingList[listcounter].Brush = new SolidColorBrush(Colors.Black);
                pointsDrawingList[listcounter].Pen = new Pen(Brushes.Black, 1);

                drawingGroup.Children.Add(pointsDrawingList[listcounter]);


            }
        }

        void drawFinish()
        {
           
            DrawingImage drawingImage = new DrawingImage(drawingGroup);
            Image image = new Image();
            image.Source = drawingImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            //this.Content = image;
            if(dataClustering.Finished)
                NextStepButton.Visibility = Visibility.Collapsed;
            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.
            ImageStackPanel.Children.Clear();
            ImageStackPanel.Children.Add(image);
            //ChartImage = image;
            //ChartImage.UpdateLayout();  
        }
        

        public void drawAndDisplay()
        {
            drawingGroup.Children.Clear();
            drawingGroup.Children.Add(coordinatesDrawing);
            drawPoints();
            drawFinish();
        }

        public void SetTitle()
        {
            DataClustering[] algorithms = { dataClustering as KMeans,
                                            dataClustering as JarvisPatrick,
                                            dataClustering as SingleLinkageHierarchical,
                                            dataClustering as AverageLinkageHierarchical,
                                            dataClustering as CompleteLinkageHierarchical};
            Title = "Wizualizacja algorytmu ";
            if (algorithms[0]!=null) Title += "k-średnich";
            else if (algorithms[1] != null) Title += "Jarvisa-Patricka";
            else if (algorithms[2] != null) Title += "hierarchiczny aglomeracyjny z miarą odległości single-link";
            else if (algorithms[3] != null) Title += "hierarchiczny aglomeracyjny z miarą odległości average-link";
            else  Title += "hierarchiczny aglomeracyjny z miarą odległości complete-link";
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            
            if(!dataClustering.Finished)
            {
                dataClustering.Clustering();
                drawAndDisplay();
            }
            if (dataClustering.Finished)
                NextStepButton.Visibility = Visibility.Collapsed;
            
        }
    }
}