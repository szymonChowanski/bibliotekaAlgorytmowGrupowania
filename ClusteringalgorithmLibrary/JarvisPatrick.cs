using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    public class JarvisPatrick : DataClustering
    {
        public JarvisPatrick(List<DataPoint> dataPoints, ProcessingData data): base(dataPoints,data)
        {
            

            foreach (DataPoint point in Points)
            {
                point.NearestNeighbors = new List<DataPoint>();
                foreach (DataPoint neighborPoint in Points)
                {
                    if (point.Index != neighborPoint.Index)
                    {
                        if ((point.NearestNeighbors.Count < ProcessingData.NeighborsToExamine))
                        {

                            point.NearestNeighbors.Add(neighborPoint);
                            SortNeighbors(point);
                        }
                        else
                        {
                            if ((Distance(neighborPoint, point)) < (Distance(point, point.NearestNeighbors[point.NearestNeighbors.Count - 1])))
                            {
                                point.NearestNeighbors[point.NearestNeighbors.Count - 1] = neighborPoint;
                                SortNeighbors(point);
                            }
                        }
                    }
                }
            }
        }
        public override void Clustering()
        {
           foreach(DataPoint point in Points)
           {
                point.ClusterIndex = point.Index;
           }
            foreach (DataPoint point in Points)
            {
                foreach(DataPoint neighbor in Points)
                {
                    if (point.ClusterIndex != neighbor.ClusterIndex)
                        if(ExamineNeighbourLists(point,neighbor))
                        {
                            int newClusterIndex;
                            int oldClusterIndex;
                            if (point.ClusterIndex < neighbor.ClusterIndex)
                            {
                                newClusterIndex = point.ClusterIndex;
                                oldClusterIndex = neighbor.ClusterIndex;
                            }
                            else
                            {
                                newClusterIndex = neighbor.ClusterIndex;
                                oldClusterIndex = neighbor.ClusterIndex;
                            }
                            foreach(DataPoint dataPoint in Points)
                            {
                                if (dataPoint.ClusterIndex == oldClusterIndex)
                                    dataPoint.ClusterIndex = newClusterIndex;
                            }
                        }
                }
                
            }
            Points.Sort(CompareByClusterIndex);
            int currentLabel = Points[0].ClusterIndex;
            int currentCluster=0;
            clusters.Add(new Cluster());
            foreach(DataPoint point in Points)
            {
                if (point.ClusterIndex != currentLabel)
                {
                    currentLabel = point.ClusterIndex;
                    clusters.Add(new Cluster());
                    currentCluster++;
                    
                }
                clusters[currentCluster].Points.Add(point);
            }
            Finished = true;

        }

        private void SortNeighbors(DataPoint point)
        {
            double dist;
            int index;
            bool swap = false;

            for (int i = 0; i < point.NearestNeighbors.Count; i++)
            {
                dist = Distance(point, point.NearestNeighbors[i]);
                index = i;
                for (int j = i + 1; j < point.NearestNeighbors.Count; j++)
                {
                    if (Distance(point, point.NearestNeighbors[j]) < dist)
                    {
                        dist = Distance(point, point.NearestNeighbors[j]);
                        index = j;
                        swap = true;
                    }
                }

                if (swap)
                {
                    DataPoint tmp = point.NearestNeighbors[i];
                    point.NearestNeighbors[i] = point.NearestNeighbors[index];
                    point.NearestNeighbors[index] = tmp;
                    swap = false;
                }

            }

        }

        bool ExamineNeighbourLists(DataPoint pointA, DataPoint pointB)
        {
            bool AinB=false, BinA=false;
            int commonNeighbors=0 ;
            foreach(DataPoint neighborOfA in pointA.NearestNeighbors)
            {
                if (neighborOfA.Index == pointB.Index)
                    AinB = true;
                foreach(DataPoint neighborOfB in pointB.NearestNeighbors)
                {
                    if (neighborOfB.Index == pointA.Index)
                        BinA = true;
                    if (neighborOfA.Index == neighborOfB.Index)
                        commonNeighbors++;
                }
            }

            if ((AinB) && (BinA))
                commonNeighbors++;

            if (commonNeighbors >= ProcessingData.RequiredNeighbors)
                return true;
            else return false;
        }

        private static int CompareByClusterIndex(DataPoint a, DataPoint b)
        {
            return a.ClusterIndex.CompareTo(b.ClusterIndex);
        }
    }
}

































//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProjektInżynierski
//{
//    public class JarvisPatrick : DataClustering
//    {
//        public JarvisPatrick(MainProgramClass Main)
//        {
//            main = Main;

//            foreach (DataPoint point in main.Points)
//            {
//                point.NearestNeighbors = new List<DataPoint>();
//                foreach (DataPoint neighborPoint in main.Points)
//                {
//                    if (point.Index != neighborPoint.Index)
//                    {
//                        if ((point.NearestNeighbors.Count < main.processingData.NeighborsToExamine))
//                        {

//                            point.NearestNeighbors.Add(neighborPoint);
//                            SortNeighbors(point);
//                        }
//                        else
//                        {
//                            if ((Distance(neighborPoint, point)) < (Distance(point, point.NearestNeighbors[point.NearestNeighbors.Count - 1])))
//                            {
//                                point.NearestNeighbors[point.NearestNeighbors.Count - 1] = neighborPoint;
//                                SortNeighbors(point);
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        public override void Clustering()
//        {
//            int common = 0;
//            bool bothNeighbors=false;

//            foreach (DataPoint point in main.Points)
//            {
//                if (point.ClusterIndex == -1)
//                {
//                    point.ClusterIndex = clusters.Count;
//                    clusters.Add(new Cluster());
//                    clusters[point.ClusterIndex].Points.Add(point);
//                }

//                foreach (DataPoint neighbor in point.NearestNeighbors)
//                {
//                    if (!(neighbor.ClusterIndex > -1))
//                    {
//                        foreach (DataPoint neighborOfNeighbor in neighbor.NearestNeighbors)
//                        {
//                            if (neighborOfNeighbor.Index == point.Index)
//                            {
//                                bothNeighbors = true;
//                                common++;
//                            }

//                            foreach (DataPoint neighborAgain in point.NearestNeighbors)
//                            {
//                                if (neighborOfNeighbor.Index == neighborAgain.Index)
//                                    common++;
//                            }
//                        }

//                        if (bothNeighbors && (common >= main.processingData.NeighborsInCommon))
//                        {
//                            neighbor.ClusterIndex = point.ClusterIndex;
//                            clusters[point.ClusterIndex].Points.Add(neighbor);
//                        }
//                        common = 0;
//                        bothNeighbors = false;
//                    }

//                    Finished = true;
//                }
//            }

//        }

//        private void SortNeighbors(DataPoint point)
//        {
//            double dist;
//            int index;
//            bool swap = false;

//            for(int i = 0; i < point.NearestNeighbors.Count;i++)
//            {
//                dist = Distance(point, point.NearestNeighbors[i]);
//                index = i;
//                for(int j =i+1;j< point.NearestNeighbors.Count; j++)
//                {
//                    if(Distance(point, point.NearestNeighbors[j])<dist)
//                    {
//                        dist = Distance(point, point.NearestNeighbors[j]);
//                        index = j;
//                        swap = true;
//                    }
//                }

//                if(swap)
//                {
//                    DataPoint tmp = point.NearestNeighbors[i];
//                    point.NearestNeighbors[i] = point.NearestNeighbors[index];
//                    point.NearestNeighbors[index] = tmp;
//                    swap = false;
//                }

//            }

//        }
//    }
//}
