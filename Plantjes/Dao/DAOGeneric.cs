using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOGeneric : plantenContext
    {

        //private static readonly DAOGeneric instance = new DAOGeneric();
        //datacontext niet public maken (connectie nr db)
        protected static plantenContext Context = new plantenContext();

        //public static plantenContext Context{ get { return instance._context; } }

        public DAOGeneric()
        {
            //this._context = new plantenContext();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        //J: lists to get all types from db tables ABIOIEK
        public List<AbioBezonning> getAllTypes()
        {
            var soorten = Context.AbioBezonnings.ToList();
            return soorten;
        }
        public List<AbioVochtbehoefte> getAllTypesVochtbehoefte()
        {
            var soorten = Context.AbioVochtbehoeftes.ToList();
            return soorten;
        }
        public List<AbioVoedingsbehoefte> getAllTypesVoedingsbehoefte()
        {
            var soorten = Context.AbioVoedingsbehoeftes.ToList();
            return soorten;
        }
        public List<AbioGrondsoort> getAllTypesGrondsoort()
        {
            var soorten = Context.AbioGrondsoorts.ToList();
            return soorten;
        }
        public List<AbioReactieAntagonischeOmg> getAllTypesOmgeving()
        {
            var soorten = Context.AbioReactieAntagonischeOmgs.ToList();
            return soorten;
        }
        public List<AbioHabitat> getAllTypesHabitat()
        {
            var soorten = Context.AbioHabitats.ToList();
            return soorten;
        }


        //J: lists to get all types from db tables COMMENSALISME
        public List<CommOntwikkelsnelheid> getAllTypesOntwSnelheid()
        {
            var soorten = Context.CommOntwikkelsnelheids.ToList();
            return soorten;
        }
        public List<CommStrategie> getAllTypesStrategie()
        {
            var soorten = Context.CommStrategies.ToList();
            return soorten;
        }
        public List<CommSocialbiliteit> getAllTypesSociabiliteit()
        {
            var soorten = Context.CommSocialbiliteits.ToList();
            return soorten;
        }
        public List<CommLevensvorm> getAllTypesLevensvorm()
        {
            var soorten = Context.CommLevensvorms.ToList();
            return soorten;
        }


        //J: lists to get all types from db tables Fenotype
        public List<FenoBladgrootte> getAllTypesBladgrootte()
        {
            var soorten = Context.FenoBladgroottes.ToList();
            return soorten;
        }
        public List<FenoBladvorm> getAllTypesBladvorm()
        {
            var soorten = Context.FenoBladvorms.ToList();
            return soorten;
        }
        public List<FenoRatioBloeiBlad> getAllTypesRatioBloei()
        {
            var soorten = Context.FenoRatioBloeiBlads.ToList();
            return soorten;
        }
        public List<FenoSpruitfenologie> getAllTypesSpruitFeno()
        {
            var soorten = Context.FenoSpruitfenologies.ToList();
            return soorten;
        }
        public List<FenoBloeiwijze> getAllTypesBloeiwijze()
        {
            var soorten = Context.FenoBloeiwijzes.ToList();
            return soorten;
        }
        public List<FenoHabitu> getAllTypesFenoHabitus()
        {
            var soorten = Context.FenoHabitus.ToList();
            return soorten;
        }
        public List<FenoLevensvorm> getAllTypesFenoLevensvorm()
        {
            var soorten = Context.FenoLevensvorms.ToList();
            return soorten;
        }


    }
}
