using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Interaction logic for UserControlName.xaml
    /// </summary>
    public partial class UserControlName : UserControl
    {
        public UserControlName()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            //StreamWriter myOutputStream = new StreamWriter("Myfile.csv");

            //foreach (var item in lstDetails.Items)
            //{
            //    myOutputStream.WriteLine(item.ToString());
            //}

            //myOutputStream.Close();


            if (lstDetails.Items.Count !=0)
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
