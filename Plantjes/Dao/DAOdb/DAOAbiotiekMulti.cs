using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOAbiotiekMulti : DAOGeneric
    {
        public DAOAbiotiekMulti() : base()
        {
            //ctor
        }

        //Get a list of all the AbiotiekMulti types
        public List<AbiotiekMulti> GetAllAbiotieksMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filteren will take place in the ViewModel

            var abioMultiList = Context.AbiotiekMultis.ToList();

            return abioMultiList;
        }
    }
}
