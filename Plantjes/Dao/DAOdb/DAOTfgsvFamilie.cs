using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOTfgsvFamilie
    {
        private static readonly DAOTfgsvFamilie instance = new DAOTfgsvFamilie();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOTfgsvFamilie()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOTfgsvFamilie Instance()
        {
            return instance;
        }

        public IQueryable<TfgsvFamilie> fillTfgsvFamilie(int selectedItem)
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext

            if (selectedItem > 0)
            {
                var selection = context.TfgsvFamilies.Distinct().OrderBy(s => s.Familienaam).Where(s => s.TypeTypeid == selectedItem);
                return selection;

            }
            else
            {
                var selection = context.TfgsvFamilies.Distinct().OrderBy(s => s.Familienaam);
                return selection;
            }

        }
    }
}
