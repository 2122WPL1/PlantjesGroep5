﻿using Plantjes.ViewModels.Interfaces;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Enums;
using Plantjes.Views.Home;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Plantjes.Dao.DAOdb;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;

namespace Plantjes.ViewModels.Services
{
    public class LoginUserService : IloginUserService
    {   
        //gebruiker verklaren  om te gebruiken in de logica
        private Gebruiker _gebruiker { get; set; }
        private DAORol _daoRol;
        //dao verklaren om data op te vragen en te setten in de databank
        private DAOGebruiker _dao;


        public LoginUserService()
        {
            this._dao = SimpleIoc.Default.GetInstance<DAOGebruiker>();
        }
        public void fillComboBoxRol(ObservableCollection<Rol> cmbRolCollection)
        {
            //this._daoRol = DAORol.Instance();
            this._daoRol = SimpleIoc.Default.GetInstance<DAORol>();
            var list = _daoRol.fillRol();

            foreach (var item in list)
            {
                cmbRolCollection.Add(item);
            }

        }
        #region Login Region
        //globale gebruiker om te gebruiken in de service
        public Gebruiker gebruiker = new Gebruiker();
        //zorgen dat de INotifyPropertyChanged geimplementeerd wordt
        public event PropertyChangedEventHandler PropertyChanged;
        
        //het eigenlijke loginsysteem
        public LoginResult CheckCredentials(string userNameInput, string passwordInput)
        {   //Nieuw loginResult om te gebruiken, status op NotLoggedIn zetten
            var loginResult = new LoginResult() {loginStatus = LoginStatus.NotLoggedIn};
           
            //check if email is valid email
            if (userNameInput != null && userNameInput.Contains("@"))
            {   //gebruiker zoeken in de databank
                gebruiker = _dao.GetGebruikerWithEmail(userNameInput);
                loginResult.gebruiker = gebruiker;
            }
            else
            {//indien geen geldig emailadress, errorMessage opvullen
                loginResult.errorMessage = "Dit is geen geldig emailadres.";
            }

            //omzetten van het ingegeven passwoord naar een gehashed passwoord
            var passwordBytes = Encoding.ASCII.GetBytes(passwordInput);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            if (gebruiker != null)
            {
                _gebruiker = gebruiker;
                loginResult.gebruiker = gebruiker;
                //passwoord controle
                if (gebruiker.HashPaswoord != null && passwordHashed.SequenceEqual(gebruiker.HashPaswoord))
                {   //indien true status naar LoggedIn zetten
                    loginResult.loginStatus = LoginStatus.LoggedIn;
                }
                else
                {   //indien false errorMessage opvullen
                    loginResult.errorMessage += "\r\n"+"Het ingegeven wachtwoord is niet juist, probeer opnieuw";
                }
            }
            else
            {   // als de gebruiker niet gevonden wordt, errorMessage invullen
                loginResult.errorMessage = $"Er is geen account gevonden voor {userNameInput} "+"\r\n"+" gelieve eerst te registreren";
            }
            return loginResult;
        }

        //functie om naam weer te geven in loginWindow
        public string LoggedInMessage()
        {
            string message= String.Empty;
            if (_gebruiker != null)
            {
                message = $"ingelogd als: {_gebruiker.Voornaam} {_gebruiker.Achternaam}";
                return message;
            }
            return message;
        }

        #endregion

        #region Register Region

        public string RegisterButton(string vivesNrInput, string lastNameInput, 
                                   string firstNameInput, string emailAdresInput,
                                   string passwordInput, string passwordRepeatInput, int SelectedRol)
        {
            //errorMessage die gereturned wordt om de gebruiker te waarschuwen wat er aan de hand is
            string Message = string.Empty;
            //checken of alle velden ingevuld zijn
            if (firstNameInput != null &&
                lastNameInput != null &&
                SelectedRol != null &&
                emailAdresInput != null &&
                passwordInput != null &&
                passwordRepeatInput != null)
            { //checken welke rol je hebt gekozen.
               if (SelectedRol.Equals(0))
               {   //checken of het de juiste kenmerken geeft voor een docent nummer.
                   if (vivesNrInput != null && vivesNrInput.Length.Equals(8) && vivesNrInput.Contains("u"))
                   {   //checken als het emailadres een geldig vives email is voor een docent.
                       if (emailAdresInput != null && emailAdresInput.Contains("@") && emailAdresInput.Contains("vives.be") 
                       //checken als het email adres al bestaat of niet.
                       && _dao.CheckIfEmailAlreadyExists(emailAdresInput))
                       {   //checken als het herhaalde wachtwoord klopt of niet.
                           if (passwordInput == passwordRepeatInput)
                           {   //gebruiker registreren.
                               _dao.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, SelectedRol, emailAdresInput, passwordInput);
                               Message = $"{firstNameInput}, je bent succevol geregistreerd," + "\r\n" + $" uw gebruikersnaam is {emailAdresInput}." +
                                              "\r\n" + $" {firstNameInput}, je kan dit venster wegklikken en inloggen.";
                               LoginWindow loginWindow = new LoginWindow();
                               loginWindow.Show();
                                Application.Current.Windows[0]?.Close();
                            }//foutafhandeling wachtwoord
                           else
                           {
                               Message = "zorg dat de wachtwoorden overeen komen.";
                           }
                       }//foutafhandeling emailadres
                       else
                       {
                           Message = $"{emailAdresInput} is geen geldig \r\n emailadres voor een docent, " + "\r\n" + " of het eamiladres is al in gebruik.";
                       }
                   }//foutafhandeling vives nummer
                   else
                   {
                       Message = "Het vives nummer is niet juist \r\nvoor een docent";
                   }
               }//checken welke rol je hebt gekozen.
               else if (SelectedRol.Equals(1))
               {   //checken of het de juiste kenmerken geeft voor een student nummer.
                   if (vivesNrInput != null && vivesNrInput.Length.Equals(8) && vivesNrInput.Contains("r"))
                   {   //checken als het emailadres een geldig vives email is voor een student.
                       if (emailAdresInput != null && emailAdresInput.Contains("@") && emailAdresInput.Contains("student.vives.be") 
                           //checken als het email adres al bestaat of niet.
                           && _dao.CheckIfEmailAlreadyExists(emailAdresInput))
                       {   //checken als het herhaalde wachtwoord klopt of niet.
                           if (passwordInput == passwordRepeatInput)
                           {   //gebruiker registreren.
                               _dao.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, SelectedRol, emailAdresInput, passwordInput);
                               Message = $"{firstNameInput}, je bent succevol geregistreerd," + "\r\n" + $" uw gebruikersnaam is {emailAdresInput}." +
                                              "\r\n" + $" {firstNameInput}, je kan dit venster wegklikken en inloggen.";
                               LoginWindow loginWindow = new LoginWindow();
                               loginWindow.Show();
                                Application.Current.Windows[0]?.Close();
                            }//foutafhandeling wachtwoord
                           else
                           {
                               Message = "zorg dat de wachtwoorden overeen komen.";
                           }
                       }//foutafhandeling emailadres
                       else
                       {
                           Message = $"{emailAdresInput} is geen geldig \r\n emailadres voor een student, " + "\r\n" + " of het eamiladres is al in gebruik.";
                       }
                   }//foutafhandeling vives nummer
                   else
                   {
                       Message = "Het vives nummer is niet juist \r\nvoor een student";
                   }
               }//checken welke rol je hebt gekozen.
               else if (SelectedRol.Equals(2))
               {   //checken of het leeg is voor een oudstudent.
                   if (string.IsNullOrWhiteSpace(vivesNrInput))
                   {   //checken als het een geldig emailadres is.
                       if (emailAdresInput != null && emailAdresInput.Contains("@") && _dao.CheckIfEmailAlreadyExists(emailAdresInput))
                       {   //checken als het herhaalde wachtwoord klopt of niet.
                           if (passwordInput == passwordRepeatInput)
                           {   //gebruiker registreren.
                               _dao.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, SelectedRol, emailAdresInput, passwordInput);
                               Message = $"{firstNameInput}, je bent succevol geregistreerd," + "\r\n" + $" uw gebruikersnaam is {emailAdresInput}." +
                                              "\r\n" + $" {firstNameInput}, je kan dit venster wegklikken en inloggen.";
                               LoginWindow loginWindow = new LoginWindow();
                               loginWindow.Show();
                                Application.Current.Windows[0]?.Close();
                            }//foutafhandeling wachtwoord
                            else
                           {
                               Message = "zorg dat de wachtwoorden overeen komen.";
                           }
                       }//foutafhandeling emailadres
                        else
                        {
                            Message = $"{emailAdresInput} is geen geldig emailadres.";
                        }
                   }//foutafhandeling vives nummer
                   else
                   {
                       Message = "vivesnummer moet leeg zijn.";
                   }
               }//foutafhandeling rol
               else
               {
                   Message = "Het in gegeven rol bestaat niet, kies uit:\r\n Docent, Student, Oud-student.";
               }
            }//foutafhandeling velden
            else
            {
                Message = "zorg dat alle velden ingevuld zijn";
            }//Message terugsturen om te binden aan een label in de viewModel.
            return Message;
        }

        public void SaveChanges()
        {
            //throw new NotImplementedException();
        }



        #endregion
    }
    
}