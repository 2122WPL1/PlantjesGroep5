//using GalaSoft.MvvmLight.Ioc;
//using Plantjes.ViewModels;
using Microsoft.Win32;
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
        ObservableCollection<Gebruiker> studentData;
        public UserControlUserManagement()
        {
            InitializeComponent();
            studentData = new ObservableCollection<Gebruiker>();
            //Bind the DataGrid to the student list data - Trúc
            DG1.DataContext = studentData;
            ds = new DataSet();
        }
        DataSet ds;


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
        {
            //Entity Framework Extensions: 
            //List<Gebruiker> students = new List<Gebruiker>();
            //using (var ctx = new EntitiesContext())
            //{
            //    // Do not use the ChangeTracker or require to add the list in the DbSet
            //    ctx.BulkInsert(students);
            //}


            //binding grid to data from database:
            SqlConnection connection = new SqlConnection(@"Data Source=ltp-truwald\vives;Initial Catalog=planten;Integrated Security=True");
            connection.Open();
            SqlDataAdapter adp = new SqlDataAdapter("select vivesnr, voornaam, achternaam, emailadres from gebruiker", connection);
            adp.Fill(ds);
            //DataSet doesn't implement IEnumerable, so you can't cast it to any of those forms.
            //A DataSet is a collection of DataTables and those DataTables contain collections of Rows.
            //the tables of DataSet can potentially contain different column sets.
            //original:  DataTable dt = ds.Tables[0];1
            //dataGridView1.DataSource = dt;       2
            DataTable dt = ds.Tables.SelectMany(table => table.Rows);
            //studentData.Add((Gebruiker)ds);??
            DG1.ItemsSource = ds.Tables[0].AsEnumerable();//here i cast Datagrid.Itemsource directly to Enumerable, it might be a problem here
            connection.Close();


            //updating values in grid:
            connection.Open();
            //MessageBox.Show("test");
            SqlDataAdapter adp2 = new SqlDataAdapter("select * from gebruiker", connection);
            SqlCommandBuilder build = new SqlCommandBuilder(adp2);
            adp2.Update(ds.Tables[0]);
            connection.Close();


            //int count = 100;
            //using (var db = new BloggingContext())
            //{
            //    data.Configuration.AutoDetectChangesEnabled = false;
            //    foreach (var item in data)
            //    {
            //        db.Items.Add(item);
            //        --count;
            //        if (count <= 0)
            //        {
            //            count = 100;
            //            db.SaveChanges();
            //        }
            //    }
            //    db.SaveChanges(); // To make sure we save everything if the last part was smaller than 100
            //}

        }
    }

    //internal class EntitiesContext : IDisposable
    //{
    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
