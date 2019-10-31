﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjektInżynierski
{
    class CompleteLinkageHierarchical : Hierarchical
    {

        public CompleteLinkageHierarchical(MainProgramClass main) : base(main)
        {

        }
        //public override void Clustering()
        //{

        //}
        public override double Linkage(Cluster cluster1, Cluster cluster2)
        {
            double clusterDistance = 0;
            foreach (DataPoint point1 in cluster1.Points)
            {
                foreach (DataPoint point2 in cluster2.Points)
                {
                    if (Distance(point1, point2) > clusterDistance)
                        clusterDistance = Distance(point1, point2);
                }
            }
            return clusterDistance;
        }

    }
}