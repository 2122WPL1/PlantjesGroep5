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
using System.Collections.Generic;

namespace Plantjes.ViewModels
{
    public class ViewModelNameResult : ViewModelBase
    {

        

        //private ServiceProvider _serviceProvider;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService;
        public IloginUserService loginUserService;

        public IAddPlantService addPlantService;

        //addbiotiek

        private IAddAbiotiekService _addAbiotiekService = iocc.GetInstance<IAddAbiotiekService>();

        private IAddAbiotiekMultiService _addAbiotiekMultiService = iocc.GetInstance<IAddAbiotiekMultiService>();


        //Fenotype
        private IAddFenotypeService _addFenotypeService = iocc.GetInstance<IAddFenotypeService>();



        public ViewModelNameResult(ISearchService searchService, IloginUserService loginUserService, IAddPlantService addPlantService, IAddAbiotiekService addBiotiekService, IAddAbiotiekMultiService addBiotiekMultiService, IAddFenotypeService addFenotyêService)
        {
            _addAbiotiekService = addBiotiekService;
            _addAbiotiekMultiService = addBiotiekMultiService;

            _addFenotypeService = addFenotyêService;

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


            cmbVariantText = null;
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

            //Checks to see if the user written in the required info , otherwise give up an error message

            if (string.IsNullOrEmpty(SelectedType?.Planttypenaam))
            {
                MessageBox.Show("VUl DE TYPE IN !");
                return;
            }
            if (string.IsNullOrEmpty(SelectedFamilie?.Familienaam))
            {
                MessageBox.Show("VUl DE FAMILIE IN !");
                return;
            }
            if (string.IsNullOrEmpty(SelectedGeslacht?.Geslachtnaam))
            {
                MessageBox.Show("VUl DE GESLACHT IN !");
                return;
            }

          
           


            addPlantService.AddPlantButton(SelectedNederlandseNaam, SelectedType, SelectedFamilie, SelectedGeslacht,
                     SelectedSoort, SelectedVariant);



            //feno
            ViewModelFenotype fenotype = iocc.GetInstance<ViewModelFenotype>();






            int fenoBladgrootte = 0;
            string fenoBladvorm = null, fenoRatioBloeiBlad = null, fenoSpruitfenologie = null, fenoBloeiwijze = null, fenoHabitus = null, fenoLevensvorm = null;
            //functie toevoegen abiotiek
            //gaat elke radio button af in de ui, als hij één checked vindt dan weergeeft hij de radioButtonweer
            foreach (RadioButton item in fenotype.FenoControlsBladgrootte)
            {
                if ((bool)item.IsChecked)
                {

                    fenoBladgrootte = item.Content.GetHashCode();

                }
            }


            foreach (RadioButton item in fenotype.FenoControlsBladvorm)
            {
                if ((bool)item.IsChecked)
                {
                    fenoBladvorm = item.Content.ToString();
                }
            }

            foreach (RadioButton item in fenotype.FenoControlsRatiobloeiblad)
            {
                if ((bool)item.IsChecked)
                {
                    fenoRatioBloeiBlad = item.Content.ToString();
                }
            }

            foreach (RadioButton item in fenotype.FenoControlsSpruitfenologie)
            {
                if ((bool)item.IsChecked)
                {
                    fenoSpruitfenologie = item.Content.ToString();
                }
            }

            foreach (RadioButton item in fenotype.FenoControlsBloeiwijze)
            {
                if ((bool)item.IsChecked)
                {
                    fenoBloeiwijze = item.Content.ToString();
                }
            }

            foreach (RadioButton item in fenotype.FenoControlsHabitus)
            {
                if ((bool)item.IsChecked)
                {
                    fenoHabitus = item.Content.ToString();
                }
            }

            foreach (RadioButton item in fenotype.FenoControlsLevensvorm)
            {
                if ((bool)item.IsChecked)
                {
                    fenoLevensvorm = item.Content.ToString();
                }
            }
            _addFenotypeService.AddFenotypeButton(fenoBladgrootte, fenoBladvorm, fenoRatioBloeiBlad, fenoSpruitfenologie, fenoBloeiwijze.Substring(0,3), fenoHabitus.Substring(0, 3), fenoLevensvorm.Substring(0, 1));

           

      






        //HIIER

        //Get the info from viewmodel Abiotiek -I

        ViewModelAbiotiek abiotiek  =  iocc.GetInstance<ViewModelAbiotiek>();
            
            //declaration of empty elements-I

            string abioBezonning =null, abioGrondsoort =null, AbioVochtbehoefte=null, AbioVoedingsBehoefte =null, 
                AbioReactieAntagonischeOmg=null;

            List<string> abioHabitat = new List<string>();


            //checks in Viewmodel Abiotiek and checks each element and gets the element associated string if it's checked

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

            foreach (RadioButton item in abiotiek.AbioControlsVochtbehoefte)
            {
                if ((bool)item.IsChecked)
                {
                    AbioVochtbehoefte = item.Content.ToString();
                }
            }
            foreach (RadioButton item in abiotiek.AbioControlsVoedingsbehoefte)
            {
                if ((bool)item.IsChecked)
                {
                    AbioVoedingsBehoefte = item.Content.ToString();
                }
            }

            foreach (RadioButton item in abiotiek.AbioControlsReactieAntagonischeOmg)
            {
                if ((bool)item.IsChecked)
                {
                    AbioReactieAntagonischeOmg = item.Content.ToString();
                }
            }



            foreach (CheckBox item in abiotiek.AbioControlsHabitat)
            {
                if ((bool)item.IsChecked)
                {
                    abioHabitat.Add(item.Content.ToString());
                }
            }



            //Links the datataken from viewmodel to the AddBiotiekButton and continue in the classes focused in Abiotiek-I
            _addAbiotiekService.AddAbiotiekButton( abioBezonning, abioGrondsoort, AbioVochtbehoefte, AbioVoedingsBehoefte, AbioReactieAntagonischeOmg);

            _addAbiotiekMultiService.AddAbiotiekMultiButton(abioHabitat);




            //hier is feno








        }




        /// <summary>
        /// Fenotypes
        /// </summary>








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
                // fills the combobox in
                _searchService.fillComboBoxVariant(SelectedSoort, cmbVariant);
                OnPropertyChanged();
            }
        }

        //Binding properties - Imran

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


     

        //written by Mathias
      
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



        //Bindings combobox -Imran

        private string _cmbVariantText { get; set; }
   

    public string cmbVariantText
        {
        get
        {
                


            return this._cmbVariantText;
        }
        set
        {

                if (_cmbVariantText == "")
                {
                    _cmbVariantText = null;
                }
                else
                {
                    _cmbVariantText = value;
                }

                OnPropertyChanged();





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

                if (_cmbSoortText == "")
                {
                    _cmbSoortText = null;
                }
                else
                {
                    _cmbSoortText = value;
                }

                OnPropertyChanged();






            }
        }


        

        //geschreven door owen
        public void FillAllImages()
        {
            ImageBlad = _searchService.GetImageLocation("blad",SelectedPlantInResult);
            ImageBloei = _searchService.GetImageLocation("bloei", SelectedPlantInResult);
            ImageHabitus = _searchService.GetImageLocation("habitus",SelectedPlantInResult);
        }

      
        //geschreven door owen
     

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


       
    }
}





