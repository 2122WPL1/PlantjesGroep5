using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOCommensalismeMulti : DAOGeneric
    {
        public DAOCommensalismeMulti() : base()
        {
            //ctor
        }

        public List<CommensalismeMulti> GetAllCommensalismeMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filtering will take place in the ViewModel

            var commensalismeMulti = base.Context.CommensalismeMultis.ToList();
            return commensalismeMulti;
        }

    }
}
