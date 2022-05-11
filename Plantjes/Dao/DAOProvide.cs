using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOProvide
    {
        public static void RegisterViewModels()
        {
            //basisstructuur kenny, mede gebruikt door Robin
            // gebruik de default instantie (singleton van de SimpleIoc class)
            var iocc = SimpleIoc.Default;

            // registreer de viewmodels in de IoC Container
            // factory pattern om een instantie te maken van de viewmodels
            // Dependency Injection: constructor injection: injecteer  de services in the constructors van de viewmodels;

            //Register = grote Singleton
            iocc.Register<DAOAbiotiek>(() => new DAOAbiotiek(/*lege ctor*/));
            iocc.Register<DAOGebruiker>(() => new DAOGebruiker());
            iocc.Register<DAOAbiotiekMulti>(() => new DAOAbiotiekMulti());
            iocc.Register<DAOBeheerMaand>(() => new DAOBeheerMaand());
            iocc.Register<DAOCommensalisme>(() => new DAOCommensalisme());
            iocc.Register<DAOCommensalismeMulti>(() => new DAOCommensalismeMulti());
            iocc.Register<DAOExtra>(() => new DAOExtra());
            iocc.Register<DAOExtraEigenschap>(() => new DAOExtraEigenschap());
            iocc.Register<DAORol>(()=>new DAORol());

            iocc.Register<DAOExtraNectarWaarde>(() => new DAOExtraNectarWaarde());
            iocc.Register<DAOExtraPollenWaarde>(() => new DAOExtraPollenWaarde());
            iocc.Register<DAOFenotype>(() => new DAOFenotype());
            iocc.Register<DAOFoto>(() => new DAOFoto());
            iocc.Register<DAOPlant>(() => new DAOPlant());
            iocc.Register<DAOTfgsvFamilie>(() => new DAOTfgsvFamilie());
            iocc.Register<DAOTfgsvGeslacht>(() => new DAOTfgsvGeslacht());
            iocc.Register<DAOTfgsvSoort>(() => new DAOTfgsvSoort());
            iocc.Register<DAOTfgsvType>(() => new DAOTfgsvType());
            iocc.Register<DAOTfgsvVariant>(() => new DAOTfgsvVariant());
            iocc.Register<DAOUpdatePlant>(() => new DAOUpdatePlant());

            iocc.Register<DAONieuwWachtwoord>(()=> new DAONieuwWachtwoord());

            //Voor Rol later:
            //iocc.Register<DAORol>(() => new DAORol());
        }
    }
}
