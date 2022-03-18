using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOExtraNectarWaarde : DAOGeneric
    {
        public DAOExtraNectarWaarde() : base()
        {
            //ctor
        }

        public List<ExtraNectarwaarde> FillExtraNectarwaardes()
        {
            var selection = context.ExtraNectarwaardes.ToList();
            return selection;
        }
    }
}
