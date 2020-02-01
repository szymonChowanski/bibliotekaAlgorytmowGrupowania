using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;


namespace Clustering
{
    
    public partial class MainWindow : Window
    {
        MainProgramClass main = new MainProgramClass();
        bool _startAlreadyClicked=false;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(_startAlreadyClicked)
            {
                ProcessingData oldProcessingData = main.processingData;
                main = new MainProgramClass(oldProcessingData);   
            }

            _startAlreadyClicked = true;

            for(int i = 0;i<3;i++)
            {
                if (main.processingData.AlgorithmChecklist[i] == true)
                    break;
                if(i==4)
                {
                    MessageBox.Show("Proszę wybrać algorytm");
                    return;
                }
            }

            if((RandomDataRadioButton.IsChecked==false) && (FromFileRadioButton.IsChecked==false)&&(FromBitmapRadioButton.IsChecked==false))
            {
                MessageBox.Show("Proszę wybrać typ danych wejściowych");
                return;
            }

            if(((FromFileRadioButton.IsChecked==true)||(FromBitmapRadioButton.IsChecked==true))&&(main.processingData.InputFilePath==""))
            {
                MessageBox.Show("Proszę wybrać plik wejściowy");
                return;
            }

            if(main.processingData.OutputFilePath=="result.txt")
            {
                MessageBox.Show("Nie wybrano pliku. Wyniki zostaną zapisane w domyślnym pliku result.txt");
            }

            if(RandomDataRadioButton.IsChecked==true)
                try
                {
                    main.processingData.PointsQuantity = int.Parse(QuantityInputTextBox.Text);
                    main.processingData.dimensionQuantity = int.Parse(DimensionTextBox.Text);
                }
                catch (FormatException)
                {

                    MessageBox.Show("Proszę wpisać liczbę całkowitą.");
                    return;
                }

            if(KmeansCheckbox.IsChecked==true)
                try
                {
                    main.processingData.GroupsQuantity = int.Parse(GroupQuantityTextBox.Text);
                }
                catch (FormatException)
                {

                    MessageBox.Show("Proszę wpisać liczbę całkowitą.");
                    return;
                }

            if (JarvisPatrickCheckbox.IsChecked == true)
                try
                {
                    main.processingData.RequiredNeighbors = int.Parse(NeighborsInCommonTextBox.Text);
                    main.processingData.NeighborsToExamine = int.Parse(NeighborsToExamineTextBox.Text);
                }
                catch (FormatException)
                {

                    MessageBox.Show("Proszę wpisać liczbę całkowitą.");
                    return;
                }


            main.Setup();
        }

        private void KmeansCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            KmeansLabel.IsEnabled = true;
            KmeansGroupsLabel.IsEnabled = true;
            GroupQuantityTextBox.IsEnabled = true;
            main.processingData.AlgorithmChecklist[0] = true;
        }

        private void KmeansCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            KmeansLabel.IsEnabled = false;
            KmeansGroupsLabel.IsEnabled = false;
            GroupQuantityTextBox.IsEnabled = false;
            main.processingData.AlgorithmChecklist[0] = false;
        }

        private void SingleLinkCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //HierarchicalLabel.IsEnabled = true;
            main.processingData.AlgorithmChecklist[1] = true;
        }

        private void SingleLinkCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            //HierarchicalLabel.IsEnabled = false;
            main.processingData.AlgorithmChecklist[1] = false;
        }

        private void CompleteLinkCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            main.processingData.AlgorithmChecklist[3] = true;
        }

        private void CompleteLinkCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            main.processingData.AlgorithmChecklist[3] = false;
        }

        private void AverageLinkCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            main.processingData.AlgorithmChecklist[4] = true;
        }

        
        private void AverageLinkCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            main.processingData.AlgorithmChecklist[4] = false;
        }

        private void JarvisPatrickCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            JarvisPatrickLabel.IsEnabled = true;
            NeighborsInCommonLabel.IsEnabled = true;
            NeighborsInCommonTextBox.IsEnabled = true;
            NeighborsToExamineLabel.IsEnabled = true;
            NeighborsToExamineTextBox.IsEnabled = true;
          
            main.processingData.AlgorithmChecklist[2] = true;
        }

        private void JarvisPatrickCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            JarvisPatrickLabel.IsEnabled = false;
            NeighborsInCommonLabel.IsEnabled = false;
            NeighborsInCommonTextBox.IsEnabled = false;
            NeighborsToExamineLabel.IsEnabled = false;
            NeighborsToExamineTextBox.IsEnabled = false;

            main.processingData.AlgorithmChecklist[2] = false;
        }

        private void FileOpen(ref string processingDataReference, string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                processingDataReference = openFileDialog.FileName;
            }
        }

        private void FileOpenButton_Click(object sender, RoutedEventArgs e) => FileOpen(ref main.processingData.InputFilePath, "Text files (*.arff)|*.arff|All files (*.*)|*.*");
        private void BitmapOpenButton_Click(object sender, RoutedEventArgs e) => FileOpen(ref main.processingData.InputFilePath, "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*");
        private void OutputFilePathButton_Click(object sender, RoutedEventArgs e) => FileOpen(ref main.processingData.OutputFilePath, "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        

        private void FromFileRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            VisualisationCheckbox.IsEnabled = true;
            BitmapOpenButton.IsEnabled = false;
            QuantityInputTextBox.IsEnabled = false;
            QuantityOfRandomDataLabel.IsEnabled = false;
            FileOpenButton.IsEnabled = true;
            DimensionLabel.IsEnabled = false;
            DimensionTextBox.IsEnabled = false;
            
            main.processingData.readFromBitmap = false;
            main.processingData.InputFilePath = "";
            main.processingData.readFromFile = true;
        }

        private void RandomDataRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            VisualisationCheckbox.IsEnabled = false;
            BitmapOpenButton.IsEnabled = false;
            QuantityInputTextBox.IsEnabled = true;
            QuantityOfRandomDataLabel.IsEnabled = true;
            FileOpenButton.IsEnabled = false;
            DimensionLabel.IsEnabled = true;
            DimensionTextBox.IsEnabled = true;
            
            main.processingData.readFromBitmap = false;
            main.processingData.readFromFile = false;
        }

        private void FromBitmapRadioButton_Checked(object sender, RoutedEventArgs e)
        {           
            QuantityInputTextBox.IsEnabled = false;
            QuantityOfRandomDataLabel.IsEnabled = false;
            FileOpenButton.IsEnabled = false;
            DimensionLabel.IsEnabled = false;            
            DimensionTextBox.IsEnabled = false;
            VisualisationCheckbox.IsEnabled = true;
            BitmapOpenButton.IsEnabled = true;

            main.processingData.readFromBitmap = true;
            main.processingData.dimensionQuantity = 2;
            main.processingData.InputFilePath = "";
            main.processingData.readFromFile = false;
        }

        private void VisualisationCheckbox_Checked(object sender, RoutedEventArgs e) => main.processingData.Visualization = true;

        private void VisualisationCheckbox_Unchecked(object sender, RoutedEventArgs e) => main.processingData.Visualization = false;


        private void DistanceFunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // main.processingData.DistanceFunction = DistanceFunctionComboBox.GetValue(null);
        }

        public static void ShowMessage(string message) => MessageBox.Show(message);

        private void DimensionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (int.Parse(DimensionTextBox.Text) == 2)
                    VisualisationCheckbox.IsEnabled = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Proszę wpisać liczbę całkowitą");
                return;
            } 
        }  
    }
}
