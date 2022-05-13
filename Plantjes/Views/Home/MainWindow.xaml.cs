using GalaSoft.MvvmLight.Ioc;
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
            //StreamWriter myOutputStream = new StreamWriter("Myfile.csv");

            //foreach (var item in lstDetails.Items)
            //{
            //    myOutputStream.WriteLine(item.ToString());
            //}

            //myOutputStream.Close();

            //using (TextWriter outputfile = new StreamWriter("StudentRecords.csv"))
            //{
            //foreach (var data in lstDetails.Items)
            //{

            //    //var dataArray = data.split(':');
            //   // var line = string.Format("{0},{1}", data[0], data[1]);
            //    //outputfile.WriteLine(line);
            //    outputfile.Flush();
            //}
            //MessageBox.Show("Student Information inserted successfully");



            //}

            //MessageBox.Show(lstDetails.Items[0].ToString());

            





        }

       
    }
}
