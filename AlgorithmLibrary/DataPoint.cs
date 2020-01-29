using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClusteringLibrary
{
    public class DataPoint
    {
        private List<double> _coordinates = new List<double>();
        public List<double> Coordinates { get => _coordinates; set => _coordinates = value; }
        private int _index;
        public int Index { get => _index; set => _index = value; }
        public Point ChartPoint = new Point();
        private List<DataPoint> _nearestNeighbors;
        public List<DataPoint> NearestNeighbors { get => _nearestNeighbors; set => _nearestNeighbors = value; }
        private int _clusterIndex=-1;
        public int ClusterIndex { get => _clusterIndex; set => _clusterIndex = value; }

        
        public DataPoint() {}

        public DataPoint(List<double> numbers) => Coordinates = numbers;
 
        public DataPoint(DataPoint previous)
        {
            foreach (double number in previous._coordinates)
                this._coordinates.Add(number);
            Index = previous.Index;
        }

        public DataPoint (int x,int y)
        {
            _coordinates.Add(x);
            _coordinates.Add(y);
        }

        public bool Equals(DataPoint another)
        {
            if (_coordinates.Count != another._coordinates.Count)
                return false;

            if (_coordinates.Count == 0)
                return true;

            for (int i = 0; i < _coordinates.Count; i++)
                if (_coordinates[i] != another._coordinates[i])
                    return false;

            return true;
        }
    }
}
