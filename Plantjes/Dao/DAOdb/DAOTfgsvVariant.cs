using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOTfgsvVariant : DAOGeneric
    {
        public DAOTfgsvVariant() : base()
        {
            //ctor
        }

        public IQueryable<TfgsvVariant> fillTfgsvVariant(int selectedItem)
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext

            //var selection = Context.TfgsvVariants.Distinct().OrderBy(s => s.Variantnaam);
            //return selection;

            if (selectedItem > 0)
            {
                var selection = Context.TfgsvVariants.Where(s => s.SoortSoortid == selectedItem).OrderBy(s => s.Variantnaam).Distinct();
                return selection;
            }
            else
            {
                var selection = Context.TfgsvVariants.Distinct().OrderBy(s => s.Variantnaam);
                return selection;
            }
        }
    }
}
