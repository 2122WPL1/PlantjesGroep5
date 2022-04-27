using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOCommensalismeMulti
    {
        private static readonly DAOCommensalismeMulti instance = new DAOCommensalismeMulti();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOCommensalismeMulti()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOCommensalismeMulti Instance()
        {
            return instance;
        }

        public List<CommensalismeMulti> GetAllCommensalismeMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filtering will take place in the ViewModel

            var commensalismeMulti = context.CommensalismeMultis.ToList();
            return commensalismeMulti;
        }

    }
}
