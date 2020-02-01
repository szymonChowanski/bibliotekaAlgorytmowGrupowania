using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    public enum DistanceAlgorithm { Euclidean, SquaredEuclidean, Manhattan, Maximum};

    public class ProcessingData
    {
        //
        public int PointsQuantity=0;
        public int GroupsQuantity=0;
        public bool[] AlgorithmChecklist = new bool[] {false, false, false, false, false};
        public string InputFilePath="";
        public string OutputFilePath = "result.txt";
        public bool readFromFile=false;
        public bool readFromBitmap = false;
        public int dimensionQuantity=0;
        public bool Visualization = false;
        public int NeighborsToExamine = 0;
        public int NeighborsInCommon = 0;
        public DistanceAlgorithm distanceAlgorithm = DistanceAlgorithm.Euclidean;
    }

   
}
