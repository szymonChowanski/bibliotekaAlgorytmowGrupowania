
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInżynierski
{
    public abstract class Hierarchical : DataClustering 
    {
        
        public Hierarchical(MainProgramClass main)
        {
            foreach(DataPoint point in main.Points)
            {
                clusters.Add(new Cluster());
                clusters[clusters.Count - 1].Points.Add(point);
            }
        }

        public override void Clustering()
        {
            double clusterDistance= double.MaxValue;
            int closesti=0, closestj=1;
            //foreach(Cluster cluster in clusters)
            //{
            //    foreach(Cluster cluster2 in clusters)
            //    {
            //        if (cluster.Equals(cluster2))
            //            continue;
            //        else if(Linkage(cluster,cluster2)<clusterDistance)
            //        {
            //            clusterDistance = Linkage(cluster, cluster2);
            //           // closest = 
                       
            //        }
            //    }
            //}

            for(int i = 0; i<clusters.Count-1;i++)
            {
                for (int j = i + 1; j < clusters.Count; j++)
                {
                    if (Linkage(clusters[i], clusters[j]) < clusterDistance)
                    {
                        clusterDistance = Linkage(clusters[i], clusters[j]);
                        closesti = i;
                        closestj = j;
                    }
                }
            }

            foreach(DataPoint point in clusters[closestj].Points)
            {
                clusters[closesti].Points.Add(point);
            }
            clusters.RemoveAt(closestj);

            if (clusters.Count == 1)
                Finished = true;

        }

        public abstract double Linkage(Cluster cluster1, Cluster cluster2);


    }
}
