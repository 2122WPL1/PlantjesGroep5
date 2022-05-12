using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
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
        public MyICommand<string> mainNavigationCommand { get; set; }
        public ViewModelBase currentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public IloginUserService loginUserService;
        private ISearchService _searchService;
        private IChangePassword _changePassword;


        //ZIjn deze delen wel belangrijk genoeg?
        public ViewModelMain(IloginUserService loginUserService, ISearchService searchService, IChangePassword changePasswordService)
        {
            loggedInMessage = loginUserService.LoggedInMessage();
            this._viewModelRepo = iocc.GetInstance<ViewModelRepo>();
            this._searchService = searchService;
            this.loginUserService = loginUserService;

            // written by Mathias
            //if the user is a docent then it wil make the button visible
            if (loggedInMessage.Contains("Docent"))
            {
                btnVisible = "Visible";
            }
            else 
            {
                btnVisible = "Hidden";
            }
            
            //changepassword
            this._changePassword = changePasswordService;

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

        // writen by Mathias
        private string _btnVisible { get; set; }
        public string btnVisible
        {
            get
            {
                return _btnVisible;
            }
            set
            {
                _btnVisible = value;
                RaisePropertyChanged("btnVisible");
            }
        }

        private void _onNavigationChanged(string userControlName)
        {
            this.currentViewModel = this._viewModelRepo.GetViewModel(userControlName);
            this.currentViewModel.Load();
        }
    }
}
