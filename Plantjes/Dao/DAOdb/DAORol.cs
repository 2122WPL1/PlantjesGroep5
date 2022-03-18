using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAORol : DAOGeneric
    {
        public DAORol() : base()
        {
            //ctor
        }



        public Rol GetRol(string Rol)
        {
            var getRol = base.Context.Rols.SingleOrDefault(g => g == g.Gebruikers);
            return getRol;

        }

        public List<Rol> GetAllRol()
        {
            var updateRol = base.Context.Rols.ToList();
            return updateRol;
        }
    }
}
