using Plantjes.Models.Classes;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels.Interfaces
{/*written by kenny*/
    public interface IloginUserService
    {
        LoginResult CheckCredentials(string userNameInput, string passwordInput);
        string RegisterButton(string vivesNrInput, string lastNameInput,
            string firstNameInput, string emailAdresInput,
            string passwordInput, string passwordRepeatInput, Rol rolInput);
        string LoggedInMessage();

    }
}