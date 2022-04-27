using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOCommensalisme
    {
        private static readonly DAOCommensalisme instance = new DAOCommensalisme();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOCommensalisme()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOCommensalisme Instance()
        {
            return instance;
        }

        //Get a list of all the Commensalisme types
        public List<Commensalisme> GetAllCommensalisme()
        {
            var commensalisme = context.Commensalismes.ToList();
            return commensalisme;
        }
    }
}
