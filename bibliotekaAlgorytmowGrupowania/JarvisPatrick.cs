using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInżynierski
{
    public class JarvisPatrick : DataClustering
    {
        public override void Clustering()
        {
            int common = 0;
            bool bothNeighbors=false;
            foreach (DataPoint point in main.Points)
            {
                if (point.ClusterIndex == -1)
                {
                    point.ClusterIndex = clusters.Count;
                    clusters.Add(new Cluster());
                    clusters[point.ClusterIndex].Points.Add(point);
                }
                foreach (DataPoint neighbor in point.NearestNeighbors)
                {
                    if (neighbor.ClusterIndex > -1)
                    { }
                    else
                    {
                        foreach (DataPoint neighborOfNeighbor in neighbor.NearestNeighbors)
                        {
                            if (neighborOfNeighbor.Index == point.Index)
                            {
                                bothNeighbors = true;
                                common++;
                            }
                            foreach (DataPoint neighborAgain in point.NearestNeighbors)
                            {
                                if (neighborOfNeighbor.Index == neighborAgain.Index)
                                    common++;
                            }
                        }
                        if (bothNeighbors && (common >= main.processingData.NeighborsInCommon))
                        {
                            neighbor.ClusterIndex = point.ClusterIndex;
                            clusters[point.ClusterIndex].Points.Add(neighbor);
                        }
                        common = 0;
                        bothNeighbors = false;
                    }
                    Finished = true;

                }
                //    foreach(DataPoint neighbor in point.NearestNeighbors)
                //    {
                //        if(neighbor.Index==point.Index)
                //        {
                //            common++;
                //            foreach(DataPoint neighborOfNeighbor in neighbor.NearestNeighbors)
                //            {
                //                foreach (DataPoint originalNeighbor in point.NearestNeighbors)
                //                    if (originalNeighbor.Index == neighborOfNeighbor.Index)
                //                        common++;
                //            }
                //            if(common>=main.processingData.NeighborsInCommon)
                //            {
                //                if(point.IsInCluster)
                //                {
                //                    clusters[point.ClusterIndex].Points.Add(neighbor);
                //                    neighbor.IsInCluster = true;
                //                    neighbor.ClusterIndex = point.ClusterIndex;
                //                }
                //                else if(neighbor.IsInCluster)
                //                {
                //                    clusters[neighbor.ClusterIndex].Points.Add(point);
                //                    point.IsInCluster = true;
                //                    point.ClusterIndex = neighbor.ClusterIndex;
                //                }
                //                else
                //                {
                //                    clusters.Add(new Cluster());
                //                    clusters[clusters.Count - 1].Points.Add(neighbor);
                //                    clusters[clusters.Count - 1].Points.Add(point);
                //                    point.IsInCluster = true;
                //                    neighbor.IsInCluster = true;
                //                    point.ClusterIndex = clusters.Count - 1;
                //                    neighbor.ClusterIndex = clusters.Count - 1;
                //                }
                //            }
                //        }
                //    }
                //    if(!point.IsInCluster)
                //    {
                //        clusters.Add(new Cluster());
                //        clusters[clusters.Count - 1].Points.Add(point);
                //        point.IsInCluster = true;
                //        point.ClusterIndex = clusters.Count - 1;
                //    }
                //    common = 0;
                //}

            }
        
        }
        public JarvisPatrick(MainProgramClass Main)
        {
            main = Main;
            
            foreach (DataPoint point in main.Points)
            {
                point.NearestNeighbors = new List<DataPoint>();
                foreach(DataPoint neighborPoint in main.Points)
                {
                    if (point.Index != neighborPoint.Index)
                    {
                        if ((point.NearestNeighbors.Count < main.processingData.NeighborsToExamine))
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

        

        private void SortNeighbors(DataPoint point)
        {
            double dist;
            int index;
            bool swap = false;
            for(int i = 0; i < point.NearestNeighbors.Count;i++)
            {
                dist = Distance(point, point.NearestNeighbors[i]);
                index = i;
                for(int j =i+1;j< point.NearestNeighbors.Count; j++)
                {
                    if(Distance(point, point.NearestNeighbors[j])<dist)
                    {
                        dist = Distance(point, point.NearestNeighbors[j]);
                        index = j;
                        swap = true;
                    }
                }
                if(swap)
                {
                    DataPoint tmp = point.NearestNeighbors[i];
                    point.NearestNeighbors[i] = point.NearestNeighbors[index];
                    point.NearestNeighbors[index] = tmp;
                    swap = false;
                }

            }

        }

     

    }
}
