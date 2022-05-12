using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    class DAOPlant : DAOGeneric
    {


        public DAOPlant() : base()
        {
            //ctor
        }

        //get a list of all the plants
        ///Kenny-1st year wrote this
        public List<Plant> getAllPlants()
        {
            // kijken hoeveel er zijn geselecteerd
            var plants = Context.Plants.ToList();
            return plants;
        }

      
        public void RegisterNewPlant(string naamPlant, TfgsvType typePlant, TfgsvFamilie familiePlant, TfgsvGeslacht geslachtPlant, TfgsvSoort soortPlant, TfgsvVariant variantPlant)
        {
            //het object plant wordt hier in de database toegevoegd

            int? convertSoort = (int?)soortPlant.Soortid;
            if (convertSoort == 0)
            {
                convertSoort = null;
            }


            int? convertVariant = (int?)variantPlant.VariantId;
            if (convertVariant == 0)
            {
                convertVariant = null;
            }


            var plant = new Plant()
            {
                NederlandsNaam = naamPlant,
                Type = typePlant.Planttypenaam,
                Familie = familiePlant.Familienaam,
                Geslacht = geslachtPlant.Geslachtnaam,
                Soort = soortPlant.Soortnaam,
                Variant = variantPlant.Variantnaam,
                Fgsv = familiePlant.Familienaam + " " + geslachtPlant.Geslachtnaam + " " + soortPlant.Soortnaam + " " + variantPlant.Variantnaam,
                TypeId = (int?)typePlant.Planttypeid,
                FamilieId = (int?)familiePlant.FamileId,
                GeslachtId = (int?)geslachtPlant.GeslachtId,
                SoortId = convertSoort,
                VariantId = convertVariant
                
               
                 






            };

            Context.Plants.Add(plant);
            
            Context.SaveChanges();

            
        }
    }
}
