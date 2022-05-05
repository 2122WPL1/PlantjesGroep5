using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DAOAbiotiek : DAOGeneric
    {
        public DAOAbiotiek() : base()
        {
            //ctor
        }

        //Get a list of all the Abiotiek types
        public List<Abiotiek> GetAllAbiotieks()
        {
            var abiotiek = Context.Abiotieks.ToList();
            return abiotiek;
        }

        public IQueryable<Abiotiek> filterAbiotiekFromPlant(int selectedItem)
        {

            var selection = Context.Abiotieks.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }
    }
}
