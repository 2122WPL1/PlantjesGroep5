using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOBeheerMaand
    {
        private static readonly DAOBeheerMaand instance = new DAOBeheerMaand();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOBeheerMaand()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }
        public List<BeheerMaand> FillBeheerdaad()
        {
            var selection = context.BeheerMaands.ToList();
            return selection;
        }

        public static DAOBeheerMaand Instance()
        {
            return instance;
        }

        //Get a list of all the Beheermaand types
        public List<BeheerMaand> GetBeheerMaanden()
        {
            var beheerMaanden = context.BeheerMaands.ToList();
            return beheerMaanden;
        }
    }
}
