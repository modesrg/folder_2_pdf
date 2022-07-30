using Ookii.Dialogs.Wpf;
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

namespace MMMergeToPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string selectedPath;
        int rotarPag = 0;
        int fileType = 0;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void FolderTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedPath = FolderTxtBox.Text;
        }

        private void BrowseBtn_Click_1(object sender, RoutedEventArgs e)
        {
            switch (Environment.OSVersion.Platform)
            {

                case PlatformID.Win32NT:
                    VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
                    folderBrowserDialog.ShowDialog();
                    FolderTxtBox.Text = folderBrowserDialog.SelectedPath;
                    break;
                case PlatformID.MacOSX:
                    
                    break;
                case PlatformID.Unix:
                    break;


            }
        }

        private void RotateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rotarPag = RotateComboBox.SelectedIndex;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            MMMergeToPDFService mMMergeToPDFService = new MMMergeToPDFService();

            switch (fileType)
            {
                case 0:
                    mMMergeToPDFService.MergePdfFiles(selectedPath, rotarPag);
                    break;
                case 1:
                    mMMergeToPDFService.MergePdfFilesFromJpeg(selectedPath, rotarPag);
                    break;
            }
        }

        private void TipoFicheros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fileType = TipoFicheros.SelectedIndex;
        }
    }
}
