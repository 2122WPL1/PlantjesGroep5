using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOExtraPollenWaarde : DAOGeneric
    {
        public DAOExtraPollenWaarde() : base()
        {
            //ctor
        }

        public List<ExtraPollenwaarde> FillExtraPollenwaardes()
        {
            var selection = base.Context.ExtraPollenwaardes.ToList();
            return selection;
        }

        
    }
}
