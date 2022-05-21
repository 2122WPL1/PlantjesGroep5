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

        //makes the relay for the command on the UI-I
     
        public RelayCommand newPasswordCmd { get; set; }


        //decl and asign Ioc container alongside interfaces-I
        private SimpleIoc iocc = SimpleIoc.Default;
     
        public IloginUserService loginUserService;



        private IChangePassword _ChangePassword { get; set; }


        //constructor and asignement of interface, relaycommand and IOc-I
        public ViewModelChangePassword(IChangePassword _changePassword)
        {
           
            loginUserService = iocc.GetInstance<IloginUserService>();

            this._ChangePassword = _changePassword;

            //In the assignement of the button there must be a function, this is changepasswordClick-I



            
            newPasswordCmd = new RelayCommand(ChangePasswordClick);



        }
        //function when you press the button-I
        public void ChangePasswordClick()
        {

            //user gets searched up and gives their id in int-I
             int id = loginUserService.getCurrentUser().Id;


            //sees if the password is empty or not-I

            if (_txtPassword != null &&
                _txtControlPassword != null)
            {
                //performs the function as giving back a string for label if there's something wrong-I
                 lblErrorsPasswordText = _ChangePassword.ChangePasswordButton( _txtPassword, _txtControlPassword, id);
               

            }//foutafhandeling velden bij het registeren als alle velden leeg zijn.-I
            else
            {
                lblErrorsPasswordText = "al de velden moeten worden in gevuld ";
            }


        }


        //prop textbox passsword and checkpassword from ui-I


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
        //if there's no input, set it to null-I
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
                //let the ui know something changed-I
                OnPropertyChanged();
            }

           

        }



        //property labels to give it value of something important to convey to the user-I
        private string _lblErrorsPasswordText;
        public string lblErrorsPasswordText
        {
            get { return _lblErrorsPasswordText; } 
            
            set
            {
                    _lblErrorsPasswordText = value;
                RaisePropertyChanged("lblErrorsPasswordText");

            } }




    }
}
