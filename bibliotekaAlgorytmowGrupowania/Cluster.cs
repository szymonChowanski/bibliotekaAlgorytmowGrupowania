using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjektInżynierski
{
    public class Cluster
    {
        private Color _colorOnChart = Colors.White;

        private List<DataPoint> _points = new List<DataPoint>();
        internal List<DataPoint> Points { get => _points; set => _points = value; }

        private DataPoint _centroid = new DataPoint();
        internal DataPoint Centroid { get => _centroid; set => _centroid = value; }

        public Color ColorOnChart { get => _colorOnChart; set => _colorOnChart = value; }
        
        

        public Cluster(){}

        public Cluster(DataPoint centroid) => _centroid = centroid;
        
        public Cluster(Cluster another)
        {
            foreach(DataPoint anotherPoint in another.Points)
            {
                Points.Add(new DataPoint(anotherPoint));
            }
        }


        public void SetColor() => ColorOnChart = (Color)ColorConverter.ConvertFromString(MyColors.GetColor());


        public bool Equals(Cluster another)
        {
            if (_points.Count != another._points.Count)
                return false;
            if (_points.Count == 0)
                return true;
            if (_points[0].Coordinates.Count != another._points[0].Coordinates.Count)
                return false;
            for(int i = 0;i<_points.Count;i++)
                if (!(_points[i].Index == another._points[i].Index))
                    return false;
            return true;
        }
    }
}
