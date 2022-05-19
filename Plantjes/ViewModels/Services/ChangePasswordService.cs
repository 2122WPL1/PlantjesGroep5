using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Views.Home;
using XAct.Messages;

namespace Plantjes.ViewModels.Services
{
    public class ChangePasswordService : INotifyPropertyChanged, IChangePassword
    {
       

       // private DAONieuwWachtwoord _nieuwWachtwoord; - Imran


        //decl propertychanged - I

        public event PropertyChangedEventHandler PropertyChanged;
       


        //decl used variables of DAO type  - I
        private DAOGebruiker _dao;


       
        private DAONieuwWachtwoord _daoNieuwWachtwoord = new DAONieuwWachtwoord();


        //assign the instance of user, which means you get the current user their info -I
        public ChangePasswordService()
        {
            this._dao = SimpleIoc.Default.GetInstance<DAOGebruiker>();
        }


        //function ChangePassword (check i it's the same or not, or give a error message which will be pasted into the label in UI-I
        public string ChangePasswordButton(string passwordInput, string passwordRepeatInput ,int id)
        {
            //Message for lblError to show if password is changed or not -I
            string message = String.Empty;


            //Check the input and see if it's correct (two times the same written password and if it isn't just empty) -I
            if (passwordInput.Equals( passwordRepeatInput) && passwordInput != null  && !passwordInput.Trim().Equals(""))
            {
                //Call up the function to replace the new password in database - I
                _daoNieuwWachtwoord.RegisterNewPassword(passwordInput, id);


                message = "Paswoord is verandert";
               
            }
            else
            {
                message = "Paswoord is niet verandert";
            }
            //the message for lblError
            return message;



        }






    }
}
