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
            Plant plant = new Plant();

            if (!string.IsNullOrEmpty(naamPlant))
                plant.NederlandsNaam = naamPlant;
            if (!string.IsNullOrEmpty(typePlant?.Planttypenaam))
                plant.Type = typePlant.Planttypenaam;
            if (!string.IsNullOrEmpty(familiePlant?.Familienaam))
                plant.Familie = familiePlant.Familienaam;
            if (!string.IsNullOrEmpty(geslachtPlant?.Geslachtnaam))
                plant.Geslacht = geslachtPlant.Geslachtnaam;
            if (!string.IsNullOrEmpty(soortPlant?.Soortnaam))
                plant.Soort = soortPlant.Soortnaam;
            if (!string.IsNullOrEmpty(variantPlant?.Variantnaam))
                plant.Variant = variantPlant.Variantnaam;

            //vrder in doen
            //Incase the object is nonexistant, declare a default one with none and assign it-Imran


            //Make an object Plant to add to DataBase-Imran

            

            //Plant plant = new Plant()
            //{
            //    NederlandsNaam = naamPlant,
            //    Type = typePlant.Planttypenaam,
            //    Familie = familiePlant.Familienaam,
            //    Geslacht = geslachtPlant.Geslachtnaam,
            //    Soort = soortPlant.Soortnaam,
            //    Variant = variantPlant.Variantnaam,
            //    Fgsv = familiePlant.Familienaam + " " + geslachtPlant.Geslachtnaam + " " + soortPlant.Soortnaam + " " + variantPlant.Variantnaam,
            //    TypeId = (int?)typePlant.Planttypeid,
            //    FamilieId = (int?)familiePlant.FamileId,
            //    GeslachtId = (int?)geslachtPlant.GeslachtId,
            //    SoortId = convertSoort,
            //    VariantId = convertVariant,




            //};

            //Add to the database and afterwards assign that same Plant object to the property-Imran

            Context.Plants.Add(plant);
            Context.SaveChanges();
            return plant;
        }




        //Important property that enables to give the ID to Abiotiek as a reference key later on-Imran
        public Plant GetPlant { get; set; }
    
    }


    

}
