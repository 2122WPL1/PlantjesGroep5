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

        //private static readonly DAOGeneric instance = new DAOGeneric();
        //datacontext niet public maken (connectie nr db)
        protected static plantenContext Context = new plantenContext();

        //public static plantenContext Context{ get { return instance._context; } }

        public DAOGeneric()
        {
            //this._context = new plantenContext();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }






    }
}
