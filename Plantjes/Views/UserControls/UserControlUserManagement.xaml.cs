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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Reader myReader = new Reader();
                string[] content = myReader.Read(openFileDialog.FileName);
                List<Gebruiker> students = content.Skip(1).Select(x => Gebruiker.FromLine(x)).ToList(); //to skip the first headlines in every table
                foreach (Gebruiker s in students)
                {
                    productData.Add(s);
                }
            }

            //DG1.DataContext = File.ReadAllText(openFileDialog.FileName);

        }

        //private void btnStudentAanmaken_Click(object sender, RoutedEventArgs e)
        //{
        //    //These codes are used to save the file from the function above into a text file, but this is not the intention - Trúc
        //    //The intention is to save this file into database as "students"
        //    //SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    //saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
        //    //if (saveFileDialog.ShowDialog() == true)
        //    //{
        //    //    File.WriteAllText(saveFileDialog.FileName, (string)DG1.DataContext);
        //    //}
            
        //    //File.WriteAllText(saveFileDialog.FileName, (string)DG1.DataContext);



        //}
    }
}
