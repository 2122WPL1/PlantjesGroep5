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

        //Imran
        //property change implemenation 
        public event PropertyChangedEventHandler PropertyChanged;



    //declare DAO objects to asign later for access to databank-Imran

    private DAOLogic _dao;

        //necessary for PlantId (fk)-I
    private DAOPlant _plantDao;


        //To get into Databank Abiotiek-I
    private DAOAbiotiek _daoAbiotiek;



        //Assignement of DAO - I
    public AddBiotiekService()
    {
        this._dao = DAOLogic.Instance();
        this._plantDao = SimpleIoc.Default.GetInstance<DAOPlant>();
            this._daoAbiotiek = SimpleIoc.Default.GetInstance<DAOAbiotiek>();
        }

    



        //Add function for ABiotik - Imran
    public void AddAbiotiekButton(string abioBezonning, string abioGrondsoort)
    {
       

           
            
            //When you add a Abiotiek you need a plantId, this is where the calls works in - Imran
            _daoAbiotiek.AddPlantAbiotiek( _plantDao.GetPlant.PlantId,  abioBezonning,  abioGrondsoort);
            

        }


    }
}
