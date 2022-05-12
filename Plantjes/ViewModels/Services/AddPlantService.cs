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
    public class AddPlantService : IAddPlantService, INotifyPropertyChanged
    {


        private Plant _plant { get; set; }


        //dao verklaren om data op te vragen en te setten in de databank

        private DAOLogic _dao;

        private DAOPlant _plantDao;

        public AddPlantService()
        {
            this._dao = DAOLogic.Instance();
            this._plantDao = SimpleIoc.Default.GetInstance<DAOPlant>();
        }

        //globale plant om te gebruiken in de service
        public Plant plant = new Plant();
        //zorgen dat de INotifyPropertyChanged geimplementeerd wordt
        public event PropertyChangedEventHandler PropertyChanged;


        //Voegt plant toe


        public void AddPlantButton(string naam, TfgsvType type, TfgsvFamilie familie, TfgsvGeslacht geslacht, TfgsvSoort soort, TfgsvVariant variant )
        {
            //errorMessage die gereturned wordt om de gebruiker te waarschuwen wat er aan de hand is
            string Message = string.Empty;
            //checken of alle velden ingevuld zijn en checkt of het plant al bestaat (de naam specifiek)




            //if (naam != null && _dao.CheckIfPlantAlreadyExists(naam))


            if (naam != null)
            {
                //plant registreren.


                




                //method wordt opgeroepen op basis van de meegegeven waarden
                //_dao.RegisterNewPlant(newPlant);

                _plantDao.RegisterNewPlant(naam, type,familie, geslacht, soort, variant);



            }



            //Idee, wat als ik nog een methode maakt met andere verwachtingen en daarna daarvan de data zal nemen?
            //en daarna ze allemaal tesamen doen




            //

        }
    }
}