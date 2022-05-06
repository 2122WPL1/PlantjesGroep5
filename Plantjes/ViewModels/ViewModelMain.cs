using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Views.Home;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Plantjes.ViewModels
{
    public class ViewModelMain :ViewModelBase
    {
        //geschreven door kenny, adhv een voorbeeld van roy

        private SimpleIoc iocc = SimpleIoc.Default;
        private ViewModelRepo _viewModelRepo;

        private ViewModelBase _currentViewModel;
        //public RelayCommand registerCommand { get; set; }
        public MyICommand<string> mainNavigationCommand { get; set; }
        public ViewModelBase currentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public IloginUserService loginUserService;
        private ISearchService _searchService;
        public ViewModelMain(IloginUserService loginUserService, ISearchService searchService)
        {
            loggedInMessage = loginUserService.LoggedInMessage();
            this._viewModelRepo = iocc.GetInstance<ViewModelRepo>();
            this._searchService = searchService;
            this.loginUserService = loginUserService;

            //om te testen of er een rol word mee gegevenmag later mag dit weg worden gedaan
            if (loggedInMessage.Contains("Docent"))
            {
                MessageBox.Show("ingelogd als Docent");

            }
            else if (loggedInMessage.Contains("Student"))
            {
                MessageBox.Show("ingelogd als Student");
            }
            else
            {
                MessageBox.Show("ingelogd als Oudstudent");
            }

            mainNavigationCommand = new MyICommand<string>(this._onNavigationChanged);
            //  dialogService.ShowMessageBox(this, "", "");
        }
        

        private string _loggedInMessage { get; set; }
        public string loggedInMessage
        {
            get
            {
                return _loggedInMessage;
            }
            set
            {
                _loggedInMessage = value;

                RaisePropertyChanged("loggedInMessage");
            }
        }

        private void _onNavigationChanged(string userControlName)
        {
            this.currentViewModel = this._viewModelRepo.GetViewModel(userControlName);
        }
    }
}
