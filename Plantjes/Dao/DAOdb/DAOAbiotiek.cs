using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DAOAbiotiek
    {
        private static readonly DAOAbiotiek instance = new DAOAbiotiek();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOAbiotiek()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOAbiotiek Instance()
        {
            return instance;
        }
        //Get a list of all the Abiotiek types
        public List<Abiotiek> GetAllAbiotieks()
        {
            var abiotiek = context.Abiotieks.ToList();
            return abiotiek;
        }
    }
}
