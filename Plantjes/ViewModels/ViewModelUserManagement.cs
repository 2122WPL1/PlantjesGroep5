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
        private DAOProvide _dao;

        private static SimpleIoc iocc = SimpleIoc.Default;
        private IDetailService _detailService = iocc.GetInstance<IDetailService>();
        public ViewModelUserManagement(IDetailService detailservice)
        {
            _detailService = detailservice;
            this._dao = SimpleIoc.Default.GetInstance<DAOProvide>();
        }
    }
}
