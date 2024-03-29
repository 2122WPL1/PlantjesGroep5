﻿using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels;

namespace Plantjes.ViewModels.HelpClasses
{  /*written by kenny*/
    /// <summary>
    /// Provider van viewmodels
    /// De views worden in de SimpleIoc IoC (Inversion of Control) container geregistreerd
    /// </summary>
    public class ViewModelProvider
    {
        public ViewModelProvider()
        {
            this.RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            //basisstructuur kenny, mede gebruikt door Robin
            // gebruik de default instantie (singleton van de SimpleIoc class)
            var iocc = SimpleIoc.Default;

            // haal singletons (elke keer dezelfde instantie) van de services om de viewmodels te voorzien van de nodige services(service locator)
            //ILoginUserService is leeg (interface), en hun methode is gemaakt in var loginService
            
            var loginService = iocc.GetInstance<IloginUserService>();
            var searchService = iocc.GetInstance<ISearchService>();
            var detailService = iocc.GetInstance<IDetailService>();


            var changePasswordService = iocc.GetInstance<IChangePassword>();

         
            var addPlantService = iocc.GetInstance<IAddPlantService>();


            var addAbiotiekService = iocc.GetInstance<IAddAbiotiekService>();

            var addAbiotiekMultiService = iocc.GetInstance<IAddAbiotiekMultiService>();


            // registreer de viewmodels in de IoC Container
            // factory pattern om een instantie te maken van de viewmodels
            // Dependency Injection: constructor injection: injecteer  de services in the constructors van de viewmodels;

            iocc.Register<ViewModelLogin>(() => new ViewModelLogin(loginService));
            iocc.Register<ViewModelRegister>(() => new ViewModelRegister(loginService));

            iocc.Register<ViewModelFenotype>(() => new ViewModelFenotype(detailService));
            iocc.Register<ViewModelGrooming>(() => new ViewModelGrooming(detailService));
            iocc.Register<ViewModelAbiotiek>(() => new ViewModelAbiotiek(detailService, addAbiotiekService, addAbiotiekMultiService));


            iocc.Register<ViewModelCommensalisme>(() => new ViewModelCommensalisme(detailService));
            iocc.Register<ViewModelImages>(() => new ViewModelImages(detailService));
            iocc.Register<ViewModelRequest>(() => new ViewModelRequest());

            iocc.Register<ViewModelAppearance>(() => new ViewModelAppearance(detailService));
            
            iocc.Register<ViewModelNameResult>(() => new ViewModelNameResult(searchService, loginService, addPlantService, addAbiotiekService, addAbiotiekMultiService));
            iocc.Register<ViewModelUserManagement>(() => new ViewModelUserManagement(loginService));

            //SimpleIoc.Default.Unregister<ViewModelMain>();
            iocc.Register<ViewModelBase>(() => new ViewModelBase());
            iocc.Register<ViewModelMain>(() => new ViewModelMain(loginService, searchService, changePasswordService));


            
            
            iocc.Register<ViewModelRepo>(() => new ViewModelRepo());
          
            iocc.Register<ViewModelChangePassword>(() => new ViewModelChangePassword(changePasswordService));

           


        }
    }
}