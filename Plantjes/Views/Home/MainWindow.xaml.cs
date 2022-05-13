using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using Plantjes.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = SimpleIoc.Default.GetInstance<ViewModelMain>();
            InitializeComponent();
        }

        private void btnExporteergeselecteerd_Click(object sender, RoutedEventArgs e)
        {
            if (lstDetails.Items.Count != 0)
            {

                //roept op een dialog box met een default file type csv
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "unknown.csv";
                savefile.Filter = "CSV Files|*.csv";



                if (savefile.ShowDialog() == true)
                {

                    //genereert de csv file die de list van details overloopt en toevoegd
                    using (StreamWriter sw = new StreamWriter(savefile.FileName, false, System.Text.Encoding.Unicode))
                    { // You are missing this one.. 
                        foreach (var item in lstDetails.Items)
                        {
                            sw.WriteLine(item.ToString());
                        }
                    }

                    MessageBox.Show("Saved");
                }

            }
            else
            {
                MessageBox.Show("Zoek en kies eerst een plant");
            }

        }

        
    }
}
