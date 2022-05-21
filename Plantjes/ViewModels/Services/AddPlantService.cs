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
using System.Windows;

namespace Plantjes.ViewModels.Services
{
    public class AddPlantService : IAddPlantService, INotifyPropertyChanged
    {
        //Imran

        private Plant _plant { get; set; }


        //Declare DAO -I

        //private DAOLogic _dao;
       

        private DAOPlant _plantDao;


        private DAOAbiotiek _daoAbiotiek;

        //constr assignement DAO - I
        public AddPlantService()
        {
         
            this._plantDao = SimpleIoc.Default.GetInstance<DAOPlant>();
            this._daoAbiotiek = SimpleIoc.Default.GetInstance<DAOAbiotiek>();
        }

        //Necessary for plant object (which shall be giving into database. -
        public Plant plant = new Plant();
        //impl propchanged - I
        public event PropertyChangedEventHandler PropertyChanged;


        //Add Plant to database - I

        public void AddPlantButton(string naam, TfgsvType type, TfgsvFamilie familie, TfgsvGeslacht geslacht, TfgsvSoort soort, TfgsvVariant variant )
        {
           
            //if (naam != null && _dao.CheckIfPlantAlreadyExists(naam))

            //Check if name property is empty or not (a must prop) - Imran
            
             
               //Data is giving into the new function for DAO class - I

            _plantDao.GetPlant = _plantDao.RegisterNewPlant(naam, type,familie, geslacht, soort, variant);

            MessageBox.Show("Plant is opgeslaan");
            
            

        }



    }
}