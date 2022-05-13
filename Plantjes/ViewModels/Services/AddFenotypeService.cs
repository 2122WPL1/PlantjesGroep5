using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels.Services
{
    public class AddFenotypeService: IAddFenotypeService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        private Plant _plant { get; set; }


        //dao verklaren om data op te vragen en te setten in de databank

        

        private DAOPlant _plantDao;

        private DAOFenotype _daoFenotype;


        public AddFenotypeService()
        {
           
            this._plantDao = SimpleIoc.Default.GetInstance<DAOPlant>();
            this._daoFenotype= SimpleIoc.Default.GetInstance<DAOFenotype>();
        }


        public void AddFenotypeButton(int fenoBladgrootte, string fenoBladvorm, string fenoRatioBloeiBlad, string fenoSpruitfenologie/*, string fenoBloeiwijze, string fenoHabitus, string fenoLevensvorm*/)
        {

            _daoFenotype.AddPlantFenotype(_plantDao.GetPlant.PlantId, fenoBladgrootte, fenoBladvorm, fenoRatioBloeiBlad, fenoSpruitfenologie/*, fenoBloeiwijze, fenoHabitus, fenoLevensvorm*/);

        }
    }
}

