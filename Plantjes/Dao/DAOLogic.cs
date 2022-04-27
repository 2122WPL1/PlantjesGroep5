﻿using System.Collections.Generic;
using System.Text;
using System.Linq;
//using Plantjes.DAL.Models;
using System.Security.Cryptography;
using Plantjes.Models;
using Plantjes.Models.Db;
using Plantjes.Dao.DAOdb;
/*comments kenny*/
//using System.Windows.Controls;

namespace Plantjes.Dao
{
    public class DAOLogic
    {
        //Robin: opzetten DAOLogic, singleton pattern

        //1.een statische private instantie instatieren die enkel kan gelezen worden.
        //deze wordt altijd teruggegeven wanneer de Instance method wordt opgeroepen
        private static readonly DAOLogic instance = new DAOLogic();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOLogic()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }
        //3.publieke methode instance die altijd kan aangeroepen worden
            //door zijn statische eigenschappen kan hij altijd aangeroepen worden 
            //zonder er een instantie van te maken
        public static DAOLogic Instance()
        {
            return instance;
        }
        /* 4.gebruik: var example = DAOLogic.Instance();
}










         */


        //search functions

        /* NARROW DOWN FUNCTIONS */

        #region Kenny's first search

        //DIT IS KENNY ZIJN CODE KAN ZIJN DAT WE DIT NOG GEBRUIKEN IN HET VOLGEND KWARTAAL.

        //A function that looks if the given list of plants contains the given string in plant.type .
        //if this is the case the plant will stay in the list.
        //if this is not the case, the plant will be deleted out of the list.
        //public void narrowDownOnType(List<Plant> listPlants, string type)
        //{
        //    foreach (Plant plant in listPlants.ToList())
        //    {           
        //        if (plant.Type != null)
        //        {
        //            var simplifyString = Simplify(plant.Geslacht.ToString());
        //            if (simplifyString.Contains(Simplify(type)) != true)
        //            {
        //                listPlants.Remove(plant);
        //            }
        //        }
        //    }
        //}

        ////A function that looks if the given list of plants contains the given string in plant.geslacht .
        ////if this is the case the plant will stay in the list.
        ////if this is not the case, the plant will be deleted out of the list.

        //public void narrowDownOnGeslacht(List<Plant> listPlants, string geslacht)
        //{
        //    foreach (Plant plant in listPlants.ToList())
        //    {
        //        if (plant.Geslacht != null)
        //        {
        //            var simplifyString = Simplify(plant.Geslacht.ToString());
        //            if (simplifyString.Contains(Simplify(geslacht)) != true)
        //            {
        //                listPlants.Remove(plant);
        //            }
        //        }
        //    }
        //}
        ////A function that looks if the given list of plants contains the given string in plant.Family .
        ////if this is the case the plant will stay in the list.
        ////if this is not the case, the plant will be deleted out of the list.
        //public void narrowDownOnFamily(List<Plant> listPlants, string Familie)
        //{
        //    foreach (Plant plant in listPlants.ToList())
        //    {
        //        if (plant.Familie != null)
        //        {
        //            var simplifyString = Simplify(plant.Familie.ToString());
        //            if (simplifyString.Contains(Simplify(Familie)) != true)
        //            {
        //                listPlants.Remove(plant);
        //            }
        //        }
        //    }
        //}
        ////A function that looks if the given list of plants contains the given string in plant.soort .
        ////if this is the case the plant will stay in the list.
        ////if this is not the case, the plant will be deleted out of the list.
        //public void narrowDownOnSoort(List<Plant> listPlants, string soort)
        //{
        //    foreach (Plant plant in listPlants.ToList())
        //    {
        //        if (plant.Soort != null)
        //        {
        //            var simplifyString = Simplify(plant.Soort.ToString());
        //            if (simplifyString.Contains(Simplify(soort)) != true)
        //            {
        //                listPlants.Remove(plant);
        //            }
        //        }
        //    }
        //}
        ////A function that looks if the given list of plants contains the given string in plant.naam .
        ////if this is the case the plant will stay in the list.
        ////if this is not the case, the plant will be deleted out of the list.
        //public void narrowDownOnName(List<Plant> listPlants, string naam)
        //{
        //    foreach (Plant plant in listPlants.ToList())
        //    {
        //        if (plant.Fgsv != null)
        //        {
        //            var simplifyString = Simplify(plant.Fgsv.ToString());
        //            if (simplifyString.Contains(Simplify(naam)) != true)
        //            {
        //                listPlants.Remove(plant);
        //            }
        //        }
        //    }
        //}
        ////A function that looks if the given list of plants contains the given string in plant.variant .
        ////if this is the case the plant will stay in the list.
        ////if this is not the case, the plant will be deleted out of the list.
        //public void narrowDownOnVariant(List<Plant> listPlants, string variant)
        //{
        //    foreach (Plant plant in listPlants.ToList())
        //    {
        //        if (plant.Variant != null)
        //        {
        //            var simplifyString = Simplify(plant.Variant.ToString());
        //            if (simplifyString.Contains(Simplify(variant)) != true)
        //            {
        //                listPlants.Remove(plant);
        //            }
        //        }
        //    }
        //}
        //A function that returns a list of plants
        //the returned list are all the plants that contain the given string in their geslacht



        //Robin: removed "static", couldn't reach context
        //public List<Plant> OnGeslacht(string geslacht)
        //{
        //    var listPlants = context.Plant.Where(p => p.Geslacht.Contains(geslacht)).ToList();
        //    return listPlants;
        //}
        ////A function that returns a list of plants
        ////the returned list are all the plants that contain the given string in their latin name
        //public List<Plant> OnName(string name)
        //{
        //    var listPlants = context.Plant.Where(p => p.Fgsv.Contains(name)).ToList();
        //    return listPlants;
        //}
        //public List<Plant> OnVariant(string variant)
        //{
        //    var listPlants = context.Plant.Where(p => p.Variant.Contains(variant)).ToList();
        //    return listPlants;
        //}
        ////A function that returns a list of plants
        ////the returned list are al the plants that contain the given string in their family
        //public List<Plant> OnFamily(string family)
        //{
        //    var listPlants = context.Plant.Where(p => p.Familie.Contains(family)).ToList();
        //    return listPlants;
        //}
        #endregion
        /* HELP FUNCTIONS */


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOBeheerMaand
        /// and applied changes in SearchService.cs
        /// </summary>
        ////get a list of all the plants.
        /////Kenny
        //public List<Plant> getAllPlants()
        //{
        //    // kijken hoeveel er zijn geselecteerd

        public string GetImages(long id , string ImageCategorie)
        {
            var foto = context.Fotos.Where(s=>s.Eigenschap == ImageCategorie).SingleOrDefault(s=> s.PlantId == id);
            
        {
            var foto = context.Fotos.Where(s=>s.Eigenschap == ImageCategorie).SingleOrDefault(s=> s.Plant == id);
            
        {
            var foto = context.Fotos.Where(s=>s.Eigenschap == ImageCategorie).SingleOrDefault(s=> s.Plant == id);
            

        //    if (foto != null)
        //    {
        //        var location = foto;
        //        return location.UrlLocatie;
        //    }

        //    return null;
        //}
        ///Robin
        #region Lists of all the plant properties with multiple values, used to display plant details


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOAbiotiek
        /// and applied changes in SearchService.cs******
        /// </summary>
        //Get a list of all the Abiotiek types
        //public List<Abiotiek> GetAllAbiotieks()
        //{
        //    var abiotiek = context.Abiotieks.ToList();
        //    return abiotiek;
        //}

        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOAbiotiekMulti
        /// and applied changes in SearchService.cs******
        /// </summary>
        //Get a list of all the AbiotiekMulti types
        //public List<AbiotiekMulti> GetAllAbiotieksMulti()
        //{
        //    //List is unfiltered, a plantId can be present multiple times
        //    //The aditional filteren will take place in the ViewModel

        //    var abioMultiList = context.AbiotiekMultis.ToList();

        //    return abioMultiList;
        //}

        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOBeheerMaand 
        /// </summary>
        //Get a list of all the Beheermaand types
        //public List<BeheerMaand> GetBeheerMaanden()
        //{
        //    var beheerMaanden = context.BeheerMaands.ToList();
        //    return beheerMaanden;
        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOCommensalisme
        /// and applied changes in SearchService.cs
        /// </summary>
        //public List<Commensalisme> GetAllCommensalisme()
        //{
        //    var commensalisme = context.Commensalismes.ToList();
        //    return commensalisme;
        //}

        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOCommensalismeMulti
        /// and applied changes in SearchService.cs
        /// </summary>
        //public List<CommensalismeMulti> GetAllCommensalismeMulti()
        //{
        //    //List is unfiltered, a plantId can be present multiple times
        //    //The aditional filtering will take place in the ViewModel

        //    var commensalismeMulti = context.CommensalismeMultis.ToList();
        //    return commensalismeMulti;
        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOExtraEigenschap
        /// and applied changes in SearchService
        /// </summary>
        //public List<ExtraEigenschap> GetAllExtraEigenschap()
        //{
        //    var extraEigenschap = context.ExtraEigenschaps.ToList();
        //    return extraEigenschap;
        //}



        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOFenotype
        /// and applied changes in SearchService
        /// </summary>
        //public List<Fenotype> GetAllFenoTypes()
        //{
        //    var fenoTypes = context.Fenotypes
        //        .ToList();
        //    return fenoTypes;
        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOFoto
        /// and applied changes in SearchService.cs
        /// </summary>
        //public List<Foto> GetAllFoto()
        //{
        //    var foto = context.Fotos.ToList();
        //    return foto;
        //}



        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOUpdatePlant
        /// and applied changes in SearchService.cs
        /// </summary>
        //public List<UpdatePlant> GetAllUpdatePlant()
        //{
        //    var updatePlant = context.UpdatePlants.ToList();
        //    return updatePlant;
        //}

        #endregion

        ///Owen, Robin, Christophe
        #region Fill Tfgsv

        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOTfgsvType
        /// </summary>
        //public IQueryable<TfgsvType> fillTfgsvType()
        //{
        //    // request List of wanted type
        //    // distinct to prevrent more than one of each type
        //    // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
        //    // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
        //    // Good way to interact with our datacontext
        //    var selection = context.TfgsvTypes.Distinct();
        //    return selection;
        //}



        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOTfgsvFamilie
        /// </summary>
        //public IQueryable<TfgsvFamilie> fillTfgsvFamilie(int selectedItem)
        //{
        //    // request List of wanted type
        //    // distinct to prevrent more than one of each type
        //    // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
        //    // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
        //    // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
        //    // Good way to interact with our datacontext

        //    if (selectedItem > 0)
        //    {
        //        var selection = context.TfgsvFamilies.Distinct().OrderBy(s => s.Familienaam).Where(s => s.TypeTypeid == selectedItem);
        //        return selection;

        //    }
        //    else
        //    {
        //        var selection = context.TfgsvFamilies.Distinct().OrderBy(s => s.Familienaam);
        //        return selection;
        //    }

        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOTfgsvGeslacht
        /// </summary>
        //public IQueryable<TfgsvGeslacht> fillTfgsvGeslacht(int selectedItem)
        //{
        //    // request List of wanted type
        //    // distinct to prevrent more than one of each type
        //    // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
        //    // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
        //    // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
        //    // Good way to interact with our datacontext
        //    if (selectedItem > 0)
        //    {
        //        var selection = context.TfgsvGeslachts.Distinct().OrderBy(s => s.Geslachtnaam)
        //            .Where(s => s.FamilieFamileId == selectedItem);
        //        return selection;
        //    }
        //    else
        //    {
        //        var selection = context.TfgsvGeslachts.Distinct().OrderBy(s => s.Geslachtnaam);
        //        return selection;
        //    }

        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOTfgsvSoort
        /// </summary>
        //public IQueryable<TfgsvSoort> fillTfgsvSoort(int selectedItem)
        //{
        //    // request List of wanted type
        //    // distinct to prevrent more than one of each type
        //    // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
        //    // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
        //    // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
        //    // Good way to interact with our datacontext
        //    if (selectedItem > 0)
        //    {
        //        var selection = context.TfgsvSoorts.Where(s => s.GeslachtGeslachtId == selectedItem).OrderBy(s => s.Soortnaam).Distinct();
        //        return selection;
        //    }
        //    else
        //    {
        //        var selection = context.TfgsvSoorts.Distinct().OrderBy(s => s.Soortnaam);
        //        return selection;
        //    }

        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOTfgsvVariant
        /// </summary>
        //public IQueryable<TfgsvVariant> fillTfgsvVariant()
        //{
        //    // request List of wanted type
        //    // distinct to prevrent more than one of each type
        //    // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
        //    // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
        //    // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
        //    // Good way to interact with our datacontext

        //    var selection = context.TfgsvVariants.Distinct().OrderBy(s => s.Variantnaam);
        //    return selection;

        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOFenotype
        /// </summary>
        //public IQueryable<Fenotype> fillFenoTypeRatioBloeiBlad()
        //{
        //    // this is NOT part of the cascade function and wil not be added as it is not needed 
        //    // request List of wanted type
        //    // distinct to prevrent more than one of each type
        //    // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter.
        //    // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
        //    // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
        //    // Good way to interact with our datacontext

        //    var selection = context.Fenotypes.Distinct().OrderBy(s => s.RatioBloeiBlad);
        //    return selection;

        //}


        #endregion

        #region FilterFromPlant
        ///Owen: op basis van basiscode Kenny, Christophe
        #region FilterFenoTypeFromPlant 

        public IQueryable<Fenotype> filterFenoTypeFromPlant(int selectedItem)
        {

            var selection = context.Fenotypes.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

        public IQueryable<FenotypeMulti> FilterFenotypeMultiFromPlant(int selectedItem)
        {

            var selection = context.FenotypeMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }
        #endregion

        #region FilterAbiotiekFromPlant
        public IQueryable<Abiotiek> filterAbiotiekFromPlant(int selectedItem)
        {

            var selection = context.Abiotieks.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

        public IQueryable<AbiotiekMulti> filterAbiotiekMultiFromPlant(int selectedItem)
        {

            var selection = context.AbiotiekMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #region FilterBeheerMaandFromPlant
        public IQueryable<BeheerMaand> FilterBeheerMaandFromPlant(int selectedItem)
        {

            var selection = context.BeheerMaands.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #region FilterCommensalismeFromPlant
        public IQueryable<Commensalisme> FilterCommensalismeFromPlant(int selectedItem)
        {

            var selection = context.Commensalismes.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

        public IQueryable<CommensalismeMulti> FilterCommensalismeMulti(int selectedItem)
        {

            var selection = context.CommensalismeMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #region FilterExtraEigenschapFromPlant
        public IQueryable<ExtraEigenschap> FilterExtraEigenschapFromPlant(int selectedItem)
        {

            var selection = context.ExtraEigenschaps.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #endregion
        //Christophe: Op basis van basiscode Owen
        #region Fill Combobox Pollenwaarde en Nectarwaarde


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOExtraPollenWaarde
        /// </summary>
        //public List<ExtraPollenwaarde> FillExtraPollenwaardes()
        //{
        //    var selection = context.ExtraPollenwaardes.ToList();
        //    return selection;
        //}

        //public List<ExtraNectarwaarde> FillExtraNectarwaardes()
        //{
        //    var selection = context.ExtraNectarwaardes.ToList();
        //    return selection;
        //}

        #endregion

        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOBeheerMaand 
        /// and applied changes in ViewModelGrooming
        /// </summary>
        //public List<BeheerMaand> FillBeheerdaad()
        //{
        //    var selection = context.BeheerMaands.ToList();
        //    return selection;
        //}


        /// <summary>
        /// Trúc put these codes in comments after wrote an apart file: DAOGebruiker 
        /// and applied changes in LoginUserService.cs
        /// </summary>
        //written by kenny
        //public Gebruiker GetGebruikerWithEmail(string userEmail)
        //{
        //    var gebruiker = context.Gebruikers.SingleOrDefault(g => g.Emailadres == userEmail);
        //    return gebruiker;

        //}
        //written by kenny
        //public void RegisterUser(string vivesNr, string firstName, string lastName, Rol rol, string emailadres, string password)
        //{
        //    var passwordBytes = Encoding.ASCII.GetBytes(password);
        //    var md5Hasher = new MD5CryptoServiceProvider();
        //    var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

        //    var gebruiker = new Gebruiker()
        //    {
        //        Vivesnr = vivesNr,
        //        Voornaam = firstName,
        //        Achternaam = lastName,
        //        Rol = rol,
        //        Emailadres = emailadres,
        //        HashPaswoord = passwordHashed
        //    };
        //    context.Gebruikers.Add(gebruiker);
        //    _ = context.SaveChanges();
        //}
        ////written by kenny
        //public List<Gebruiker> getAllGebruikers()
        //{
        //    var gebruiker = context.Gebruikers.ToList();
        //    return gebruiker;
        //}
        //written by kenny
        //public bool CheckIfEmailAlreadyExists(string email)
        //{
        //    bool result = false;
        //    if (_dao.GetGebruikerWithEmail(email) == null)
        //    {
        //        result = true;
        //    }

        //    return result;
        //}

    }


}
