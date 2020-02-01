using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{

    
    public abstract class DataClustering
    {
        protected List<Cluster> clusters = new List<Cluster>();
        //protected MainProgramClass main;
        public bool Finished { get; set; }
        private List<DataPoint> points;
        
        public List<Cluster> Clusters { get => clusters; set => clusters = value; }
        private ProcessingData processingData;
        protected List<DataPoint> Points { get => points; set => points = value; }
        protected ProcessingData ProcessingData { get => processingData; set => processingData = value; }

        public DataClustering(List<DataPoint> dataPoints, ProcessingData data)
        {
            points = dataPoints;
            ProcessingData = data;
        }
        public abstract void Clustering();

        public double Distance(DataPoint point1, DataPoint point2)
        {
            //switch (main.processingData.distanceAlgorithm)
            //{
            //    case DistanceAlgorithm.Euclidean:
            //        return EuclideanDistance(point1, point2);

            //    case DistanceAlgorithm.SquaredEuclidean:
            //        return SquaredEuclideanDistance(point1, point2);

            //    case DistanceAlgorithm.Manhattan:
            //        return ManhattanDistance(point1, point2);

            //    default:
            //        return MaximumDistance(point1, point2);

            //}
            return EuclideanDistance(point1, point2);
        }


        public double SquaredEuclideanDistance(DataPoint point1, DataPoint point2)
        {
            double result = 0;
            for (int i = 0; i < point1.Coordinates.Count; i++)
            {
                result += Math.Pow(point2.Coordinates[i] - point1.Coordinates[i], 2);
            }
            return result;
        }

        public double EuclideanDistance(DataPoint point1, DataPoint point2) => Math.Sqrt(SquaredEuclideanDistance(point1, point2));

        public double ManhattanDistance(DataPoint point1, DataPoint point2)
        {
            double result = 0;
            for (int i = 0; i < point1.Coordinates.Count; i++)
            {
                result += Math.Abs(point2.Coordinates[i] - point1.Coordinates[i]);
            }
            return result;
        }

        public double MaximumDistance(DataPoint point1, DataPoint point2)
        {
            double result = 0;
            for (int i = 0; i < point1.Coordinates.Count; i++)
            {
                if(Math.Abs(point2.Coordinates[i] - point1.Coordinates[i])>result)
                    result = Math.Abs(point2.Coordinates[i] - point1.Coordinates[i]);
            }
            return result;
        }   

        public DataPoint Mean(Cluster cluster)
        {
            List<double> tmp = new List<double>();
            for (int i = 0; i < ProcessingData.dimensionQuantity; i++)
            {
                tmp.Add(new double());
                tmp[i] = 0;
                
            }
            foreach(DataPoint point in cluster.Points)
            {
                for(int i =0;i<point.Coordinates.Count;i++)
                {
                    tmp[i] += point.Coordinates[i];
                }
            }
            for (int i = 0; i < tmp.Count; i++)
            {
                tmp[i] /= cluster.Points.Count;
            }
            return new DataPoint(tmp);        
        }
    }
}
