using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public abstract class DAOGeneric : plantenContext
    {
        //datacontext niet public maken (connectie nr db)
        protected static plantenContext Context = new plantenContext();

        public DAOGeneric()
        {
            //
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
