using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOCommensalisme : DAOGeneric
    {
        public DAOCommensalisme() : base()
        {
            //ctor
        }

        //Get a list of all the Commensalisme types
        public List<Commensalisme> GetAllCommensalisme()
        {
            var commensalisme = Context.Commensalismes.ToList();
            return commensalisme;
        }

        public IQueryable<Commensalisme> FilterCommensalismeFromPlant(int selectedItem)
        {

            var selection = Context.Commensalismes.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }
    }
}
