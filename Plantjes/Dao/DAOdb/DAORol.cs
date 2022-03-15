using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAORol
    {
        private static readonly DAORol instance = new DAORol();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAORol()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }


        public static DAORol Instance()
        {
            return instance;
        }

        public List<Rol> GetAllRol()
        {
            var updateRol = context.Rols.ToList();
            return updateRol;
        }
    }
}
