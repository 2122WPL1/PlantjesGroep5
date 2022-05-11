﻿using GalaSoft.MvvmLight.Command;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models.Classes;
using Plantjes.Models.Enums;
using Plantjes.Views.Home;
using System.Windows;
//written by kenny
namespace Plantjes.ViewModels
{
    public class ViewModelLogin : ViewModelBase
    {
        private IloginUserService _loginService { get; }
        public RelayCommand loginCommand { get; set; }
       //public RelayCommand cancelCommand { get; set; }
       // public RelayCommand registerCommand { get; set; }

        private string _userNameInput;
        private string _passwordInput;
        private string _errorMessage;
        private string _loggedInMessage;

        public ViewModelLogin(IloginUserService loginUserService)
        {

            this._loginService = loginUserService;
            loginCommand = new RelayCommand(LoginButtonClick);
            //cancelCommand = new RelayCommand(CancelButton);
            //registerCommand = new RelayCommand(RegisterButtonView);
        }
        //public void RegisterButtonView()
        //{
        //    RegisterWindow registerWindow = new RegisterWindow();
        //    registerWindow.Show();
        //    Application.Current.Windows[0]?.Close();
        //}
        //public void CancelButton()
        //{
        //    Application.Current.Shutdown();
        //}

        private void LoginButtonClick()
        {   //checkt of er wel iets is ingegeven bij username
            if (!string.IsNullOrWhiteSpace(userNameInput))
            {   //checkt of er wel iets is ingegeven bij password
                if (!string.IsNullOrWhiteSpace(passwordInput))
                {
                    LoginResult loginResult = _loginService.CheckCredentials(userNameInput, passwordInput);

                    if (loginResult.loginStatus == LoginStatus.LoggedIn)
                    {
                        //  loggedInMessage = _loginService.LoggedInMessage(userNameInput);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Application.Current.Windows[0]?.Close();
                    }
                    else
                    {
                        errorMessage = loginResult.errorMessage;
                    }
                }//foutafhandeling voor password
                else
                {
                    errorMessage = "wachtwoord invullen";
                }
            }//foutafhandeling voor de gebruikers naam
            else
            {
                errorMessage = "gebruikersnaam invullen";
            }
        }

        public string errorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;

                RaisePropertyChanged("errorMessage");
            }
        }
     
        public string userNameInput
        {
            get
            {
                return _userNameInput;
            }
            set
            {
                _userNameInput = value;
                OnPropertyChanged();
            }
        }

        public string passwordInput
        {
            get
            {
                return _passwordInput;
            }
            set
            {
                _passwordInput = value;
                OnPropertyChanged();
            }


        }

        

    }
}
