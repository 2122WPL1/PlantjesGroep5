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
            //written by Mathias
            string message= String.Empty;
            if (_gebruiker != null)
            {
                string rol = "";
                if (_gebruiker.RolId == 0)
                {
                    rol = "Docent";
                }
                else if (_gebruiker.RolId == 1 )
                {
                    rol = "Student";
                }
                else
                {
                    rol = "Oudstudent";
                }
                message = $"ingelogd als: {_gebruiker.Voornaam} {_gebruiker.Achternaam} " + rol;
                return message;
            }
            return message;
        }

        #endregion

        #region Register Region
        //  error handeling written by Mathias
        public string RegisterButton(string vivesNrInput, string lastNameInput, 
                                   string firstNameInput, string emailAdresInput,
                                   string passwordInput, string passwordRepeatInput, int SelectedRol)
        {
            //errorMessage die gereturned wordt om de gebruiker te waarschuwen wat er aan de hand is
            string Message = string.Empty;
            //checken of alle velden ingevuld zijn
            if (firstNameInput != null &&
                lastNameInput != null &&
                emailAdresInput != null &&
                passwordInput != null &&
                passwordRepeatInput != null)
            { //checken welke rol je hebt gekozen rolId 0 = docent.
               if (SelectedRol.Equals(0))
                {   //checken of het de juiste kenmerken geeft voor een docent nummer en of het al wordt gebruikt.
                    if (vivesNrInput != null && vivesNrInput.Length.Equals(8) && vivesNrInput.Contains("u")
                        && _dao.CheckIVivesnrAlreadyExists(vivesNrInput))
                   {   //checken als het emailadres een geldig vives email is voor een docent.
                       if (emailAdresInput != null && emailAdresInput.Contains("@") && emailAdresInput.Contains("vives.be") 
                       //checken als het email adres al bestaat of niet.
                       && _dao.CheckIfEmailAlreadyExists(emailAdresInput))
                       {   //checken als het herhaalde wachtwoord klopt of niet.
                           if (passwordInput == passwordRepeatInput)
                           {   //gebruiker registreren.
                               _dao.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, SelectedRol, emailAdresInput, passwordInput);
                                Message = $"Je hebt succevol een nieuwe docent geregistreerd," + "\r\n" + $"de gebruikersnaam is {emailAdresInput}" +
                                               "\r\n" + "je kan nu naar andere tabs gaan of nog gebruikers toevoegen.";
                            }//foutafhandeling wachtwoord
                           else
                           {
                               Message = "zorg dat de wachtwoorden overeen komen.";
                           }
                       }//foutafhandeling emailadres
                       else
                       {
                           Message = $"{emailAdresInput} is geen geldig \r\nemailadres voor een docent bv: docentnaam@vives.be, " + "\r\n" + "of het emailadres is al in gebruik.";
                       }
                   }//foutafhandeling vives nummer
                   else
                   {
                       Message = "Het vives nummer is niet juist of het nummer is al in gebruik \r\nvoor een docent moet het een u bevaten \r\nen een lengte hebben van 8 characters bv: u1234567";
                   }
                }//checken welke rol je hebt gekozen rolId 1 = student.
               else if (SelectedRol.Equals(1))
               {   //checken of het de juiste kenmerken geeft voor een student nummer en of het al wordt gebruikt.
                   if (vivesNrInput != null && vivesNrInput.Length.Equals(8) && vivesNrInput.Contains("r") | vivesNrInput.Contains("s")
                        && _dao.CheckIVivesnrAlreadyExists(vivesNrInput))
                   {   //checken als het emailadres een geldig vives email is voor een student.
                       if (emailAdresInput != null && emailAdresInput.Contains("@") && emailAdresInput.Contains("student.vives.be") 
                           //checken als het email adres al bestaat of niet.
                           && _dao.CheckIfEmailAlreadyExists(emailAdresInput))
                       {   //checken als het herhaalde wachtwoord klopt of niet.
                           if (passwordInput == passwordRepeatInput)
                           {   //gebruiker registreren.
                               _dao.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, SelectedRol, emailAdresInput, passwordInput);
                                Message = $"Je hebt succevol een nieuwe student geregistreerd," + "\r\n" + $"de gebruikersnaam is {emailAdresInput}" +
                                               "\r\n" + "je kan nu naar andere tabs gaan of nog gebruikers toevoegen.";
                            }//foutafhandeling wachtwoord
                            else
                           {
                               Message = "zorg dat de wachtwoorden overeen komen.";
                           }
                       }//foutafhandeling emailadres
                       else
                       {
                           Message = $"{emailAdresInput} is geen geldig \r\nemailadres voor een student bv: studentnaam@student.vives.be, " + "\r\n" + "of het emailadres is al in gebruik.";
                       }
                   }//foutafhandeling vives nummer
                   else
                   {
                       Message = "Het vives nummer is niet juist of het nummer is al in gebruik \r\nvoor een student moet het een r of s bevaten \r\nen een lengte hebben van 8 characters bv: r1234567";
                   }
               }//checken welke rol je hebt gekozen rolId 2 = oudstudent.
               else if (SelectedRol.Equals(2))
               {   //checken of het leeg is voor een oudstudent.
                   if (string.IsNullOrWhiteSpace(vivesNrInput))
                   {   //checken als het een geldig emailadres is.
                       if (emailAdresInput != null && emailAdresInput.Contains("@") && _dao.CheckIfEmailAlreadyExists(emailAdresInput))
                       {   //checken als het herhaalde wachtwoord klopt of niet.
                           if (passwordInput == passwordRepeatInput)
                           {   //gebruiker registreren.
                               _dao.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, SelectedRol, emailAdresInput, passwordInput);
                                Message = $"Je hebt succevol een nieuwe oudstudent geregistreerd," + "\r\n" + $"de gebruikersnaam is {emailAdresInput}" +
                                                "\r\n" + "je kan nu naar andere tabs gaan of nog gebruikers toevoegen.";
                            }//foutafhandeling wachtwoord
                            else
                           {
                               Message = "zorg dat de wachtwoorden overeen komen.";
                           }
                       }//foutafhandeling emailadres
                        else
                        {
                            Message = $"{emailAdresInput} is geen geldig emailadres \r\nbv: oudstudent@gmail.com"+ "\r\n" + "of het emailadres is al in gebruik.";
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

        public Gebruiker getCurrentUser()
        {
            return _gebruiker;
        }



        #endregion
    }
    
}