//using GalaSoft.MvvmLight.Ioc;
//using Plantjes.ViewModels;
using Microsoft.Win32;
using Plantjes.Models.Db;
using Plantjes.Models.Db.UserManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Plantjes.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlUserManagement.xaml
    /// </summary>
    public partial class UserControlUserManagement : UserControl
    {
        ObservableCollection<Gebruiker> productData;
        public UserControlUserManagement()
        {
            InitializeComponent();
            productData = new ObservableCollection<Gebruiker>();
            //Bind the DataGrid to the customer data - Trúc
            DG1.DataContext = productData;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //string path = @"C:\Users\trucv\source\repos\WERKPLEKLEREN\Excel\Gebruikers_csv.csv";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Reader myReader = new Reader();
                string[] content = myReader.Read(openFileDialog.FileName);
                List<Gebruiker> students = content.Skip(1).Select(x => Gebruiker.FromLine(x)).ToList();
                foreach (Gebruiker s in students)
                {
                    productData.Add(s);
                }
            }

            //DG1.DataContext = File.ReadAllText(openFileDialog.FileName);



            ////to skip the first headlines: gebeurt op iedere lijn
            ////List<Gebruiker> students = content.Skip(1).Select(x => Gebruiker.FromLine(x)).ToList();
            


            //foreach (Gebruiker s in students)
            //{
            //    productData.Add(s);
            //}


        }
    }
}
