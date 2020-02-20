using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clustering;
namespace ConsoleTestApp
{

    class Program
    {
        static void Main(string[] args)
        {
            ProcessingData processingData = new ProcessingData();
            List<DataPoint> dataPoints = new List<DataPoint>();
            //...
            AverageLinkageHierarchical aLH = new AverageLinkageHierarchical(dataPoints, processingData);
            while (aLH.Finished != true)
                aLH.ClusteringStep();

            JarvisPatrick jP = new JarvisPatrick(dataPoints, processingData);
                jP.ClusteringStep();
          
        }
    }
}
