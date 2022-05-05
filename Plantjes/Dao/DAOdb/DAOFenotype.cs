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
    }
}
