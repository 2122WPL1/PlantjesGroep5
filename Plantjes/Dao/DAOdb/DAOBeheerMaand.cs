using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plantjes.Dao.DAOdb
{
    public class DAOBeheerMaand : DAOGeneric
    {
        

        public DAOBeheerMaand() : base()
        {
            //ctor
        }


        public List<BeheerMaand> FillBeheerdaad()
        {
            var selection = Context.BeheerMaands.ToList();
            return selection;
        }


        //Get a list of all the Beheermaand types 
        public List<BeheerMaand> GetBeheerMaanden()
        {
            var beheerMaanden = Context.BeheerMaands.ToList();
            return beheerMaanden;
        }

        public IQueryable<BeheerMaand> FilterBeheerMaandFromPlant(int selectedItem)
        {

            var selection = Context.BeheerMaands.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

    
    }
}
