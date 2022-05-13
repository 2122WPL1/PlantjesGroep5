using Plantjes.ViewModels.Interfaces;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.Models.Db;
using System.Collections.ObjectModel;
using System.Windows;
using Plantjes.Dao.DAOdb;
using GalaSoft.MvvmLight.Ioc;
using System.Windows.Controls;

namespace Plantjes.ViewModels
{
    //This has to be renamed as ViewModelCommensalisme
    public class ViewModelCommensalisme : ViewModelBase
    {
        private DAOPlant _plantId;
        private DAOCommensalisme _dao;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();
        private IDetailService _detailService = iocc.GetInstance<IDetailService>();

        private ObservableCollection<UIElement> _CommControlsOntwikkelsnelheid, _CommControlsStrategie, _CommControlsSociabiliteit,
                _CommControlsLevensvorm;

        private string _plantName;

        //J: property's to bind in the Abiotiek xaml
        public ObservableCollection<UIElement> CommControlsOntwikkelsnelheid
        {
            get { return _CommControlsOntwikkelsnelheid; }
            set { _CommControlsOntwikkelsnelheid = value; }
        }

        public ObservableCollection<UIElement> CommControlsStrategie
        {
            get { return _CommControlsStrategie; }
            set { _CommControlsStrategie = value; }
        }

        public ObservableCollection<UIElement> CommControlsSociabiliteit
        {
            get { return _CommControlsSociabiliteit; }
            set { _CommControlsSociabiliteit = value; }
        }

        public ObservableCollection<UIElement> CommControlsLevensvorm
        {
            get { return _CommControlsLevensvorm; }
            set { _CommControlsLevensvorm = value; }
        }

        public string plantName
        {
            get { return _plantName; }
            set { _plantName = value; }
        }

        //constr
        public ViewModelCommensalisme(IDetailService detailservice)
        {
            _detailService = detailservice;
            this._dao = SimpleIoc.Default.GetInstance<DAOCommensalisme>();
            CreateControlsOntwikkelsnelheid();
            CreateControlsStrategie();
            CreateControlsSociabiliteit();
            CreateControlsLevensvorm();
            plantName = FillLabelWithNamePlant(_searchService.getSelectedPlant());
        }

        public override void Load()
        {
            FillBasedOnPlant(_searchService.getSelectedPlant());
            plantName = FillLabelWithNamePlant(_searchService.getSelectedPlant());
        }

        #region Functions to create contols(checkboxes) from what we know in the tables from database
        private void CreateControlsOntwikkelsnelheid()
        {
            CommControlsOntwikkelsnelheid = new ObservableCollection<UIElement>();

            foreach (CommOntwikkelsnelheid co in _dao.getAllTypesOntwSnelheid())
            {
                RadioButton rbos = new RadioButton { Content = co.Snelheid, Uid = $"{co.Id}", GroupName= co.GetType().ToString() };
                CommControlsOntwikkelsnelheid.Add(rbos);
            }
        }

        private void CreateControlsStrategie()
        {
            CommControlsStrategie = new ObservableCollection<UIElement>();

            foreach (CommStrategie cs in _dao.getAllTypesStrategie())
            {
                RadioButton rbs = new RadioButton { Content = cs.Strategie, Uid = $"{cs.Id}", GroupName= cs.GetType().ToString() };
                CommControlsStrategie.Add(rbs);
            }
        }

        private void CreateControlsSociabiliteit()
        {
            CommControlsSociabiliteit = new ObservableCollection<UIElement>();

            foreach (CommSocialbiliteit cst in _dao.getAllTypesSociabiliteit())
            {
                CheckBox cbvb = new CheckBox { Content = cst.Sociabiliteit, Uid = $"{cst.Id} + {cst.Waarde}" };
                CommControlsSociabiliteit.Add(cbvb);
            }

        }

        //checkboxes stay because there are multiple possibilities
        private void CreateControlsLevensvorm()
        {
            CommControlsLevensvorm = new ObservableCollection<UIElement>();

            foreach (CommLevensvorm cl in _dao.getAllTypesLevensvorm())
            {
                CheckBox cbvb = new CheckBox { Content = cl.Levensvorm, Uid = $"{cl.Id}" };
                CommControlsLevensvorm.Add(cbvb);
            }

        }


        #endregion


        #region Functions to fill checkboxes based on the details from search result
        private void FillBasedOnPlant(Plant? plant)
        {
            if (plant == null) return;


            foreach (Commensalisme comm in plant.Commensalismes)
            {
                foreach (RadioButton c in CommControlsOntwikkelsnelheid)
                {
                    if (comm.Ontwikkelsnelheid != null && (c as RadioButton).Content.ToString().ToLower() == comm.Ontwikkelsnelheid.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }

                foreach (RadioButton c in CommControlsStrategie)
                {
                    if (comm.Strategie != null && (c as RadioButton).Content.ToString().ToLower() == comm.Strategie.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }                
            }

            foreach (CommensalismeMulti commMulti  in plant.CommensalismeMultis)
            {
                foreach (CheckBox c in CommControlsSociabiliteit)
                {
                    if (commMulti.Waarde != null && (c as CheckBox).Content.ToString().ToLower() == commMulti.Waarde.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }

                foreach (CheckBox c in CommControlsLevensvorm)
                {
                    if (commMulti.Waarde != null && (c as CheckBox).Content.ToString().ToLower() == commMulti.Waarde.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }
            }

            
            
            



            //tijdelijk
            //foreach (CommensalismeMulti commSoc in plant.CommensalismeMultis)
            //{
            //    foreach (CheckBox c in CommControlsSociabiliteit)
            //    {
            //        //als commensamismeMulti.eigenschap = levensvorm
            //        //dan checkbox komt
            //        //als commensamismeMulti.eigenschap => gegevens uitkomen
            //        if (commSoc.Eigenschap != null && (c as CheckBox).Content.ToString().ToLower() == CommensalismeMultis..ToLower())
            //        {
            //            c.IsChecked = true;
            //        }
            //    }
            //}

            //foreach (CommensalismeMulti commSoc in plant.CommensalismeMultis)
            //{
            //    foreach (CheckBox c in CommControlsLevensvorm)
            //    {
            //        if (commSoc.Waarde != null && (c as CheckBox).Content.ToString().ToLower() == commSoc.Waarde.ToLower())
            //        {
            //            c.IsChecked = true;
            //        }
            //    }
            //}

        }
    } 
    #endregion
}

#region Code first year

//private DAOExtraPollenWaarde _daoPollen;
//private DAOExtraNectarWaarde _daoNectar;

//private static SimpleIoc iocc = SimpleIoc.Default;
//private ISearchService _SearchService = iocc.GetInstance<ISearchService>();


//private ObservableCollection<UIElement> _Controls;

//public ObservableCollection<UIElement> CommControls
//{
//    get { return _Controls; }
//    set { _Controls = value; }
//}

//public override void Load()
//{
//    //FillBasedOnPlant(_SearchService.getSelectedPlant());
//}

//public ViewModelHabitat(IDetailService detailservice)
//{
//    this._daoPollen = SimpleIoc.Default.GetInstance<DAOExtraPollenWaarde>();
//    this._daoNectar = SimpleIoc.Default.GetInstance<DAOExtraNectarWaarde>();



//    cmbPollenWaarde = new ObservableCollection<ExtraPollenwaarde>();
//    cmbNectarWaarde = new ObservableCollection<ExtraNectarwaarde>();


//    fillComboBoxPollenwaarde();
//    fillComboBoxNectarwaarde();

//}



//public ObservableCollection<ExtraPollenwaarde> cmbPollenWaarde { get; set; }
//public ObservableCollection<ExtraNectarwaarde> cmbNectarWaarde { get; set; }

////geschreven door christophe, op basis van een voorbeeld van owen
//public void fillComboBoxPollenwaarde()
//{
//    var list = _daoPollen.FillExtraPollenwaardes();

//    foreach (var item in list)
//    {

//        cmbPollenWaarde.Add(item);

//    }
//}

//private string _selectedPollenwaarde;

//public string SelectedPollenwaarde
//{
//    get { return _selectedPollenwaarde; }
//    set
//    {
//        _selectedPollenwaarde = value;
//        OnPropertyChanged();

//    }
//}

//public void fillComboBoxNectarwaarde()
//{
//    var list = _daoNectar.FillExtraNectarwaardes();

//    foreach (var item in list)
//    {
//        cmbNectarWaarde.Add(item);
//    }
//}

//private string _selectedNectarwaarde;

//public string SelectedNectarwaarde
//{
//    get { return _selectedNectarwaarde; }
//    set
//    {
//        _selectedNectarwaarde = value;
//        OnPropertyChanged();

//    }
//}

//private string _selectedOntwikkelsnelheid;

//public string SelectedOntwikkelsnelheid
//{
//    get { return _selectedOntwikkelsnelheid; }
//    set
//    {
//        _selectedOntwikkelsnelheid = value;
//        OnPropertyChanged();
//    }
//}

//#region Binding checkboxen Habitat

//private string _selectedCheckBoxHabitat1;

//public string SelectedCheckBoxHabitat1
//{
//    get { return _selectedCheckBoxHabitat1; }
//    set
//    {
//        _selectedCheckBoxHabitat1 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxHabitat2;

//public string SelectedCheckBoxHabitat2
//{
//    get { return _selectedCheckBoxHabitat2; }
//    set
//    {
//        _selectedCheckBoxHabitat2 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxHabitat3;

//public string SelectedCheckBoxHabitat3
//{
//    get { return _selectedCheckBoxHabitat3; }
//    set
//    {
//        _selectedCheckBoxHabitat3 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxHabitat4;

//public string SelectedCheckBoxHabitat4
//{
//    get { return _selectedCheckBoxHabitat4; }
//    set
//    {
//        _selectedCheckBoxHabitat4 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxHabitat5;

//public string SelectedCheckBoxHabitat5
//{
//    get { return _selectedCheckBoxHabitat5; }
//    set
//    {
//        _selectedCheckBoxHabitat5 = value;
//        MessageBox.Show(SelectedCheckBoxHabitat5.ToString());
//        OnPropertyChanged();
//    }
//}


//private string _selectedCheckBoxBezonningZ;

//public string SelectedCheckBoxBezonningZ
//{
//    get { return _selectedCheckBoxBezonningZ; }
//    set
//    {
//        _selectedCheckBoxBezonningZ = value;
//       OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxBezonningZHS;

//public string SelectedCheckBoxBezonningZHS
//{
//    get { return _selectedCheckBoxBezonningZHS; }
//    set
//    {
//        _selectedCheckBoxBezonningZHS = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxBezonningHS;

//public string SelectedCheckBoxBezonningHS
//{
//    get { return _selectedCheckBoxBezonningHS; }
//    set
//    {
//        _selectedCheckBoxBezonningHS = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxBezonningHSS;

//public string SelectedCheckBoxBezonningHSS
//{
//    get { return _selectedCheckBoxBezonningHSS; }
//    set
//    {
//        _selectedCheckBoxBezonningHSS = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxBezonningS;

//public string SelectedCheckBoxBezonningS
//{
//    get { return _selectedCheckBoxBezonningS; }
//    set
//    {
//        _selectedCheckBoxBezonningS = value;
//        OnPropertyChanged();
//    }
//}


//private string _selectedCheckBoxSociabiliteitI;

//public string SelectedCheckBoxSociabiliteitI
//{
//    get { return _selectedCheckBoxSociabiliteitI; }
//    set
//    {
//        _selectedCheckBoxSociabiliteitI = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxSociabiliteitII;

//public string SelectedCheckBoxSociabiliteitII
//{
//    get { return _selectedCheckBoxSociabiliteitII; }
//    set
//    {
//        _selectedCheckBoxSociabiliteitII = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxSociabiliteitIII;

//public string SelectedCheckBoxSociabiliteitIII
//{
//    get { return _selectedCheckBoxSociabiliteitIII; }
//    set
//    {
//        _selectedCheckBoxSociabiliteitIII = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxSociabiliteitIV;

//public string SelectedCheckBoxSociabiliteitIV
//{
//    get { return _selectedCheckBoxSociabiliteitIV; }
//    set
//    {
//        _selectedCheckBoxSociabiliteitIV = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxSociabiliteitV;

//public string SelectedCheckBoxSociabiliteitV
//{
//    get { return _selectedCheckBoxSociabiliteitV; }
//    set
//    {
//        _selectedCheckBoxSociabiliteitV = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxBijvriendelijk;

//public string SelectedCheckBoxBijvriendelijk
//{
//    get { return _selectedCheckBoxBijvriendelijk; }
//    set
//    {
//        _selectedCheckBoxBijvriendelijk = value;
//       OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxEetbaarKruidbaar;
//public string SelectedCheckBoxEetbaarKruidbaar
//{
//    get { return _selectedCheckBoxEetbaarKruidbaar; }
//    set
//    {
//        _selectedCheckBoxEetbaarKruidbaar = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxGeurend;
//public string SelectedCheckBoxGeurend
//{
//    get { return _selectedCheckBoxGeurend; }
//    set
//    {
//        _selectedCheckBoxGeurend = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxVlindervriendelijk;
//public string SelectedCheckBoxVlindervriendelijk
//{
//    get { return _selectedCheckBoxVlindervriendelijk; }
//    set
//    {
//        _selectedCheckBoxVlindervriendelijk = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBoxVorstgevoelig;
//public string SelectedCheckBoxVorstgevoelig
//{
//    get { return _selectedCheckBoxVorstgevoelig; }
//    set
//    {
//        _selectedCheckBoxVorstgevoelig = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox1;
//public string SelectedCheckBox1
//{
//    get { return _selectedCheckBox1; }
//    set
//    {
//        _selectedCheckBox1 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox2;
//public string SelectedCheckBox2
//{
//    get { return _selectedCheckBox2; }
//    set
//    {
//        _selectedCheckBox2 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox3;
//public string SelectedCheckBox3
//{
//    get { return _selectedCheckBox3; }
//    set
//    {
//        _selectedCheckBox3 = value;
//        OnPropertyChanged();
//    }
//}
//private string _selectedCheckBox4;
//public string SelectedCheckBox4
//{
//    get { return _selectedCheckBox4; }
//    set
//    {
//        _selectedCheckBox4 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox5;
//public string SelectedCheckBox5
//{
//    get { return _selectedCheckBox5; }
//    set
//    {
//        _selectedCheckBox5 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox6;
//public string SelectedCheckBox6
//{
//    get { return _selectedCheckBox6; }
//    set
//    {
//        _selectedCheckBox6 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox7;
//public string SelectedCheckBox7
//{
//    get { return _selectedCheckBox7; }
//    set
//    {
//        _selectedCheckBox7 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox8;
//public string SelectedCheckBox8
//{
//    get { return _selectedCheckBox8; }
//    set
//    {
//        _selectedCheckBox8 = value;
//        OnPropertyChanged();
//    }
//}

//private string _selectedCheckBox9;
//public string SelectedCheckBox9
//{
//    get { return _selectedCheckBox9; }
//    set
//    {
//        _selectedCheckBox9 = value;
//        MessageBox.Show(SelectedCheckBox9.ToString());
//        OnPropertyChanged();
//    }
//} 
#endregion
