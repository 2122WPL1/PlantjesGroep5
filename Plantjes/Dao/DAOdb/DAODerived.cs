using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAODerived : DAOGeneric
    {
        public DAODerived() : base()
        {
            //ctor
        }

        public void SomeSpecialFunction()
        {
            var myplants = base.Context.Plants.ToList();
        }
    }
}
