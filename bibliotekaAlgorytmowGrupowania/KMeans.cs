using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjektInżynierski
{
    public class KMeans : DataClustering
    {
        public override void Clustering()
        {

            //while (Assign())
            //{
            //    Update();

            //}

            if (Assign())
            {
                Finished = false;
                Update();
            }
            else Finished = true;
            
        }
        public bool Assign()
        {
            //foreach(Cluster cluster in clusters)
            //{
            //    cluster.Points.Clear();
            //}
            List<Cluster> clustersTmp = new List<Cluster>();
            foreach(Cluster cluster in clusters)
            {
                clustersTmp.Add(new Cluster(cluster.Centroid));
                clustersTmp[clustersTmp.Count - 1].ColorOnChart = cluster.ColorOnChart;
            }
            int closestCluster;
            bool result = false;
            foreach (DataPoint point in main.Points)
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

        public KMeans(MainProgramClass Main)
        {
            main = Main;
            DataPoint tmp = new DataPoint();
            Thread.Sleep(20);
            Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            for(int i =0;i<main.processingData.GroupsQuantity;i++)
            {
                for(int j = 0; j<main.processingData.dimensionQuantity;j++)
                {
                    tmp.Coordinates.Add((rand.Next(0, 60001)) / (double)100);
                }
                Clusters.Add(new Cluster(new DataPoint(tmp)));
                tmp.Coordinates.Clear();
            }
        }
    }
}
