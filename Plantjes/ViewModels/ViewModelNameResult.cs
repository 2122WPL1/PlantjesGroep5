using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models;
using Plantjes.Models.Db;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.ViewModels
{
    public class ViewModelNameResult : ViewModelBase
    {
        //private ServiceProvider _serviceProvider;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();
        public IloginUserService loginUserService;

        public IAddPlantService addPlantService;

        //addbiotiek

        private IAddAbiotiekService _addAbiotiekService = iocc.GetInstance<IAddAbiotiekService>();




        public ViewModelNameResult(ISearchService searchService, IloginUserService loginUserService, IAddPlantService addPlantService, IAddAbiotiekService addBiotiekService)
        {
            _addAbiotiekService = addBiotiekService;


            //loggedInMessage is used to see which user is logt in (docent, student, oldstudent)
            loggedInMessage = loginUserService.LoggedInMessage();
            this._searchService = searchService;
            //_searchService = new SearchService();

            //add the addplantservice
            this.addPlantService = addPlantService;


            //writen by Mathias
            // hids the button if a oldstudent is logt in
            if (loggedInMessage.Contains("Oudstudent"))
            {
                btnVisible = "Hidden";
            }
            else 
            {
                btnVisible = "Visible";
            }

            //Observable Collections 
            //Observable collections to fill with the necessary objects to show in the comboboxes
            cmbTypes = new ObservableCollection<TfgsvType>();
            cmbFamilies = new ObservableCollection<TfgsvFamilie>();
            cmbGeslacht = new ObservableCollection<TfgsvGeslacht>();
            cmbSoort = new ObservableCollection<TfgsvSoort>();
            cmbVariant = new ObservableCollection<TfgsvVariant>();
            cmbRatioBladBloei = new ObservableCollection<Fenotype>();

            //Observable Collections to bind to listboxes
            filteredPlantResults = new ObservableCollection<Plant>();
            detailsSelectedPlant = new ObservableCollection<string>();

            //ICommands
            //These will be used to bind our buttons in the xaml and to give them functionality
            SearchCommand = new RelayCommand(ApplyFilterClick);
            ResetCommand = new RelayCommand(ResetClick);
            AddPlantCommand = new RelayCommand(AddPlantClick);
            //These comboboxes will already be filled with data on startup
            fillComboboxes();
        }

        //written by kenny (region)
        #region tussenFunctie om de comboboxen te vullen voor knoppen met parameters

        public void fillComboboxes()
        {
            _searchService.fillComboBoxType(cmbTypes);
            _searchService.fillComboBoxFamilie(SelectedType, cmbFamilies);
            _searchService.fillComboBoxGeslacht(SelectedFamilie, cmbGeslacht);
            _searchService.fillComboBoxSoort(SelectedGeslacht, cmbSoort);
            _searchService.fillComboBoxVariant(SelectedSoort, cmbVariant);
            _searchService.fillComboBoxRatioBloeiBlad(cmbRatioBladBloei);
        }

        public void ResetClick()
        {

            filteredPlantResults.Clear();

            cmbTypes.Clear();
            cmbFamilies.Clear();
            cmbGeslacht.Clear();
            cmbSoort.Clear();
            cmbVariant.Clear();
            cmbRatioBladBloei.Clear();
            SelectedNederlandseNaam = null;

            fillComboboxes();

        }

        

        public void ApplyFilterClick()
        {
            filteredPlantResults.Clear();
            var listPlants = this._searchService.ApplyFilter(SelectedType, SelectedFamilie, SelectedGeslacht,
                SelectedSoort, SelectedVariant, SelectedNederlandseNaam, SelectedRatioBloeiBlad);
            foreach (var item in listPlants)
            {
                filteredPlantResults.Add(item);
            }
        }


        public void AddPlantClick()
        {


            //MessageBox.Show(cmbVariantText);
            //variant kan alleen bestaan als hij opkomt in bepaalde gevallen maar niet bij alles
            //het probleem is dat er verwacht wordt dat er een bestaande object is, ook al is er niets in
            //dus in geval dat het niet bestaat, voeg het toe maar niets anders
            //werkt -> 


            if (_selectedVariant == null)
            {
                TfgsvVariant incaseOfNull = new TfgsvVariant();
                incaseOfNull.Variantnaam = cmbVariantText;
                SelectedVariant = incaseOfNull;
                

            }
            //Hierdoor kun je je eigen variant invullen , maar wordt niet weergegeven in de fillcomboxlist 
            //Ook is er hier geen idee aan gekoppeld


            if (_selectedSoort==null)
            {
                TfgsvSoort incaseOfNull = new TfgsvSoort();
                incaseOfNull.Soortnaam = _cmbSoortText;
                SelectedSoort = incaseOfNull;
            }





            
                addPlantService.AddPlantButton(SelectedNederlandseNaam, SelectedType, SelectedFamilie, SelectedGeslacht,
                    SelectedSoort, SelectedVariant);



            ViewModelAbiotiek abiotiek  =  iocc.GetInstance<ViewModelAbiotiek>();

            
            MessageBox.Show(abiotiek.AbioControlsBezonning.ToString());


            string abioBezonning=null, abioGrondsoort =null;

            //functie toevoegen abiotiek ---------------------------

            foreach (RadioButton item in abiotiek.AbioControlsBezonning)
            {
                if ((bool)item.IsChecked)
                {

                    abioBezonning = item.Content.ToString();

                }
            }


            foreach (RadioButton item in abiotiek.AbioControlsGrondsoort)
            {
                if ((bool)item.IsChecked)
                {
                    abioGrondsoort = item.Content.ToString();
                }
            }

          
            _addAbiotiekService.AddAbiotiekButton( abioBezonning, abioGrondsoort);


        }


        #endregion


        //Observable collections
        ////Bind to comboboxes
        public ObservableCollection<TfgsvType> cmbTypes { get; set; }
        public ObservableCollection<TfgsvFamilie> cmbFamilies { get; set; }
        public ObservableCollection<TfgsvGeslacht> cmbGeslacht { get; set; }
        public ObservableCollection<TfgsvSoort> cmbSoort { get; set; }
        public ObservableCollection<TfgsvVariant> cmbVariant { get; set; }
        public ObservableCollection<Fenotype> cmbRatioBladBloei { get; set; }

        ////Bind to ListBoxes
        public ObservableCollection<Plant> filteredPlantResults { get; set; }

        public ObservableCollection<String> detailsSelectedPlant { get; set; }
        

        #region RelayCommands

        //RelayCommands
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand AddPlantCommand { get; set; }
        #endregion

        //Selected Item variables for each combobox
        #region Selected Item variables for each combobox

        private TfgsvType _selectedType;

        public TfgsvType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;

                cmbFamilies.Clear();
                cmbGeslacht.Clear();
                cmbSoort.Clear();
                cmbVariant.Clear();

                _searchService.fillComboBoxFamilie(SelectedType, cmbFamilies);
                OnPropertyChanged();
            }
        }

        private TfgsvFamilie _selectedFamilie;

        public TfgsvFamilie SelectedFamilie
        {
            get { return _selectedFamilie; }
            set
            {
                _selectedFamilie = value;


                cmbGeslacht.Clear();
                cmbSoort.Clear();
                cmbVariant.Clear();

                _searchService.fillComboBoxGeslacht(SelectedFamilie, cmbGeslacht);
                OnPropertyChanged();
            }
        }

        private TfgsvGeslacht _selectedGeslacht;

        public TfgsvGeslacht SelectedGeslacht
        {
            get { return _selectedGeslacht; }
            set
            {
                _selectedGeslacht = value;



                cmbSoort.Clear();
                cmbVariant.Clear();

                _searchService.fillComboBoxSoort(SelectedGeslacht, cmbSoort);
                OnPropertyChanged();
            }
        }

        private TfgsvSoort _selectedSoort;

        public TfgsvSoort SelectedSoort
        {
            get { return _selectedSoort; }
            set
            {
                _selectedSoort = value;

                cmbVariant.Clear();
                // vult de cmb van variant na dat er een soort is geselecteert
                _searchService.fillComboBoxVariant(SelectedSoort, cmbVariant);
                OnPropertyChanged();
            }
        }

        private TfgsvVariant _selectedVariant;

        public TfgsvVariant SelectedVariant
        {
            get {
                
               
               
                    return _selectedVariant;
                
                
            
            
            }
            set
            {


                _selectedVariant = value;
                OnPropertyChanged();
            }
        }

        private string _selectedRatioBloeiBlad;

        public string SelectedRatioBloeiBlad
        {
            get { return _selectedRatioBloeiBlad; }
            set
            {
               

                _selectedRatioBloeiBlad = value;
                OnPropertyChanged();
            }
        }

        private string _selectedNederlandseNaam;

        public string SelectedNederlandseNaam
        {
            get { return _selectedNederlandseNaam; }
            set
            {
                if (SelectedNederlandseNaam == "")
                {
                    _selectedNederlandseNaam = null;
                }
                else
                {
                    _selectedNederlandseNaam = value;
                }

                OnPropertyChanged();
            }
        }

        //This will update the selected plant in the result listbox
        //This will be used to show the selected plant details
        private Plant _selectedPlantInResult;

        public Plant SelectedPlantInResult
        {
            get { return _selectedPlantInResult; }
            set
            {
                _selectedPlantInResult = value;
                FillAllImages();
                OnPropertyChanged();
                _searchService.setSelectedPlant(_selectedPlantInResult);
                _searchService.FillDetailPlantResult(detailsSelectedPlant, SelectedPlantInResult);
               
                //Make the currently selected plant in the Result list available in the SearchService
             
            }
        }


        #endregion

        //written by Mathias
        #region button toevoegen hidden
        private string _loggedInMessage { get; set; }
        public string loggedInMessage
        {
            get
            {
                return _loggedInMessage;
            }
            set
            {
                _loggedInMessage = value;

                RaisePropertyChanged("loggedInMessage");
            }
        }

        private string _btnVisible { get; set; }
        public string btnVisible
        {
            get
            {
                return _btnVisible;
            }
            set
            {
                _btnVisible = value;
                RaisePropertyChanged("btnVisible");
            }
        }



        //------------combobox

        private string _cmbVariantText { get; set; }
   

    public string cmbVariantText
        {
        get
        {
            return this._cmbVariantText;
        }
        set
        {

                _cmbVariantText = value;

                //if (value != this.cmbVariantText)
                //{
                //    this.cmbVariantText = value;
                //    RaisePropertyChanged("cmbVariantText");
                //}

         



            }
    }


        private string _cmbSoortText { get; set; }

        public string cmbSoortText
        {
            get
            {
                return this._cmbSoortText;
            }
            set
            {

                _cmbSoortText = value;

                //if (value != this.cmbVariantText)
                //{
                //    this.cmbVariantText = value;
                //    RaisePropertyChanged("cmbVariantText");
                //}





            }
        }


        #endregion

        //geschreven door owen
        public void FillAllImages()
        {
            ImageBlad = _searchService.GetImageLocation("blad",SelectedPlantInResult);
            ImageBloei = _searchService.GetImageLocation("bloei", SelectedPlantInResult);
            ImageHabitus = _searchService.GetImageLocation("habitus",SelectedPlantInResult);
        }

      
        //geschreven door owen
        #region binding images

        private ImageSource _imageBloei;

        public ImageSource ImageBloei
        {
            get { return _imageBloei; }
            set
            {
                _imageBloei = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _imageHabitus;

        public ImageSource ImageHabitus
        {
            get { return _imageHabitus; }
            set
            {
                _imageHabitus = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _imageBlad;

        public ImageSource ImageBlad
        {
            get { return _imageBlad; }
            set
            {
                _imageBlad = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
            





