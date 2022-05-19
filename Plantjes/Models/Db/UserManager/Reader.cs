using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Models.Db.UserManager
{
    public class Reader
    {

        //Methode to read everything: (for reading an Excel file of Students into DataGrid, UserControlUserManagement)
        public string[] Read(string path)
        {
            string[] content = System.IO.File.ReadAllLines(path);
            return content;
        }

        //public string[] Save(string path)
        //{
        //    string[] content = System.IO.File.GetAttributes(path);
        //    return content;
        //}
    }
}
