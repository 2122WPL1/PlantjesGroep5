using GalaSoft.MvvmLight.Command;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Views.Home;
using System.Collections.ObjectModel;
using System.Windows;

namespace Plantjes.ViewModels
{
    //written by kenny test
    public class ViewModelRegister : ViewModelBase
    {
        private IloginUserService _loginService { get; set; }
        public RelayCommand registerCommand { get; set; }
        public RelayCommand backCommand { get; set; }
        public ObservableCollection<Rol> cmbRols { get; set; }
        public ViewModelRegister(IloginUserService loginUserService)
        {
            this._loginService = loginUserService;
            cmbRols = new ObservableCollection<Rol>();
            registerCommand = new RelayCommand(RegisterButtonClick);
            backCommand = new RelayCommand(BackButtonClick);
            fillComboboxe();
        }

        public void fillComboboxe()
        {
            _loginService.fillComboBoxRol(cmbRols);
        }

        public void BackButtonClick()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.Windows[0]?.Close();
        }
        public void RegisterButtonClick()
        {   //gemaakt door Mathias
            //checken dat er iets is in gevult zo dat het programma niet crached.
            if (firstNameInput != null &&
                lastNameInput != null &&
                //SelectedRol != null &&
                emailAdresInput != null &&
                passwordInput != null &&
                passwordRepeatInput != null)
            {
                errorMessage = _loginService.RegisterButton(vivesNrInput, lastNameInput,
                firstNameInput, emailAdresInput,
                passwordInput, passwordRepeatInput, SelectedRol);
                
            }//foutafhandeling velden bij het registeren als alle velden leeg zijn.
            else
            {
                errorMessage = "al de velden moeten worden in gevuld \r\n om te registeren, maar voor \r\n oudstudenten is een VivesNr niet nodig";
            }
            
            //Application.Current.Windows[0]?.Close();
        }
        
        #region MVVM TextFieldsBinding
        private string _vivesNrInput;
        private string _firstNameInput;
        private string _lastNameInput;
        private string _emailAdresInput;
        private string _passwordInput;
        private string _passwordRepeatInput;
        private int _SelectedRol;
        private string _errorMessage;

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
        public string vivesNrInput
        {
            get
            {
                return _vivesNrInput;
            }
            set
            {
                _vivesNrInput = value;
                OnPropertyChanged();
            }
        }

        public string firstNameInput
        {
            get
            {
                return _firstNameInput;
            }
            set
            {
                _firstNameInput = value;
                OnPropertyChanged();
            }
        }
        public string lastNameInput
        {
            get
            {
                return _lastNameInput;
            }
            set
            {
                _lastNameInput = value;
                OnPropertyChanged();
            }
        }
        public string emailAdresInput
        {
            get
            {
                return _emailAdresInput;
            }
            set
            {
                _emailAdresInput = value;
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
        public string passwordRepeatInput
        {
            get
            {
                return _passwordRepeatInput;
            }
            set
            {
                _passwordRepeatInput = value;
                OnPropertyChanged();
            }
        }
        public int SelectedRol
        {
            get
            {
                return _SelectedRol;
            }
            set
            {
                _SelectedRol = value;
                OnPropertyChanged();
            }
        }
        #endregion


      

}
}

