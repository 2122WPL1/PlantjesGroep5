using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    class DAOPlant : DAOGeneric
    {
        private readonly plantenContext context;

        public DAOPlant() : base()
        {
            //ctor
            this.context = new plantenContext();
        }

        //get a list of all the plants
        ///Kenny-1st year wrote this
        public List<Plant> getAllPlants()
        {
            // kijken hoeveel er zijn geselecteerd
            var plants = Context.Plants.ToList();

            var b = 1;
            var a = 1 / (b - b);
            return plants;
        }
    }
}
