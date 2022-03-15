using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOUpdatePlant
    {
        private static readonly DAOUpdatePlant instance = new DAOUpdatePlant();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOUpdatePlant()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }
        

        public static DAOUpdatePlant Instance()
        {
            return instance;
        }

        public List<UpdatePlant> GetAllUpdatePlant()
        {
            var updatePlant = context.UpdatePlants.ToList();
            return updatePlant;
        }
    }
}
