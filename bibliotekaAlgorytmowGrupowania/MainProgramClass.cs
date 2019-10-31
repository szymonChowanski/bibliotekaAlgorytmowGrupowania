﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;

namespace ProjektInżynierski
{
    public class MainProgramClass
    {
        //private ProcessingData processing;
        private List<DataPoint> points;

        public ProcessingData processingData;//{ get => processingData; set => processingData = value; }
        internal List<DataPoint> Points { get => points; set => points = value; }

        public MainProgramClass()
        {
            processingData = new ProcessingData();
            points = new List<DataPoint>();
        }

        public MainProgramClass(ProcessingData Data)
        {
            processingData = Data;
            points = new List<DataPoint>();
        }

        public void Setup()
        {
            //Wczytanie lub wygenerowanie danych
            if (processingData.readFromFile)
                ReadFromFile();
            else if (processingData.readFromBitmap)
                ReadFromBitmap();
            else GenerateRandomData();


            WriteToFile();
            //wywołanie algorytmów grupowania
            //if (processingData.AlgorithmChecklist[0])
            //{
            //    DataClustering kMeans = new KMeans(this);
            //    kMeans.Clustering();
            //    if ((processingData.Visualization == true) && (processingData.dimensionQuantity == 2))
            //    {
            //        WindowToDraw kMeansVisualisation = new WindowToDraw(kMeans,"kmeans");
            //        kMeansVisualisation.Show();

            //    }
            //    WriteToFile(kMeans, "kmeans");
            //}

            //if (processingData.AlgorithmChecklist[1])
            //{
            //    DataClustering hierarchical = new SingleLinkageHierarchical(this);
            //    if ((processingData.Visualization == true) && (processingData.dimensionQuantity == 2))
            //    {
            //        WindowToDraw hierarchicalVisualisation = new WindowToDraw(hierarchical, "hierarchical");
            //        hierarchicalVisualisation.Show();

            //    }
            //}
            ////hierarchical
            //if (processingData.AlgorithmChecklist[2])
            //{
            //    DataClustering jarvisPatrick = new JarvisPatrick(this);
            //    jarvisPatrick.Clustering();
            //    if ((processingData.Visualization == true) && (processingData.dimensionQuantity == 2))
            //    {
            //        WindowToDraw jarvisPatrickVisualisation = new WindowToDraw(jarvisPatrick,"jarvis-patrick");
            //        jarvisPatrickVisualisation.Show();

            //    }
            //    WriteToFile(jarvisPatrick, "jarvis-patrick");
            //}
            //if (processingData.AlgorithmChecklist[1])
            //{
            //    DataClustering hierarchical = new SingleLinkageHierarchical(this);
            //    if ((processingData.Visualization == true) && (processingData.dimensionQuantity == 2))
            //    {
            //        WindowToDraw hierarchicalVisualisation = new WindowToDraw(hierarchical, "hierarchical");
            //        hierarchicalVisualisation.Show();

            //    }
            //}
            ////hierarchical
            //if (processingData.AlgorithmChecklist[2])
            //{
            //    DataClustering jarvisPatrick = new JarvisPatrick(this);
            //    jarvisPatrick.Clustering();
            //    if ((processingData.Visualization == true) && (processingData.dimensionQuantity == 2))
            //    {
            //        WindowToDraw jarvisPatrickVisualisation = new WindowToDraw(jarvisPatrick,"jarvis-patrick");
            //        jarvisPatrickVisualisation.Show();

            //    }
            //    WriteToFile(jarvisPatrick, "jarvis-patrick");
            //}

           
            List<DataClustering> algorithmList = new List<DataClustering>();

            for(int i = 0; i < processingData.AlgorithmChecklist.Length;i++)
            {
                if(processingData.AlgorithmChecklist[i])
                {
                    switch(i)
                    {
                        case 0:
                            algorithmList.Add(new KMeans(this));
                            break;
                        case 1:
                            algorithmList.Add(new SingleLinkageHierarchical(this));
                            break;
                        case 2:
                            algorithmList.Add(new JarvisPatrick(this));
                            break;
                        case 3:
                            algorithmList.Add(new CompleteLinkageHierarchical(this));
                            break;
                        case 4:
                            algorithmList.Add(new AverageLinkageHierarchical(this));
                            break;
                    }
                }
                
 
            }


            foreach(DataClustering algorithm in algorithmList)
            {
                DataClustering data = algorithm as Hierarchical;
                if (data == null)
                    algorithm.Clustering();
                if ((processingData.Visualization == true) && (processingData.dimensionQuantity == 2))
                {
                    WindowToDraw visualisationWindow = new WindowToDraw(algorithm);
                    visualisationWindow.Show();
                }
                WriteToFile(algorithm, "kmeans");

            }
        }

        private void WriteToFile()
        {
                using (StreamWriter sw = File.CreateText(processingData.OutputFilePath))
                {
                    sw.WriteLine("Plik wynikowy programu Biblioteka Algorytmów Grupowania \r");
                    sw.WriteLine();
                    sw.WriteLine("Punkty: ");
                    foreach(DataPoint point in Points)
                    {
                        sw.Write("[{0}]:", point.Index);
                        foreach(double number in point.Coordinates)
                        {
                            sw.Write("{0} ", number);
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine();
                    sw.WriteLine();
            }
        }

        private void WriteToFile(DataClustering clustering,string algorithm)
        {
            using (StreamWriter sw = File.AppendText(processingData.OutputFilePath))
            {
                if (algorithm == "kmeans")
                    sw.WriteLine("Algorytm k-średnich");
                else if (algorithm == "jarvis-patrick")
                    sw.WriteLine("Algorytm Jarvisa-Patricka");
                else
                {
                    sw.WriteLine("Algorytm hierarchiczny");
                }
                int index=0;
                foreach(Cluster cluster in clustering.Clusters)
                {
                    sw.WriteLine("Cluster {0}:", index++);
                    if (algorithm == "kmeans")
                    {
                        sw.Write("Centroid: ");
                        foreach(double number in cluster.Centroid.Coordinates)
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
                sw.WriteLine();
                sw.WriteLine();
            }
        }
        public void GenerateRandomData()
        {
            Random rand = new Random();
            int index = 0;
            for(int i = 0; i<processingData.PointsQuantity;i++)
            {
                points.Add(new DataPoint());
                for (int j = 0; j < processingData.dimensionQuantity; j++)
                    points[i].Coordinates.Add((rand.Next(0, 60001)) /(double) 100);
                points[i].Index = index++;
            }
        }
        public void ReadFromFile()
        {

            processingData.dimensionQuantity = 0;
            string[] lines = File.ReadAllLines(processingData.InputFilePath);
            int counter=0;
            List<double> tmp= new List<double>();
            int index = 0;
            
            for(int i = 0;i<lines.Length;i++)
            {
                if (lines[i].Length > 1)
                    if ((lines[i][0].Equals('@')) && (lines[i][1].Equals('A')))
                        processingData.dimensionQuantity++;
                    else if (lines[i][0].Equals('@') || lines[i].Equals("") || lines[i][0].Equals('%'))
                        continue;
                    else
                    {

                        for (int j = 0; j < lines[i].Length; j++)
                        {
                            if ((lines[i][j].Equals(',')) && (counter > 0))
                            {
                                tmp.Add(double.Parse(lines[i].Substring(j - counter, counter), NumberStyles.Any, CultureInfo.InvariantCulture));
                                counter = 0;
                            }
                            else if ((j + 1 == lines[i].Length))
                            {
                                tmp.Add(double.Parse(lines[i].Substring(j - counter, counter + 1), NumberStyles.Any, CultureInfo.InvariantCulture));
                                counter = 0;
                            }
                            else counter++;
                        }
                        Points.Add(new DataPoint(new List<double>(tmp)));
                        Points[index].Index = index++;
                        processingData.PointsQuantity++;
                        tmp.Clear();
                    }
                
            }
            
        }

        public void ReadFromBitmap()
        {
            Bitmap bitmap = new Bitmap(processingData.InputFilePath);
            Color tmpColor;
            for(int y = 0;y<bitmap.Height;y++)
            {
                for (int x = 0; x < bitmap.Height; x++)
                {
                    tmpColor=bitmap.GetPixel(x, y);
                    if ((tmpColor.R != 255) && (tmpColor.G != 255) && (tmpColor.B != 255))
                        Points.Add(new DataPoint(x, y));
                }
            }
        }
      
        

        public void Nop()
        {
            return;
        }
    }
}