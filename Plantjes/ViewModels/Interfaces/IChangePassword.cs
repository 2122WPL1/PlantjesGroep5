using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels.Interfaces
{
    public  interface IChangePassword
    {



        string ChangePasswordButton(
            string passwordInput, string passwordRepeatInput, int id);


    }
}
