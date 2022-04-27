using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOTfgsvType
    {
        private static readonly DAOTfgsvType instance = new DAOTfgsvType();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOTfgsvType()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOTfgsvType Instance()
        {
            return instance;
        }

        public IQueryable<TfgsvType> fillTfgsvType()
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext
            var selection = context.TfgsvTypes.Distinct();
            return selection;
        }

        
    }
}
