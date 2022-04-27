using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOAbiotiekMulti
    {
        private static readonly DAOAbiotiekMulti instance = new DAOAbiotiekMulti();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOAbiotiekMulti()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOAbiotiekMulti Instance()
        {
            return instance;
        }

        //Get a list of all the AbiotiekMulti types
        public List<AbiotiekMulti> GetAllAbiotieksMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filteren will take place in the ViewModel

            var abioMultiList = context.AbiotiekMultis.ToList();

            return abioMultiList;
        }
    }
}
