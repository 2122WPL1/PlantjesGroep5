using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOGeneric : plantenContext
    {
        private static readonly DAOGeneric instance = new DAOGeneric();

        private plantenContext _context;

        public plantenContext Context{ get { return instance._context; } }

        public DAOGeneric()
        {
            this._context = new plantenContext();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

    }
}
