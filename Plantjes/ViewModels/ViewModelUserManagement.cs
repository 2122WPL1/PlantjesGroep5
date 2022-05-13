using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao.DAOdb;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels
{
    public class ViewModelUserManagement : ViewModelBase
    {
        private DAOGebruiker _dao;

        private static SimpleIoc iocc = SimpleIoc.Default;
        private IloginUserService _loginUserService = iocc.GetInstance<IloginUserService>();

        public ViewModelUserManagement(IloginUserService loginUserService)
        {
            this._loginUserService = loginUserService;
            this._dao = SimpleIoc.Default.GetInstance<DAOGebruiker>();

        }

    }
        
}
