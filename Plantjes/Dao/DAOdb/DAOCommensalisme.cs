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

        //J: lists to get all types from db tables COMMENSALISME
        public List<CommOntwikkelsnelheid> getAllTypesOntwSnelheid()
        {
            var soorten = Context.CommOntwikkelsnelheids.ToList();
            return soorten;
        }
        public List<CommStrategie> getAllTypesStrategie()
        {
            var soorten = Context.CommStrategies.ToList();
            return soorten;
        }
        public List<CommSocialbiliteit> getAllTypesSociabiliteit()
        {
            var soorten = Context.CommSocialbiliteits.ToList();
            return soorten;
        }
        public List<CommLevensvorm> getAllTypesLevensvorm()
        {
            var soorten = Context.CommLevensvorms.ToList();
            return soorten;
        }

    }
}
