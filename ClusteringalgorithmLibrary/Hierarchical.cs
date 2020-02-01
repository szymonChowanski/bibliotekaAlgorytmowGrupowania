
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    public abstract class Hierarchical : DataClustering 
    {
        public List<List<Cluster>> clusterHistory = new List<List<Cluster>>();


        public Hierarchical(List<DataPoint> dataPoints, ProcessingData data) : base(dataPoints, data)
        {
            clusterHistory.Add(new List<Cluster>());
            foreach (DataPoint point in Points)
            {
                clusters.Add(new Cluster());
                clusters[clusters.Count - 1].Points.Add(point);
                clusterHistory[0].Add(new Cluster(clusters[clusters.Count - 1]));
            }        
        }

        public abstract double Linkage(Cluster cluster1, Cluster cluster2);

        public override void Clustering()
        {
            double clusterDistance= double.MaxValue;
            int closesti=0, closestj=1;
           
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

            clusterHistory.Add(new List<Cluster>());
            foreach(Cluster cluster in clusters)
            {
                clusterHistory[clusterHistory.Count - 1].Add(new Cluster(cluster));
            }
        }
    }
}
