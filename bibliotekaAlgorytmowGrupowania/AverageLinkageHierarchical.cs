﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ProjektInżynierski
{
    class AverageLinkageHierarchical : Hierarchical
    {

        public AverageLinkageHierarchical(MainProgramClass main) : base(main)
        {

        }
        //public override void Clustering()
        //{

        //}
        public override double Linkage(Cluster cluster1, Cluster cluster2)
        {
            double clusterDistance = 0;
            int counter = 0;
            foreach (DataPoint point1 in cluster1.Points)
            {
                foreach (DataPoint point2 in cluster2.Points)
                {
                    
                    clusterDistance += Distance(point1, point2);
                    counter++;
                }
            }
            return clusterDistance/counter;
        }

    }
}