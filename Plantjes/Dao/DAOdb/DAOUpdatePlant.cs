using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOUpdatePlant : DAOGeneric
    {
        public DAOUpdatePlant() : base()
        {
            //ctor
        }

        public List<UpdatePlant> GetAllUpdatePlant()
        {
            var updatePlant = base.Context.UpdatePlants.ToList();
            return updatePlant;
        }
    }
}
