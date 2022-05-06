using Plantjes.Dao.DAOdb;
using Plantjes.ViewModels.HelpClasses;
using System.Windows;

namespace Plantjes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //SearchService.CreateInstance();
            // services registeren
            this.Resources.Add("VMProvider", new ViewModelProvider());

            ServiceProvider.RegisterServices();
            DAOProvide.RegisterViewModels();

            // VMprovider toevoegen als "static resource" in MvvM zodat die kan worden gebruikt in de Views om
            // de ViewModels te koppelen aan de DataContext
            // instantie die over de hele applicatie kan worden gebruikt in de Views met onderstaande binding
            // <Window: ...
            // DataContext="{Binding Source={ StaticResource VMProvider }, Path=MainWindowViewModel }" 
            // ... 
            // >

            //var iocc = SimpleIoc.Default;

            //ViewModelRepo.CreateInstance();



            // de viewmodellen kunnen ook worden toegekend aan de 
            // datacontext van de view met GetInstance methode van de IoC Container
            // in de code behind van de view (yyy.xaml.cs)
            //vb. DataContext = GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<MainWindowViewModel>

        }


    }
}
