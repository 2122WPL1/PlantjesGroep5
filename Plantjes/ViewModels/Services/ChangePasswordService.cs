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
       

       // private DAONieuwWachtwoord _nieuwWachtwoord;


        public event PropertyChangedEventHandler PropertyChanged;
        private DAOGebruiker _dao;


        //error kwam hier op van instantie
        private DAONieuwWachtwoord _daoNieuwWachtwoord = new DAONieuwWachtwoord();


        public ChangePasswordService()
        {
            this._dao = SimpleIoc.Default.GetInstance<DAOGebruiker>();
        }

        public string ChangePasswordButton(string passwordInput, string passwordRepeatInput ,int id)
        {
            string message = String.Empty;


            //
            if (passwordInput.Equals( passwordRepeatInput) && passwordInput != null  && !passwordInput.Trim().Equals(""))
            {
                _daoNieuwWachtwoord.RegisterNewPassword(passwordInput, id);

                message = "Paswoord is verandert";
               
            }
            else
            {
                message = "Paswoord is niet verandert";
            }

            return message;



        }


        //public string RegisterButton(
        //                           string passwordInput, string passwordRepeatInput)
        //{
        //    //errorMessage die gereturned wordt om de gebruiker te waarschuwen wat er aan de hand is
        //    string Message = string.Empty;
        //    //checken of alle velden ingevuld zijn
        //    if (
        //        passwordInput != null &&
        //        passwordRepeatInput != null)
        //    { //checken welke rol je hebt gekozen.
        //       if (passwordInput == passwordRepeatInput)
        //                    {   //gebruiker registreren.
        //                        _daoNieuwWachtwoord.RegisterNewPassword(passwordInput);
        //                        Message = $"{firstNameInput}, je hebt het wachtwoord verandert,";
        //                        LoginWindow loginWindow = new LoginWindow();
        //                        loginWindow.Show();
        //                        Application.Current.Windows[0]?.Close();
        //                    }//foutafhandeling wachtwoord
        //                    else
        //                    {
        //                        Message = "zorg dat de wachtwoorden overeen komen.";
        //                    }
        //                    if (passwordInput == passwordRepeatInput)
        //                    {   //gebruiker registreren.
        //                        _daoNieuwWachtwoord.RegisterNewPassword(passwordInput);

        //                        LoginWindow loginWindow = new LoginWindow();
        //                        loginWindow.Show();
        //                        Application.Current.Windows[0]?.Close();
        //                    }//foutafhandeling wachtwoord
        //                    else
        //                    {
        //                        Message = "zorg dat de wachtwoorden overeen komen.";
        //                    }
        //                }
        //    else
        //    {
        //        Message = "zorg dat alle velden ingevuld zijn";
        //    }//Message terugsturen om te binden aan een label in de viewModel.
        //    return Message;
        //}






    }
}
