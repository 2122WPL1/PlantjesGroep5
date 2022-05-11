using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DAOAbiotiek : DAOGeneric
    {
        public DAOAbiotiek() : base()
        {
            //ctor
        }

        //Get a list of all the Abiotiek types
        public List<Abiotiek> GetAllAbiotieks()
        {
            var abiotiek = Context.Abiotieks.ToList();
            return abiotiek;
        }

        public IQueryable<Abiotiek> filterAbiotiekFromPlant(int selectedItem)
        {

            var selection = Context.Abiotieks.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
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

    }
}
