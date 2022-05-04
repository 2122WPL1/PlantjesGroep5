//using GalaSoft.MvvmLight.Ioc;
//using Plantjes.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using Plantjes.Models.Db.UserManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
        DAOGebruiker _daoGebruiker = SimpleIoc.Default.GetInstance<DAOGebruiker>();
        ObservableCollection<Gebruiker> studentData;
        
        public UserControlUserManagement()
        {
            InitializeComponent();
            studentData = new ObservableCollection<Gebruiker>();
            //Bind the DataGrid to the student list data - Trúc
            DG1.DataContext = studentData;
        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Reader myReader = new Reader();
                string[] content = myReader.Read(openFileDialog.FileName);
                List<Gebruiker> students = content.Skip(1).Select(x => Gebruiker.FromLine(x)).ToList(); //to select all the rest and skip the first headlines in every table - Trúc
                foreach (Gebruiker s in students)
                {
                    studentData.Add(s);
                }
            }
        }

        private void btnStudentAanmaken_Click(object sender, RoutedEventArgs e)
        {;
            _daoGebruiker.AddStudents(studentData.ToList());
        }
    }

}
