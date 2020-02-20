using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Clustering
{ 
    public class KMeans : DataClustering
    {
        public KMeans(List<DataPoint> dataPoints, ProcessingData data):base(dataPoints,data)
        {
            
            DataPoint tmp = new DataPoint();
            Thread.Sleep(20);
            Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            for (int i = 0; i < ProcessingData.GroupsQuantity; i++)
            {
                for (int j = 0; j < ProcessingData.dimensionQuantity; j++)
                {
                    tmp.Coordinates.Add((rand.Next(0, 60001)) / (double)100);
                }
                Clusters.Add(new Cluster(new DataPoint(tmp)));
                tmp.Coordinates.Clear();
            }
        }

        public override void ClusteringStep()
        {
            if (Assign())
                Update();
            else Finished = true;
        }

        public bool Assign()
        {
            List<Cluster> clustersTmp = new List<Cluster>();
            foreach(Cluster cluster in clusters)
            {
                clustersTmp.Add(new Cluster(cluster.Centroid));
                clustersTmp[clustersTmp.Count - 1].ColorOnChart = cluster.ColorOnChart;
            }
            int closestCluster;
            bool result = false;
            foreach (DataPoint point in Points)
            {
                closestCluster = 0;
                for (int i = 1; i < clusters.Count; i++)
                    if (Distance(clustersTmp[i].Centroid, point) < Distance(clustersTmp[closestCluster].Centroid, point))
                    {
                        closestCluster = i;
                    }
                clustersTmp[closestCluster].Points.Add(point);
            }
            for(int i =0;i<clusters.Count;i++)
            {
                if(!(clusters[i].Equals(clustersTmp[i])))
                {
                    result = true;
                    break;
                }
            }
            clusters = clustersTmp;
            return result;
        }

        public void Update()
        {
            foreach (Cluster cluster in Clusters)
            {
                cluster.Centroid = Mean(cluster);
            }
        }        
    }
}
