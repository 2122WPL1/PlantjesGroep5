using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao.DAOdb;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels
{
    public class ViewModelUserManagement : ViewModelBase
    {
        private DAOProvide _dao;

        private static SimpleIoc iocc = SimpleIoc.Default;
        private IloginUserService _loginUserService = iocc.GetInstance<IloginUserService>();

        public ViewModelUserManagement(IloginUserService loginUserService)
        {
            this._loginUserService = loginUserService;
            this._dao = SimpleIoc.Default.GetInstance<DAOProvide>();

            //ICommands
            ////These will be used to bind our buttons in the xaml and to give them functionality
            addStudentCommand = new RelayCommand(InsertStudentsClick);
        }


        //RelayCommands
        public RelayCommand addStudentCommand { get; set; }

        //AddStudent - variables:
        private string _selectedVivesnr;
        private string _selectedFirstname;
        private string _selectedLastname;
        private int _selectedRolid;
        private string _selectedEmail;
        private byte[] _selectedPassword;

        //AddStudent - properties:
        public void InsertStudentsClick()
        {
            _loginUserService.InsertStudents(SelectedvivesNr, Selectedfirstname, Selectedlastname, Selectedrolid, Selectedemail, Selectedpassword);
        }

        private string SelectedvivesNr
        {
            get { return _selectedVivesnr; }
            set
            {
                if (_selectedVivesnr == "")
                {
                    _selectedVivesnr = null;
                }
                else
                {
                    _selectedVivesnr = value;
                }

                OnPropertyChanged();
            }
        }

        private string Selectedfirstname
        {
            get { return _selectedFirstname; }
            set
            {
                if (_selectedFirstname == "")
                {
                    _selectedFirstname = null;
                }
                else
                {
                    _selectedFirstname = value;
                }

                OnPropertyChanged();
            }
        }

        private string Selectedlastname
        {
            get { return _selectedLastname; }
            set
            {
                if (_selectedLastname == "")
                {
                    _selectedLastname = null;
                }
                else
                {
                    _selectedLastname = value;
                }

                OnPropertyChanged();
            }
        }

        private int Selectedrolid
        {
            get { return _selectedRolid; }
            set
            {
                if (_selectedRolid == null)
                {
                    _selectedRolid = -1; //it was here null
                }
                else
                {
                    _selectedRolid = value;
                }

                OnPropertyChanged();
            }
        }

        private string Selectedemail
        {
            get { return _selectedEmail; }
            set
            {
                if (_selectedEmail == "")
                {
                    _selectedEmail = null;
                }
                else
                {
                    _selectedEmail = value;
                }

                OnPropertyChanged();
            }
        }

        private byte[] Selectedpassword
        {
            get { return _selectedPassword; }
            set
            {
                if (_selectedPassword == null)
                {
                    _selectedPassword = null;
                }
                else
                {
                    _selectedPassword = value;
                }

                OnPropertyChanged();
            }
        }


        
    }
        
}
