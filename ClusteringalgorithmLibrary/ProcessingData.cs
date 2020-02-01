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
        //for every algorithm
        public int PointsQuantity=0;
        public int dimensionQuantity = 0;

        //K-Means
        public int GroupsQuantity=0;
        
        //Jarvis-Patrick
        public int NeighborsToExamine = 0;
        public int RequiredNeighbors = 0;

        //for Desktop App
        public bool Visualization = false;
        public bool[] AlgorithmChecklist = new bool[] { false, false, false, false, false };
        public bool readFromFile = false;
        public bool readFromBitmap = false;
        public string InputFilePath = "";
        public string OutputFilePath = "result.txt";

        public DistanceAlgorithm distanceAlgorithm = DistanceAlgorithm.Euclidean;
    }

   
}
