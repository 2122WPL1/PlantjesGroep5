using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOExtraEigenschap : DAOGeneric
    {
        public DAOExtraEigenschap() : base()
        {
            //ctor
        }


        public List<ExtraEigenschap> GetAllExtraEigenschap()
        {
            var extraEigenschap = base.Context.ExtraEigenschaps.ToList();
            return extraEigenschap;
        }
    }
}
