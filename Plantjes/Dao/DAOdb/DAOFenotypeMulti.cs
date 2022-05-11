using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOFenotypeMulti : DAOGeneric
    {
        public DAOFenotypeMulti() : base()
        {
            //ctor
        }

        public IQueryable<FenotypeMulti> FilterFenotypeMultiFromPlant(int selectedItem)
        {

            var selection = Context.FenotypeMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }
    }
}
