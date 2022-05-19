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
        //Imran
        

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

        //gives up a query based on id-I
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

        

       
       //Add Abiotiek properties to database   - Imran
        public void AddPlantAbiotiek(long PlantId, string abioBezonning, string abioGrondsoort , string AbioControlsVochtbehoefte, string AbioControlsVoedingsbehoefte, string AbioControlsReactieAntagonischeOmg)
        {
            //Decl object Abiotiek to add to database- Imran
            Abiotiek abio = new Abiotiek()
            {

                PlantId = PlantId,
                Bezonning = abioBezonning,
                Grondsoort = abioGrondsoort,
                Vochtbehoefte = AbioControlsVochtbehoefte,
                Voedingsbehoefte = AbioControlsVoedingsbehoefte,
                AntagonischeOmgeving = AbioControlsReactieAntagonischeOmg




            };


            //Update database Abiotieks -Imran
            Context.Abiotieks.Add(abio);

            Context.SaveChanges();


        }


        

    }
}
