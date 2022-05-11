using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;

namespace Plantjes.ViewModels
{
    public class ViewModelChangePassword : ViewModelBase
    {

        //Maakt de relay command (button laten werken)
        public RelayCommand registerCommand { get; set; }



        private SimpleIoc iocc = SimpleIoc.Default;
        //ioc container - een lijst die de info van de huidige gebruiker bevat -> het haalt de huidige gebruiker hun paswoord o.a

        //Interfaces waarop de Ioc content toegepast zal worden
        public IloginUserService loginUserService;

        private IChangePassword _ChangePassword { get; set; }


        //Past wachtwoord aan en zet de interface gelijk aan de content.
        public ViewModelChangePassword(IChangePassword _changePassword)
        {
            //steek de gegevens van de gebruiker in de loginUserService
            loginUserService = iocc.GetInstance<IloginUserService>();

            this._ChangePassword = _changePassword;

            //de button werkt en op klik voert ChangePassword Click uit
            registerCommand = new RelayCommand(ChangePasswordClick);


            //!!!!!!!!!!
            //
            //
            //dit werkt ->txtPassword = loginUserService.getCurrentUser().Voornaam;
            //
            //_txtControlPassword = "test2";
            //_lblErrorsPasswordText = "label";




            newPasswordCmd = new RelayCommand(ChangePasswordClick);



        }

        public void ChangePasswordClick()
        {

            //gebruiker wordt opgehaald en zijn id wordt ingenomen voor verder gebruik
             int id = loginUserService.getCurrentUser().Id;


            //kijkt of het ingegeven password leeg is of niet

            if (_txtPassword != null &&
                _txtControlPassword != null)
            {
                //voert de functie uit en geeft het id mee
                 lblErrorsPasswordText = _ChangePassword.ChangePasswordButton( _txtPassword, _txtControlPassword, id);
               

            }//foutafhandeling velden bij het registeren als alle velden leeg zijn.
            else
            {
                lblErrorsPasswordText = "al de velden moeten worden in gevuld ";
            }



           //_lblErrorsPasswordText = "sdfsdkfdfkl,kdl,k";


        }


        //voor referentie, niet gebruikt
        //public void RegisterButtonClick()
        //{   //checken dat er iets is in gevult zo dat het programma niet crached.
        //    if (txtPassword != null &&
        //        txtControlPassword != null)
        //    {
        //        lblErrorsPasswordText = _ChangePassword.ChangePasswordButton(_txtPassword, _txtControlPassword);


        //    }//foutafhandeling velden bij het registeren als alle velden leeg zijn.
        //    else
        //    {
        //        lblErrorsPasswordText = "al de velden moeten worden in gevuld \r\n om te registeren, maar voor \r\n oudstudenten is een VivesNr niet nodig";
        //    }

        //    //Application.Current.Windows[0]?.Close();
        //}

         
        //properties (binding) van textbox van passwoord (en checken passwoord) en label (voor fouten)

        private string _txtPassword;
        public string txtPassword
        {
            get
            {
                return _txtPassword;
            }
            set
            {
                //if (txtPassword.Equals(""))
                //{
                //    _txtPassword = null;
                //}
                //else
                //{
                //    
                //}

                _txtPassword = value;

                OnPropertyChanged();
            }



        }

        private string _txtControlPassword;
        public string txtControlPassword
        {
            get { return _txtControlPassword;}

            set
            {
                if (txtControlPassword == "")
                {
                    _txtControlPassword = null;
                }
                else
                {
                    _txtControlPassword = value;
                    


                }
                OnPropertyChanged();
            }

           

        }



        
        private string _lblErrorsPasswordText;
        public string lblErrorsPasswordText
        {
            get { return _lblErrorsPasswordText; } 
            
            
            set
            {
                
                    _lblErrorsPasswordText = value;
                    
                RaisePropertyChanged("lblErrorsPasswordText");

                    //RaisePropertyChanged("lblErrorsPassword");
                  // OnPropertyChanged();

            } }



        



        public RelayCommand newPasswordCmd { get; set; }


        

        //Delen zijn er nog niet in. Dus heet is belangrijk dat de eigenlijke data kan doorgevoerd worden

    }
}
