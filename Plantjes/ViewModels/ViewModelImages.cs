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
    internal class ViewModelImages : ViewModelBase
    {
        private string _plantName;
        private DAOFoto _daoFoto;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();
        public ViewModelImages(IDetailService detailservice)
        {
            this._daoFoto = SimpleIoc.Default.GetInstance<DAOFoto>();
        }

        //buttons voor de foto's in te laden
        public RelayCommand registerCommand { get; set; }

        public string plantName
        {
            get { return _plantName; }
            set { _plantName = value; }
        }

        public override void Load()
        {
            
            plantName = FillLabelWithNamePlant(_searchService.getSelectedPlant());
        }
    }
}
