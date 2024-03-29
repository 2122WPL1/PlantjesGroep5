﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models.Db;
using Plantjes.Dao.DAOdb;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Models.Classes;

namespace Plantjes.ViewModels.Services
{

    /*written by kenny and robin from an example of Roy and some help of Killian*/
    public class SearchService : ISearchService, INotifyPropertyChanged
    {
        private DAOPlant _dao;
        //private DAOPlant _daoPlant;
        private DAOFoto _daoFoto;
        private DAOAbiotiek _daoAbiotiek;
        private DAOAbiotiekMulti _daoAbiotiekMulti;
        private DAOBeheerMaand _daoBeheerMaand;
        private DAOCommensalisme _daoCommensalisme;
        private DAOCommensalismeMulti _daoCommensalismeMulti;
        private DAOFenotype _daoFenotype;
        private DAOExtraEigenschap _daoExtraEigenschap;
        private DAOTfgsvFamilie _daoTfgsvFamilie;
        private DAOTfgsvGeslacht _daoTfgsvGeslacht;
        private DAOTfgsvType _daoTfgsvType;
        private DAOTfgsvSoort _daoTfgsvSoort;
        private DAOTfgsvVariant _daoTfgsvVariant;
        private DAOFenotypeMulti _daoFenotypeMulti;

        private Plant _SelectedPlant;


        public event PropertyChangedEventHandler PropertyChanged;

        
        public SearchService()
        {
            this._dao = SimpleIoc.Default.GetInstance<DAOPlant>();
            //this._daoAbiotiek = DAOAbiotiek.Instance();

        }

        #region RelayCommandMethods
       
        
        public List<Plant> ApplyFilter(TfgsvType SelectedtType, TfgsvFamilie SelectedFamilie, TfgsvGeslacht SelectedGeslacht, TfgsvSoort SelectedSoort, TfgsvVariant SelectedVariant, string SelectedNederlandseNaam, string SelectedRatioBloeiBlad)
        {
            var listPlants = _dao.getAllPlants();

            if (SelectedtType != null)
            {

                foreach (var item in listPlants.ToList())
                {

                    if (item.TypeId != SelectedtType.Planttypeid)
                    {
                        listPlants.Remove(item);
                    }
                }
            }
            if (SelectedFamilie != null)
            {

                foreach (var item in listPlants.ToList())
                {

                    if (item.FamilieId != SelectedFamilie.FamileId)
                    {
                        listPlants.Remove(item);
                    }
                }
            }
            if (SelectedGeslacht != null)
            {

                foreach (var item in listPlants.ToList())
                {

                    if (item.GeslachtId != SelectedGeslacht.GeslachtId)
                    {
                        listPlants.Remove(item);
                    }
                }
            }
            if (SelectedSoort != null)
            {

                foreach (var item in listPlants.ToList())
                {

                    if (item.SoortId != SelectedSoort.Soortid)
                    {
                        listPlants.Remove(item);
                    }
                }
            }
            if (SelectedVariant != null)
            {

                foreach (var item in listPlants.ToList())
                {

                    if (item.VariantId != null)
                    {

                        if (item.VariantId != SelectedVariant.VariantId)
                        {

                            listPlants.Remove(item);
                        }
                    }
                    else if (item.VariantId == null)
                    {
                        listPlants.Remove(item);
                    }

                }
            }

            if (SelectedNederlandseNaam != null)
            {
                foreach (var item in listPlants.ToList())
                {

                    if (item.NederlandsNaam != null)
                    {
                        if (!item.NederlandsNaam.Contains(SelectedNederlandseNaam))
                        {
                            listPlants.Remove(item);
                        }

                    }
                    else if (item.NederlandsNaam == null)
                    {
                        listPlants.Remove(item);
                    }

                }
            }

            if (SelectedRatioBloeiBlad != null)
            {

                foreach (var item in listPlants.ToList())
                {
                    if (item.Fenotypes.Count != 0)
                    {
                        foreach (var itemFenotype in item.Fenotypes)
                        {

                            if (itemFenotype.RatioBloeiBlad != null || itemFenotype.RatioBloeiBlad != String.Empty)
                            {

                                if (itemFenotype.RatioBloeiBlad != SelectedRatioBloeiBlad)
                                {
                                    listPlants.Remove(item);
                                }
                            }
                            else
                            {
                                listPlants.Remove(item);
                            }
                        }
                    }
                    else
                    {
                        listPlants.Remove(item);
                    }

                }
            }
            return listPlants;
        }
        #endregion
        #region Fill methods

        //Simplifiy method so that the words are more presentable
        //A function that takes a string, puts it to lowercase, 
        //changes all the ' and " chars and replaces them by a space
        //next it deletes al the spaces and returns the string.

        //geschreven door kenny
        public string Simplify(string stringToSimplify)
        {                                              // sommige planten hebben geen soort maar wel een variant en dan kan je __ gebruiken om die variant te vinden
            string answer = stringToSimplify.Replace(",", "").Replace("'", "")/*.Replace("__", "")*/;
            answer = String.Concat(answer.Where(c => !Char.IsWhiteSpace(c)));
            return answer;
        }
        //geschreven door owen, aangepast door robin voor mvvm en later services
        public void fillComboBoxType(ObservableCollection<TfgsvType> cmbTypeCollection)
        {
            this._daoTfgsvType = SimpleIoc.Default.GetInstance<DAOTfgsvType>();
            var list = _daoTfgsvType.fillTfgsvType();

            foreach (var item in list)
            {
                cmbTypeCollection.Add(item);
            }
        }
        //geschreven door owen, aangepast door robin en christophe voor mvvm en later services
        public void fillComboBoxFamilie(TfgsvType selectedType, ObservableCollection<TfgsvFamilie> cmbFamilieCollection)
        {
            this._daoTfgsvFamilie = SimpleIoc.Default.GetInstance<DAOTfgsvFamilie>();
            var list = new List<TfgsvFamilie>(); /*Enumerable.Empty<TfgsvFamilie>().AsQueryable();*/

            //use the typeId, selected in the combobox to filter the list and load the remaining plant families in the family combobox
            // checking if selected type is selected to prevent null exception
            if (selectedType != null)
            {
                // Requesting te list of families 
                list = _daoTfgsvFamilie.fillTfgsvFamilie(Convert.ToInt32(selectedType.Planttypeid)).ToList();

            }
            else
            {
                // Requesting te list of families  with 0 because there is noting selected in the combobox of type.
                list = _daoTfgsvFamilie.fillTfgsvFamilie(0).ToList();
            }

            // clearing te content of te combobox of familie
            cmbFamilieCollection.Clear();
            // a list to add type that have been added to the combobox. this is used for preventing two of the same type in the combo box
            var ControleList = new List<string>();
            //adding or list to the combobox
            foreach (var item in list)
            {
                if (!ControleList.Contains(item.Familienaam))
                {
                    cmbFamilieCollection.Add(item);
                    ControleList.Add(item.Familienaam);
                }
            }
        }
        //geschreven door owen, aangepast door robin en christophe voor mvvm en later services
        public void fillComboBoxGeslacht(TfgsvFamilie selectedFamilie, ObservableCollection<TfgsvGeslacht> cmbGeslachtCollection)
        {
            this._daoTfgsvGeslacht = SimpleIoc.Default.GetInstance<DAOTfgsvGeslacht>();
            var list = Enumerable.Empty<TfgsvGeslacht>().AsQueryable();

            //use the FamilieId, selected in the combobox to filter the list and load the remaining plant geslacht in the geslacht combobox
            // checking if selected geslacht is selected to prevent null exception
            if (selectedFamilie != null)
            {
                // Requesting te list of geslacht 
                list = _daoTfgsvGeslacht.fillTfgsvGeslacht(Convert.ToInt32(selectedFamilie.FamileId));
            }
            else
            {
                // Requesting te list of Geslacht  with 0 because there is noting selected in the combobox of type.
                list = _daoTfgsvGeslacht.fillTfgsvGeslacht(0);
            }

            // clearing te content of te combobox of geslacht
            cmbGeslachtCollection.Clear();
            // a list to add type that have been added to the combobox. this is used for preventing two of the same type in the combo box
            var ControleList = new List<string>();
            //adding or list to the combobox
            foreach (var item in list)
            {
                if (!ControleList.Contains(item.Geslachtnaam))
                {
                    cmbGeslachtCollection.Add(item);
                    ControleList.Add(item.Geslachtnaam);
                }
            }
        }
        //geschreven door owen, aangepast door robin en christophe voor mvvm en later services
        public void fillComboBoxSoort(TfgsvGeslacht selectedGeslacht, ObservableCollection<TfgsvSoort> cmbSoortCollection)
        {
            this._daoTfgsvSoort = SimpleIoc.Default.GetInstance<DAOTfgsvSoort>();
            var list = Enumerable.Empty<TfgsvSoort>().AsQueryable();

            //use the GeslachtId, selected in the combobox to filter the list and load the remaining plant Soort in the Soort combobox
            // checking if selected Soort is selected to prevent null exception
            if (selectedGeslacht != null)
            {
                // Requesting te list of Soort 
                list = _daoTfgsvSoort.fillTfgsvSoort(Convert.ToInt32(selectedGeslacht.GeslachtId));
            }
            else
            {
                // Requesting te list of Soort  with 0 because there is noting selected in the combobox of type.
                list = _daoTfgsvSoort.fillTfgsvSoort(0);
            }

            // clearing te content of te combobox of Soort
            cmbSoortCollection.Clear();
            // a list to add type that have been added to the combobox. this is used for preventing two of the same type in the combo box
            var ControleList = new List<string>();
            //adding or list to the combobox
            foreach (var item in list)
            {
                if (!ControleList.Contains(item.Soortnaam))
                {
                    cmbSoortCollection.Add(item);
                    ControleList.Add(item.Soortnaam);
                }
            }
        }
        //geschreven door owen, aangepast door robin en christophe voor mvvm en later services
        //aangepast door Mathias om de cmb variant te vullen na het selecteren van een soort
        public void fillComboBoxVariant(TfgsvSoort selectedSoort, ObservableCollection<TfgsvVariant> cmbVariantCollection)
        {
            //initialiseer DAOTfgsvVariant:
            this._daoTfgsvVariant = SimpleIoc.Default.GetInstance<DAOTfgsvVariant>();
            var list = Enumerable.Empty<TfgsvVariant>().AsQueryable(); ;

            // Requesting te list of Variant  with 0 because there is noting selected in the combobox of type.
            if (selectedSoort != null)
            {
                // Requesting te list of Variant 
                list = _daoTfgsvVariant.fillTfgsvVariant(Convert.ToInt32(selectedSoort.Soortid));
            }
            else
            {
                // Requesting te list of Variant  with 0 because there is noting selected in the combobox of type.
                list = _daoTfgsvVariant.fillTfgsvVariant(0);
            }

            // clearing te content of te combobox of Variant
            cmbVariantCollection.Clear();
                // a list to add type that have been added to the combobox. this is used for preventing two of the same type in the combo box
                var ControleList = new List<string>();
                //adding or list to the combobox
                foreach (var item in list)
                {
                    if (!ControleList.Contains(item.Variantnaam))
                    {
                        ControleList.Add(item.Variantnaam);
                        Simplify(item.Variantnaam);
                        cmbVariantCollection.Add(item);
                    }
                }
            }
        //geschreven door owen, aangepast door robin en christophe voor mvvm en later services
        public void fillComboBoxRatioBloeiBlad(ObservableCollection<Fenotype> cmbRatioBladBloeiCollection)
            {
            this._daoFenotype = SimpleIoc.Default.GetInstance<DAOFenotype>();
            //not currently used in the cascade search
            //will be adjusted later (dao)
            var list = _daoFenotype.fillFenoTypeRatioBloeiBlad();
                // a list to add type that have been added to the combobox. this is used for preventing two of the same type in the combo box
                var ControleList = new List<string>();
                //adding or list to the combobox
                foreach (var item in list)
                {
                    if (!ControleList.Contains(item.RatioBloeiBlad))
                    {
                        cmbRatioBladBloeiCollection.Add(item);
                        ControleList.Add(item.RatioBloeiBlad);
                    }
                }
            }
        #endregion
        #region Fill plant details in detail screen
        /// <summary>
        /// Plant detail listbox methods, geschreven door Robin, omgezet voor de service door kenny
        /// </summary>
        /// <param name="detailsSelectedPlant"></param>
        /// <param name="SelectedPlantInResult"></param>
        public void FillDetailPlantResult(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            detailsSelectedPlant.Clear();
            //Every property of the selected plant will be added to the OC
            //Now when I bind it to the list in the xaml, they will be displayed
            if (SelectedPlantInResult != null)
            {
                //Add every available plant property to the OC
                ////start with the properties consisting of a single value              
                FillSingleValuePlantDetails(detailsSelectedPlant,SelectedPlantInResult);

                //Tables linked to Plant by PlantId
                ////Abiotiek
                FillDetailsPlantAbiotiek(detailsSelectedPlant, SelectedPlantInResult);
                ////Abiotiek_Multi
                FillDetailsPlantAbiotiekMulti(detailsSelectedPlant, SelectedPlantInResult);
                ////Beheermaand
                FillDetailsPlantBeheermaand(detailsSelectedPlant,SelectedPlantInResult);
                ////Commensalisme
                FillDetailsPlantCommensalisme(detailsSelectedPlant, SelectedPlantInResult);
                ////CommensalismeMulti
                FillDetailsPlantCommensalismeMulti(detailsSelectedPlant, SelectedPlantInResult);
                ////ExtraEigenschap
                FillExtraEigenschap(detailsSelectedPlant, SelectedPlantInResult);
                ////FenoType
                FillFenotype(detailsSelectedPlant, SelectedPlantInResult);
                //J: Fenotype Multi
                FillDetailsPlantFenotypeMulti(detailsSelectedPlant, SelectedPlantInResult);

                ////Foto
                ////UpdatePlant
            }

        }
        public void FillSingleValuePlantDetails(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            //These are single value properties and can be added to the details screen immediatly
            detailsSelectedPlant.Add("Plant Id: " + SelectedPlantInResult.PlantId);
            detailsSelectedPlant.Add("Nederlandse naam: " + SelectedPlantInResult.NederlandsNaam);
            detailsSelectedPlant.Add("Wetenschappelijke naam: " + SelectedPlantInResult.Fgsv);
            detailsSelectedPlant.Add("Type: " + SelectedPlantInResult.Type);
            detailsSelectedPlant.Add("Familie: " + SelectedPlantInResult.Familie);
            detailsSelectedPlant.Add("Geslacht: " + SelectedPlantInResult.Geslacht);
            detailsSelectedPlant.Add("Soort: " + SelectedPlantInResult.Soort);
            detailsSelectedPlant.Add("Variant: " + SelectedPlantInResult.Variant);
            detailsSelectedPlant.Add("Minimale plantdichtheid: " + SelectedPlantInResult.PlantdichtheidMin);
            detailsSelectedPlant.Add("Maximale plantdichtheid: " + SelectedPlantInResult.PlantdichtheidMax);
            detailsSelectedPlant.Add("status: " + SelectedPlantInResult.Status);
        }

        public void FillDetailsPlantAbiotiek(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Abiotiek list, then we'll need to filter that list
            ////by checking if the Abiotiek.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Abiotiek types in the detailSelectedPlant Observable Collection
            this._daoAbiotiek = SimpleIoc.Default.GetInstance<DAOAbiotiek>();
            var abioList = _daoAbiotiek.GetAllAbiotieks();

            foreach (var itemAbio in abioList)
            {
                foreach (var plantItem in SelectedPlantInResult.Abiotieks)
                {
                    if (itemAbio.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Antagonische omgeving: " + itemAbio.AntagonischeOmgeving);
                        detailsSelectedPlant.Add("Bezonning: " + itemAbio.Bezonning);
                        detailsSelectedPlant.Add("Grondsoort: " + itemAbio.Grondsoort);
                        detailsSelectedPlant.Add("Vochtbehoefte: " + itemAbio.Vochtbehoefte);
                        detailsSelectedPlant.Add("Voedingsbehoefte: " + itemAbio.Voedingsbehoefte);
                    }
                }
            }
        }
        public void FillDetailsPlantAbiotiekMulti(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Abiotiek_Multi list, then we'll need to filter that list
            ////by checking if the Abiotiek_Multi.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Abiotiek_Multi types in the detailSelectedPlant Observable Collection
            this._daoAbiotiekMulti = SimpleIoc.Default.GetInstance<DAOAbiotiekMulti>();
            var abioMultiList = _daoAbiotiekMulti.GetAllAbiotieksMulti();
            bool hasCheckedPlant;

            //bool gebruiken
            foreach (var itemAbioMulti in abioMultiList)
            {
                //A multi table contains the same PlantId multiple times because it can contain multiple properties
                //To refrain the app from showing duplicate data, I use a bool to limit the foreach to 1 run per plantId
                hasCheckedPlant = true;
                foreach (var plantItem in SelectedPlantInResult.AbiotiekMultis)
                {
                    if (hasCheckedPlant == true && itemAbioMulti.PlantId == plantItem.PlantId)
                    {
                        //EVENTUEEL 1 EIGENSCHAP-> VERSCHILLENDE WAARDES MEEGEVEN OP 1 LIJN OF ONDER ELKAAR
                        detailsSelectedPlant.Add("Abio Eigenschap: " + itemAbioMulti.Eigenschap);
                        detailsSelectedPlant.Add("Abio Waarde: " + itemAbioMulti.Waarde);
                    }
                    hasCheckedPlant = false;
                }
            }
        }

        //Table without data
        public void FillDetailsPlantBeheermaand(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an BeheerMaand list consisting of every possible property, then we'll need to filter that list
            ////by checking if the Beheermaand.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Beheermaand types in the detailSelectedPlant Observable Collection

            //initialiseer DAOBeheerMaand
            this._daoBeheerMaand = SimpleIoc.Default.GetInstance<DAOBeheerMaand>();
            ////There is currently no data in this table, but the app is prepared for when it's added.
            var beheerMaandList = _daoBeheerMaand.GetBeheerMaanden();

            foreach (var itemBeheerMaand in beheerMaandList)
            {
                foreach (var plantItem in SelectedPlantInResult.BeheerMaands)
                {
                    if (itemBeheerMaand.PlantId == plantItem.PlantId)
                    {
                        //EVENTUEEL 1 EIGENSCHAP-> VERSCHILLENDE WAARDES MEEGEVEN OP 1 LIJN OF ONDER ELKAAR
                        //BOOLS OP EEN ANDERE MANIER GEBRUIKEN?
                        detailsSelectedPlant.Add("Beheerdaad: " + itemBeheerMaand.Beheerdaad);
                        detailsSelectedPlant.Add("Januari: " + itemBeheerMaand.Jan);
                        detailsSelectedPlant.Add("Februari" + itemBeheerMaand.Feb);
                        detailsSelectedPlant.Add("Maart" + itemBeheerMaand.Mrt);
                        detailsSelectedPlant.Add("April" + itemBeheerMaand.Apr);
                        detailsSelectedPlant.Add("Mei" + itemBeheerMaand.Mei);
                        detailsSelectedPlant.Add("Juni" + itemBeheerMaand.Jun);
                        detailsSelectedPlant.Add("Juli" + itemBeheerMaand.Jul);
                        detailsSelectedPlant.Add("Augustus" + itemBeheerMaand.Aug);
                        detailsSelectedPlant.Add("September" + itemBeheerMaand.Sept);
                        detailsSelectedPlant.Add("Oktober" + itemBeheerMaand.Okt);
                        detailsSelectedPlant.Add("November" + itemBeheerMaand.Nov);
                        detailsSelectedPlant.Add("December" + itemBeheerMaand.Dec);
                    }
                }
            }
        }

        //Table without data
        public void FillDetailsPlantCommensalisme(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Commensalisme list consisting of every possible property, then we'll need to filter that list
            ////by checking if the Commensalisme.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Commensalisme types in the detailSelectedPlant Observable Collection
            this._daoCommensalisme = SimpleIoc.Default.GetInstance<DAOCommensalisme>();
            ////There is currently no data in this table, but the app is prepared for when it's added.
            var commensalismeList = _daoCommensalisme.GetAllCommensalisme();

            foreach (var itemCommensalisme in commensalismeList)
            {
                foreach (var plantItem in SelectedPlantInResult.Commensalismes)
                {
                    if (itemCommensalisme.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Ontwikkelsnelheid: " + itemCommensalisme.Ontwikkelsnelheid);
                        detailsSelectedPlant.Add("Strategie" + itemCommensalisme.Strategie);
                    }
                }
            }
        }

        public void FillDetailsPlantCommensalismeMulti(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an CommensalismeMulti list consisting of every possible property, then we'll need to filter that list
            ////by checking if the CommensalismeMulti.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining CommensalismeMulti types in the detailSelectedPlant Observable Collection
            this._daoCommensalismeMulti = SimpleIoc.Default.GetInstance<DAOCommensalismeMulti>();
            ////There is currently no data in this table, but the app is prepared for when it's added.
            var commensalismeMultiList = _daoCommensalismeMulti.GetAllCommensalismeMulti();
            bool hasCheckedPlant;

            foreach (var itemCommensalismeMulti in commensalismeMultiList)
            {    //A multi table contains the same PlantId multiple times because it can contain multiple properties
                //To refrain the app from showing duplicate data, I use a bool to limit the foreach to 1 run per plantId
                hasCheckedPlant = true;
                foreach (var plantItem in SelectedPlantInResult.Commensalismes)
                {
                    if (itemCommensalismeMulti.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Commensalisme eigenschap: " + itemCommensalismeMulti.Eigenschap);
                        detailsSelectedPlant.Add("Commensalisme waarde: " + itemCommensalismeMulti.Waarde);
                    }
                    hasCheckedPlant = false;
                }
            }
        }

        public void FillExtraEigenschap(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an ExtraEigenschap list, then we'll need to filter that list
            ////by checking if the ExtraEigenschap.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining ExtraEigenschap types in the detailSelectedPlant Observable Collection
            this._daoExtraEigenschap = SimpleIoc.Default.GetInstance<DAOExtraEigenschap>();
            var extraEigenschapList = _daoExtraEigenschap.GetAllExtraEigenschap();

            foreach (var itemExtraEigenschap in extraEigenschapList)
            {
                foreach (var plantItem in SelectedPlantInResult.ExtraEigenschaps)
                {
                    if (itemExtraEigenschap.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Nectarwaarde: " + itemExtraEigenschap.Nectarwaarde);
                        detailsSelectedPlant.Add("Pollenwaarde: " + itemExtraEigenschap.Pollenwaarde);

                        //Make sure the output is in dutch
                        if (itemExtraEigenschap.Bijvriendelijke == true)
                        {
                            detailsSelectedPlant.Add("Bijvriendelijk: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Bijvriendelijk: Nee");
                        }
                        if (itemExtraEigenschap.Eetbaar == true)
                        {
                            detailsSelectedPlant.Add("Eetbaar: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Eetbaar: Nee");
                        }
                        if (itemExtraEigenschap.Geurend == true)
                        {
                            detailsSelectedPlant.Add("Geurend: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Geurend: Nee");
                        }
                        if (itemExtraEigenschap.Kruidgebruik == true)
                        {
                            detailsSelectedPlant.Add("Kruidgebruik: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Kruidgebruik: Nee");
                        }
                        if (itemExtraEigenschap.Vlindervriendelijk == true)
                        {
                            detailsSelectedPlant.Add("Vlindervriendelijk: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Vlindervriendelijk: Nee");
                        }
                        if (itemExtraEigenschap.Vorstgevoelig == true)
                        {
                            detailsSelectedPlant.Add("Vorstgevoelig: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Vorstgevoelig: Nee");
                        }

                    }
                }
            }
        }

        public void FillFenotype(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Fenotype list, then we'll need to filter that list
            ////by checking if the Fenotype.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Fenotype types in the detailSelectedPlant Observable Collection
            this._daoFenotype = SimpleIoc.Default.GetInstance<DAOFenotype>();
            var fenoTypeList = _daoFenotype.GetAllFenoTypes();

            foreach (var itemFenotype in fenoTypeList)
            {
                foreach (var item in SelectedPlantInResult.Fenotypes)
                {
                    if (item.PlantId == itemFenotype.PlantId)
                    {
                        //FILTER DE DUBBELE PLANTEN UIT DE DATABASE

                        detailsSelectedPlant.Add("Bladgrootte: " + itemFenotype.Bladgrootte.ToString());
                        detailsSelectedPlant.Add("Bladvorm: " + itemFenotype.Bladvorm);
                        detailsSelectedPlant.Add("BloeiVorm: " + itemFenotype.Bloeiwijze);
                        detailsSelectedPlant.Add("Habitus: " + itemFenotype.Habitus);
                        detailsSelectedPlant.Add("Levensvorm: " + itemFenotype.Levensvorm);
                        detailsSelectedPlant.Add("Spruitfenologie: " + itemFenotype.Spruitfenologie);
                        detailsSelectedPlant.Add("Ratio blad/bloei: " + itemFenotype.RatioBloeiBlad);
                    }
                }
            }
        }


        public void FillDetailsPlantFenotypeMulti(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Abiotiek_Multi list, then we'll need to filter that list
            ////by checking if the Abiotiek_Multi.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Abiotiek_Multi types in the detailSelectedPlant Observable Collection
            this._daoFenotypeMulti = SimpleIoc.Default.GetInstance<DAOFenotypeMulti>();
            var fenoMultiList = _daoFenotypeMulti.GetAllFenotypesMulti();
            bool hasCheckedPlant;

            //bool gebruiken
            foreach (var itemFenoMulti in fenoMultiList)
            {
                //A multi table contains the same PlantId multiple times because it can contain multiple properties
                //To refrain the app from showing duplicate data, I use a bool to limit the foreach to 1 run per plantId
                hasCheckedPlant = true;
                foreach (var plantItem in SelectedPlantInResult.FenotypeMultis)
                {
                    if (hasCheckedPlant == true && itemFenoMulti.PlantId == plantItem.PlantId)
                    {
                        //EVENTUEEL 1 EIGENSCHAP-> VERSCHILLENDE WAARDES MEEGEVEN OP 1 LIJN OF ONDER ELKAAR
                        detailsSelectedPlant.Add("Feno Eigenschap: " + itemFenoMulti.Eigenschap);
                        detailsSelectedPlant.Add("Feno Waarde: " + itemFenoMulti.Waarde);
                    }
                    hasCheckedPlant = false;
                }
            }
        }
        #endregion

        //geschreven door owen
        //omgezet voor de service door kenny
        public ImageSource GetImageLocation(string ImageCatogrie, Plant SelectedPlantInResult)
        {
            //initialise DAOFoto
            this._daoFoto = SimpleIoc.Default.GetInstance<DAOFoto>();
            // Request location of the image
            string location = "";
            if (SelectedPlantInResult != null)
            {
                location = _daoFoto.GetImages(SelectedPlantInResult.PlantId, ImageCatogrie);
            }

            if (location != null)
            {
                if (location != "")
                {
                    //Converting it to a bitmap image. This makes it possible to also have online image.
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(location, UriKind.Absolute);
                    bitmap.EndInit();
                    return bitmap;
                }
            }

            return null;
        }

        public LoginResult CheckCredentials(string userNameInput, string passwordInput)
        {
            throw new NotImplementedException();
        }

        public string RegisterButton(string vivesNrInput, string lastNameInput, string firstNameInput, string emailAdresInput, string passwordInput, string passwordRepeatInput, int rolInput)
        {
            throw new NotImplementedException();
        }

        public void setSelectedPlant(Plant p)
        {
            _SelectedPlant = p;
        }

        public Plant getSelectedPlant() { return _SelectedPlant; }
    }
}