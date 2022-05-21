using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOTfgsvGeslacht : DAOGeneric
    {
        public DAOTfgsvGeslacht() : base()
        {
            //ctor
        }

        public IQueryable<TfgsvGeslacht> fillTfgsvGeslacht(int selectedItem)
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext
            if (selectedItem > 0)
            {
                var selection = Context.TfgsvGeslachts.Distinct().OrderBy(s => s.Geslachtnaam)
                    .Where(s => s.FamilieFamileId == selectedItem);
                return selection;
            }
            else
            {
                var selection = Context.TfgsvGeslachts.Distinct().OrderBy(s => s.Geslachtnaam);
                return selection;
            }
            //
        }
    }
}
