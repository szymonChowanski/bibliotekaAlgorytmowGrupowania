using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Clustering
{
    public class WriteToFile
    {
        public static string Path { get; set; } = "result.txt";


        public static void Header(List<DataPoint> points)
        {
            using (StreamWriter sw = File.CreateText(Path))
            {
                sw.WriteLine("Plik wynikowy programu Biblioteka Algorytmów Grupowania \r");
                sw.WriteLine();
                sw.WriteLine("Punkty: ");

                foreach (DataPoint point in points)
                {
                    sw.Write("[{0}]:", point.Index);
                    foreach (double number in point.Coordinates)
                    {
                        sw.Write("{0} ", number);
                    }
                    sw.WriteLine();
                }

                sw.WriteLine();
                sw.WriteLine();
                sw.Flush();
            }
        }

        public static void Algorithm(DataClustering clustering)
        {
            DataClustering[] algorithms = { clustering as KMeans,
                                            clustering as JarvisPatrick,
                                            clustering as SingleLinkageHierarchical,
                                            clustering as AverageLinkageHierarchical,
                                            clustering as CompleteLinkageHierarchical,
                                            clustering as Hierarchical};
            using (StreamWriter sw = File.AppendText(Path))
            {
                if (algorithms[0] != null) sw.WriteLine("Algorytm k-średnich");
                else if (algorithms[1] != null) sw.WriteLine("Algorytm Jarvisa-Patricka");
                else if (algorithms[1] != null) sw.WriteLine("Algorytm hierarchiczny aglomeracyjny z miarą odległości single-linkage");
                else if (algorithms[1] != null) sw.WriteLine("Algorytm hierarchiczny aglomeracyjny z miarą odległości average-linkage");
                else sw.WriteLine("Algorytm hierarchiczny aglomeracyjny z miarą odległości complete-linkage");
                int index = 0;

                if (algorithms[5] == null)
                {
                    foreach (Cluster cluster in clustering.Clusters)
                    {
                        sw.WriteLine("Cluster {0}:", index++);
                        if (algorithms[0] != null)
                        {
                            sw.Write("Centroid: ");
                            foreach (double number in cluster.Centroid.Coordinates)
                            {
                                sw.Write("{0} ", number);
                            }
                            sw.WriteLine();
                        }
                        sw.Write("Points:");
                        foreach (DataPoint point in cluster.Points)
                            sw.Write("[{0}] ", point.Index);
                        sw.WriteLine();
                    }
                }
                else
                {
                    int iterCounter = 0;
                    
                    Hierarchical hierarchical =(Hierarchical) clustering;
                    foreach(List<Cluster> list in hierarchical.clusterHistory)
                    {
                        sw.WriteLine("Iteracja: {0}", iterCounter);
                        
                        foreach(Cluster cluster in list)
                        {
                            sw.WriteLine("Cluster {0}:", index++);
                            sw.Write("Points:");
                            foreach (DataPoint point in cluster.Points)
                                sw.Write("[{0}] ", point.Index);
                            sw.WriteLine();
                        }
                        sw.WriteLine();
                        index = 0;
                        iterCounter++;
                    }
                }

                sw.WriteLine();
                sw.WriteLine();
            }
        }

        
    }
}

