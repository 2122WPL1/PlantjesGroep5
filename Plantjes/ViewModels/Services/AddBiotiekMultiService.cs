using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.Dao.DAOdb;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels.Services
{
    public class AddBiotiekMultiService : IAddAbiotiekMultiService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DAOLogic _dao;
      

      

        private DAOPlant _plantDao;


        //To get into Databank Abiotiek-I
        private DAOAbiotiek _daoAbiotiek;

        private DAOAbiotiekMulti _daoAbiotiekMulti;


        public AddBiotiekMultiService()
        {
           
            this._plantDao = SimpleIoc.Default.GetInstance<DAOPlant>();
            this._daoAbiotiek = SimpleIoc.Default.GetInstance<DAOAbiotiek>();


            this._daoAbiotiekMulti = SimpleIoc.Default.GetInstance<DAOAbiotiekMulti>();
        }



        
        public void AddAbiotiekMultiButton(List<string> waarde)
        {

            //the same Id will be given to serve as foreigner key-Imran
            _daoAbiotiekMulti.AddPlantAbiotiekMulti(_plantDao.GetPlant.PlantId, waarde);


        }




    }

}
