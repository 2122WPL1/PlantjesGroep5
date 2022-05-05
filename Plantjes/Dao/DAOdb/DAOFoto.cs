using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOFoto : DAOGeneric
    {
        public DAOFoto() : base()
        {
            //ctor
        }

        public string GetImages(long id, string ImageCategorie)
        {
            var foto = Context.Fotos.Where(s => s.Eigenschap == ImageCategorie).FirstOrDefault(f => f.Plant == id);


            if (foto != null)
            {
                var location = foto;
                return location.UrlLocatie;
            }

            return null;
        }

        public List<Foto> GetAllFoto()
        {
            var foto = Context.Fotos.ToList();
            return foto;
        }
    }
}
