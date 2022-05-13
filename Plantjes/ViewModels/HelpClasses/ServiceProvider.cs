using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels.HelpClasses
{
    /*written by kenny*/
    public class ServiceProvider
    {
        public static void RegisterServices()
        {
            // basisstructuur kenny, mede gebruikt door Robin

            // de Default instantie (singleton) van de class SimpleIOC container
            // gebruiken als container voor de services.
            SimpleIoc iocc = SimpleIoc.Default;

            // registreren van utility services
            //als ILoginService opgrroepen dan is ook gelinkt aan LoginUserService: - Trúc
            iocc.Register<IloginUserService, LoginUserService>();
            iocc.Register<ISearchService, SearchService>();
            iocc.Register<IDetailService, DetailService>();
            iocc.Register<IChangePassword, ChangePasswordService>();

            //Register services addplant and addbiotic - Imran

            iocc.Register<IAddPlantService, AddPlantService>();

            iocc.Register<IAddAbiotiekService, AddBiotiekService>();


        }
    }
}