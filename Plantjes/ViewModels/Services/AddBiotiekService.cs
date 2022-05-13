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
    public class AddBiotiekService : IAddAbiotiekService, INotifyPropertyChanged
    {



        public event PropertyChangedEventHandler PropertyChanged;



        private Plant _plant { get; set; }


    //dao verklaren om data op te vragen en te setten in de databank

    private DAOLogic _dao;

    private DAOPlant _plantDao;

    private DAOAbiotiek _daoAbiotiek;


    public AddBiotiekService()
    {
        this._dao = DAOLogic.Instance();
        this._plantDao = SimpleIoc.Default.GetInstance<DAOPlant>();
            this._daoAbiotiek = SimpleIoc.Default.GetInstance<DAOAbiotiek>();
        }

    




    public void AddAbiotiekButton(string abioBezonning, string abioGrondsoort)
    {
       

           // _plantDao.RegisterNewPlant(naam, type, familie, geslacht, soort, variant);



            _daoAbiotiek.AddPlantAbiotiek( _plantDao.GetPlant.PlantId,  abioBezonning,  abioGrondsoort);
            //MessageBox.Show(_plantDao.getCurrentPlant().NederlandsNaam);

        }


    }
}
