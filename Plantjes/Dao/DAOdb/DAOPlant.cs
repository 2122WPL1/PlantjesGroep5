using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XAct.Messages;

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


          
            // kijken hoeveel er zijn geselecteerd-Imran
            var plants = Context.Plants.ToList();
            return plants;
        }






        
        
        //Add new plant to database with certain qualities-Imran
        public Plant RegisterNewPlant(string naamPlant, TfgsvType typePlant, TfgsvFamilie familiePlant, TfgsvGeslacht geslachtPlant,
            TfgsvSoort soortPlant, TfgsvVariant variantPlant)
        {
            //make a new object but only if the necessary information is written -Imran

            Plant plant = new Plant();

            if (!string.IsNullOrEmpty(naamPlant))
                plant.NederlandsNaam = naamPlant;
            if (!string.IsNullOrEmpty(typePlant?.Planttypenaam))
                plant.Type = typePlant.Planttypenaam;
            //add planttypeid
                plant.TypeId = (int?)typePlant.Planttypeid;
            if (!string.IsNullOrEmpty(familiePlant?.Familienaam))
                plant.Familie = familiePlant.Familienaam;
                plant.FamilieId = (int?)familiePlant.FamileId;
            if (!string.IsNullOrEmpty(geslachtPlant?.Geslachtnaam))
                plant.Geslacht = geslachtPlant.Geslachtnaam;
                plant.GeslachtId = (int?)geslachtPlant?.GeslachtId;
            if (!string.IsNullOrEmpty(soortPlant?.Soortnaam))
                plant.Soort = soortPlant.Soortnaam;
                plant.SoortId = (int?)soortPlant?.Soortid;
            if (!string.IsNullOrEmpty(variantPlant?.Variantnaam))
                plant.Variant = variantPlant.Variantnaam;
                plant.VariantId = (int?)variantPlant?.VariantId;


            //adds the whole familie name
            plant.Fgsv = familiePlant.Familienaam + " " + geslachtPlant.Geslachtnaam + " " + soortPlant?.Soortnaam + " " + variantPlant?.Variantnaam + plant?.NederlandsNaam;



            //Add to the database and afterwards assign that same Plant object to the property-Imran


            Context.Plants.Add(plant);
            Context.SaveChanges();
            return plant;
        }




        //Important property that enables to give the ID to Abiotiek as a reference key later on-Imran
        public Plant GetPlant { get; set; }
    
    }


    

}
