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

        //Get a list of all the FenotypeMulti types
        public List<FenotypeMulti> GetAllFenotypesMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filteren will take place in the ViewModel

            var fenoMultiList = Context.FenotypeMultis.ToList();

            return fenoMultiList;
        }

        public IQueryable<FenotypeMulti> FilterFenotypeMultiFromPlant(int selectedItem)
        {

            var selection = Context.FenotypeMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }
    }
}
