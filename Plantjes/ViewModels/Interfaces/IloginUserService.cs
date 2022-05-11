using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using System.Collections.ObjectModel;

namespace Plantjes.ViewModels.Interfaces
{/*written by kenny*/
    public interface IloginUserService
    {
        void fillComboBoxRol(ObservableCollection<Rol> cmbRolCollection);
        LoginResult CheckCredentials(string userNameInput, string passwordInput);
        string RegisterButton(string vivesNrInput, string lastNameInput,
            string firstNameInput, string emailAdresInput,
            string passwordInput, string passwordRepeatInput, int cmbRolCollection);
        string LoggedInMessage();

        Gebruiker getCurrentUser();

    }
}