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


        //declaratie van propertychanged (om data het model te laten weten wanneer er iets verandert is)

        public event PropertyChangedEventHandler PropertyChanged;
        //declaratie DAta access object voor gebruiker (waarbij het wachtwoord opgehaald wordt) en de passwordchange

        private DAOGebruiker _dao;


        //error kwam hier op van instantie
        private DAONieuwWachtwoord _daoNieuwWachtwoord = new DAONieuwWachtwoord();


        //zet de dao object gelijk aan de instantie van DAoGebruiker = je krijgt de huidige gebruiker in een object met al zijn info
        public ChangePasswordService()
        {
            this._dao = SimpleIoc.Default.GetInstance<DAOGebruiker>();
        }



        public string ChangePasswordButton(string passwordInput, string passwordRepeatInput ,int id)
        {
            //Message voor de lblError om te laten zien of passwoord is changed of niet (of error)
            string message = String.Empty;


            //Als het gelijk is aan elkaar, niet leeg en als het gewoon geen lege spatie is
            if (passwordInput.Equals( passwordRepeatInput) && passwordInput != null  && !passwordInput.Trim().Equals(""))
            {
                //oproep functie register passwoord (met inpassering van het id van de gebruiker)
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
