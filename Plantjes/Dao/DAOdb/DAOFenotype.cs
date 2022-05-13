using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOFenotype : DAOGeneric
    {
        public DAOFenotype() : base()
        {
            //ctor
        }

        public List<Fenotype> GetAllFenoTypes()
        {
            var fenoTypes = Context.Fenotypes.ToList();
            return fenoTypes;
        }

        public IQueryable<Fenotype> fillFenoTypeRatioBloeiBlad()
        {
            // this is NOT part of the cascade function and wil not be added as it is not needed 
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter.
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext

            var selection = Context.Fenotypes.Distinct().OrderBy(s => s.RatioBloeiBlad);
            return selection;

        }

        public IQueryable<Fenotype> filterFenoTypeFromPlant(int selectedItem)
        {

            var selection = Context.Fenotypes.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
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

        public List<FenoKleur> getAllTypesKleur()
        {
            var soorten = Context.FenoKleurs.ToList();
            return soorten;
        }
        //written by Mathias
        //this is used to add the fenotypes when making a new plant
        public void AddPlantFenotype(long PlantId, int fenoBladgrootte, string fenoBladvorm, string fenoRatioBloeiBlad, string fenoSpruitfenologie, string fenoBloeiwijze, string fenoHabitus, string fenoLevensvorm)
        {


            Fenotype feno = new Fenotype()
            {

                PlantId = PlantId,
                Bladgrootte = fenoBladgrootte,
                Bladvorm = fenoBladvorm,
                RatioBloeiBlad = fenoRatioBloeiBlad,
                Spruitfenologie = fenoSpruitfenologie,
                Bloeiwijze = fenoBloeiwijze,
                Habitus = fenoHabitus,
                Levensvorm = fenoLevensvorm

            };

            Context.Fenotypes.Add(feno);

            Context.SaveChanges();

        }


    }
}
