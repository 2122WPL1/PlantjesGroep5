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
            var foto = base.Context.Fotos.Where(s => s.Eigenschap == ImageCategorie).SingleOrDefault(s => s.PlantId == id);


            if (foto != null)
            {
                var location = foto;
                return location.UrlLocatie;
            }

            return null;
        }

        public List<Foto> GetAllFoto()
        {
            var foto = base.Context.Fotos.ToList();
            return foto;
        }
    }
}
