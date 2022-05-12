using Plantjes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace Plantjes.ViewModels
{
    public class ViewModelFenotype : ViewModelBase
    {
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
       
        private DAOFenotype _dao;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private IDetailService _detailService = iocc.GetInstance<IDetailService>();
        private ISearchService _SearchService = iocc.GetInstance<ISearchService>();

        //J
        BeheerMaand beheermaandObject = new BeheerMaand();
        List<string> subListForMonths = new List<string>();

        private ObservableCollection<UIElement> _FenoControlsBladgrootte, _FenoControlsBladvorm, _FenoControlsRatiobloeiblad,
            _FenoControlsSpruitfenologie, _FenoControlsBloeiwijze, _FenoControlsHabitus, _FenoControlsLevensvorm, _FenoControlsBladKleur,
            _FenoControlsBloeiKleur, _fenoBeheermaandBlad, _fenoBeheermaandBloei;

        //J: property's to bind in the Fenotype xaml
        public ObservableCollection<UIElement> FenoControlsBladgrootte
        {
            get { return _FenoControlsBladgrootte; }
            set { _FenoControlsBladgrootte = value; }
        }
        public ObservableCollection<UIElement> FenoControlsBladvorm
        {
            get { return _FenoControlsBladvorm; }
            set { _FenoControlsBladvorm = value; }
        }
        public ObservableCollection<UIElement> FenoControlsRatiobloeiblad
        {
            get { return _FenoControlsRatiobloeiblad; }
            set { _FenoControlsRatiobloeiblad = value; }
        }
        public ObservableCollection<UIElement> FenoControlsSpruitfenologie
        {
            get { return _FenoControlsSpruitfenologie; }
            set { _FenoControlsSpruitfenologie = value; }
        }
        public ObservableCollection<UIElement> FenoControlsBloeiwijze
        {
            get { return _FenoControlsBloeiwijze; }
            set { _FenoControlsBloeiwijze = value; }
        }
        public ObservableCollection<UIElement> FenoControlsHabitus
        {
            get { return _FenoControlsHabitus; }
            set { _FenoControlsHabitus = value; }
        }
        public ObservableCollection<UIElement> FenoControlsLevensvorm
        {
            get { return _FenoControlsLevensvorm; }
            set { _FenoControlsLevensvorm = value; }
        }
        public ObservableCollection<UIElement> FenoControlsBladKleur
        {
            get { return _FenoControlsBladKleur; }
            set { _FenoControlsBladKleur = value; }

        }
        public ObservableCollection<UIElement> FenoControlsBloeiKleur
        {
            get { return _FenoControlsBloeiKleur; }
            set { _FenoControlsBloeiKleur = value; }
        }
        public ObservableCollection<UIElement> FenoBeheermaandBlad
        {
            get { return _fenoBeheermaandBlad; }
            set { _fenoBeheermaandBlad = value; }
        }
        public ObservableCollection<UIElement> FenoBeheermaandBloei
        {
            get { return _fenoBeheermaandBloei; }
            set { _fenoBeheermaandBloei = value; }
        }

        //constr
        public ViewModelFenotype(IDetailService detailservice)
        {
            _detailService = detailservice;
            this._dao = SimpleIoc.Default.GetInstance<DAOFenotype>();
            GetMonthsFromBeheerMaand();
            CreateControlsBladgrootte();
            CreateControlsBladvorm();
            CreateControlsRatiobloeiblad();
            CreateControlsSpruitfenologie();
            CreateControlsBloeiwijze();
            CreateControlsHabitus();
            CreateControlsLevensvorm();
            CreateControlsBladkleur();
            CreateControlsBloeikleur();
            CreateControlsMaandenBlad();
            CreateControlsMaandenBloei();


        }

        public override void Load()
        {
            FillBasedOnPlant(_SearchService.getSelectedPlant());
        }

        public List<string> GetMonthsFromBeheerMaand()
        {
            List<string> propertyList = new List<string>();


            foreach (var prop in beheermaandObject.GetType().GetProperties())
            {
                propertyList.Add(prop.Name);
            }

            subListForMonths = propertyList.Skip(4).Take(12).ToList();

            return subListForMonths;

        }

        private void CreateControlsMaandenBlad()
        {
            FenoBeheermaandBlad = new ObservableCollection<UIElement>();

            foreach (var prop in subListForMonths)
            {
                CheckBox cbm = new CheckBox { Content = prop.ToString() };
                FenoBeheermaandBlad.Add(cbm);
            }

        }

        private void CreateControlsMaandenBloei()
        {
            FenoBeheermaandBloei = new ObservableCollection<UIElement>();

            foreach (var prop in subListForMonths)
            {
                CheckBox cbm = new CheckBox { Content = prop.ToString() };
                FenoBeheermaandBloei.Add(cbm);
            }

        }




        #region J: function to create the checkboxes with the info from fenotype
        private void CreateControlsBladgrootte()
        {
            FenoControlsBladgrootte = new ObservableCollection<UIElement>();

            foreach (FenoBladgrootte fbg in _dao.getAllTypesBladgrootte())
            {
                //content and name are propertys from AbioBezonning
                RadioButton cbbg = new RadioButton { Content = fbg.Bladgrootte, Uid = $"{fbg.Id}", GroupName = fbg.GetType().ToString() };
                FenoControlsBladgrootte.Add(cbbg);
            }
        }

        private void CreateControlsBladvorm()
        {
            FenoControlsBladvorm = new ObservableCollection<UIElement>();

            foreach (FenoBladvorm fbv in _dao.getAllTypesBladvorm())
            {
                RadioButton rbbv = new RadioButton { Content = fbv.Vorm, Uid = $"{fbv.Id}", GroupName = fbv.GetType().ToString() };
                FenoControlsBladvorm.Add(rbbv);
            }
        }

        private void CreateControlsRatiobloeiblad()
        {
            FenoControlsRatiobloeiblad = new ObservableCollection<UIElement>();

            foreach (FenoRatioBloeiBlad frbb in _dao.getAllTypesRatioBloei())
            {
                RadioButton rbrb = new RadioButton { Content = frbb.RatioBloeiBlad, Uid = $"{frbb.Id}", GroupName = frbb.GetType().ToString() };
                FenoControlsRatiobloeiblad.Add(rbrb);
            }
        }

        private void CreateControlsSpruitfenologie()
        {
            FenoControlsSpruitfenologie = new ObservableCollection<UIElement>();

            foreach (FenoSpruitfenologie fsf in _dao.getAllTypesSpruitFeno())
            {
                RadioButton rbsf = new RadioButton { Content = fsf.Fenologie, Uid = $"{fsf.Id}", GroupName = fsf.GetType().ToString() };
                FenoControlsSpruitfenologie.Add(rbsf);
            }
        }

        private void CreateControlsBloeiwijze()
        {
            FenoControlsBloeiwijze = new ObservableCollection<UIElement>();

            foreach (FenoBloeiwijze fbw in _dao.getAllTypesBloeiwijze())
            {
                RadioButton rbbw = new RadioButton { Content = fbw.Naam, Uid = $"{fbw.Id}", GroupName = fbw.GetType().ToString() };
                FenoControlsBloeiwijze.Add(rbbw);
            }
        }

        private void CreateControlsHabitus()
        {
            FenoControlsHabitus = new ObservableCollection<UIElement>();

            foreach (FenoHabitu fh in _dao.getAllTypesFenoHabitus())
            {
                RadioButton rbfh = new RadioButton { Content = fh.Naam, Uid = $"{fh.Id}", GroupName = fh.GetType().ToString() };
                FenoControlsHabitus.Add(rbfh);
            }
        }

        private void CreateControlsLevensvorm()
        {
            FenoControlsLevensvorm = new ObservableCollection<UIElement>();

            foreach (FenoLevensvorm fl in _dao.getAllTypesFenoLevensvorm())
            {
                RadioButton rblv = new RadioButton { Content = fl.Levensvorm, Uid = $"{fl.Id}", GroupName = fl.GetType().ToString() };
                FenoControlsLevensvorm.Add(rblv);
            }
        }

        private void CreateControlsBladkleur()
        {
            FenoControlsBladKleur = new ObservableCollection<UIElement>();

            foreach (FenoKleur fk in _dao.getAllTypesKleur())
            {
                RadioButton rbbladk = new RadioButton { Content = fk.NaamKleur, Uid = $"{fk.Id}", GroupName = fk.GetType().ToString() };
                FenoControlsBladKleur.Add(rbbladk);
            }
        }

        private void CreateControlsBloeikleur()
        {
            FenoControlsBloeiKleur = new ObservableCollection<UIElement>();

            foreach (FenoKleur fk in _dao.getAllTypesKleur())
            {
                RadioButton rbbloeik = new RadioButton { Content = fk.NaamKleur, Uid = $"{fk.Id}", GroupName = fk.GetType().ToString() };
                FenoControlsBloeiKleur.Add(rbbloeik);
            }
        }
        #endregion







        #region J: function to fill the correct radiobuttons/checkboxes with details from plant
        private void FillBasedOnPlant(Plant? plant)
        {
            if (plant == null) return;

            foreach (Fenotype feno in plant.Fenotypes)
            {
                foreach (RadioButton rbFe in FenoControlsBladgrootte)
                {
                    if (feno.Bladgrootte != null && (rbFe as RadioButton).Content.ToString() == feno.Bladgrootte.ToString())
                    {
                        rbFe.IsChecked = true;

                    }
                }

                foreach (RadioButton rbFe in FenoControlsBladvorm)
                {
                    if (feno.Bladvorm != null && (rbFe as RadioButton).Content.ToString().ToLower() == feno.Bladvorm.ToLower())
                    {
                        rbFe.IsChecked = true;

                    }
                }

                foreach (RadioButton rbFe in FenoControlsRatiobloeiblad)
                {
                    if (feno.RatioBloeiBlad != null && (rbFe as RadioButton).Content.ToString().ToLower() == feno.RatioBloeiBlad.ToLower())
                    {
                        rbFe.IsChecked = true;

                    }
                }

                foreach (RadioButton rbFe in FenoControlsSpruitfenologie)
                {
                    if (feno.Spruitfenologie != null && (rbFe as RadioButton).Content.ToString().ToLower() == feno.Spruitfenologie.ToLower())
                    {
                        rbFe.IsChecked = true;

                    }
                }

                foreach (RadioButton rbFe in FenoControlsBloeiwijze)
                {
                    if (feno.Bloeiwijze != null && (rbFe as RadioButton).Content.ToString().ToLower() == feno.Bloeiwijze.ToLower())
                    {
                        rbFe.IsChecked = true;

                    }
                }

                foreach (RadioButton rbFe in FenoControlsHabitus)
                {
                    if (feno.Habitus != null && (rbFe as RadioButton).Content.ToString().ToLower() == feno.Habitus.ToLower())
                    {
                        rbFe.IsChecked = true;

                    }
                }

                foreach (RadioButton rbFe in FenoControlsLevensvorm)
                {
                    if (feno.Levensvorm != null && (rbFe as RadioButton).Content.ToString().ToLower() == feno.Levensvorm.ToLower())
                    {
                        rbFe.IsChecked = true;

                    }
                }

            }

            foreach (FenotypeMulti fenoMulti in plant.FenotypeMultis)
            {
                foreach (CheckBox cbbladk in FenoControlsBladKleur)
                {
                    if (fenoMulti.Waarde != null && (cbbladk as CheckBox).Content.ToString().ToLower() == fenoMulti.Waarde.ToLower())
                    {
                        cbbladk.IsChecked = true;

                    }
                }

                foreach (CheckBox cbbloeik in FenoControlsBloeiKleur)
                {
                    if (fenoMulti.Waarde != null && (cbbloeik as CheckBox).Content.ToString().ToLower() == fenoMulti.Waarde.ToLower())
                    {
                        cbbloeik.IsChecked = true;

                    }
                }
            }
        } 
        #endregion





        #region code vorig jaar
        //geschreven door christophe, op basis van een voorbeeld van owen
        private string _selectedBloeiHoogte;

        public string SelectedBloeiHoogte
        {
            get { return _selectedBloeiHoogte; }
            set
            {
                _selectedBloeiHoogte = value;
                OnPropertyChanged();


            }
        } 
        #endregion

        #region Checkbox Bloeikleur

        private bool _selectedCheckBoxBloeikleurZwart;
        public bool SelectedCheckBoxBloeikleurZwart
        {
            get { return _selectedCheckBoxBloeikleurZwart; }

            set
            {
                _selectedCheckBoxBloeikleurZwart = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurWit;
        public bool SelectedCheckBoxBloeikleurWit
        {
            get { return _selectedCheckBoxBloeikleurWit; }

            set
            {
                _selectedCheckBoxBloeikleurWit = value;
                MessageBox.Show(SelectedCheckBoxBloeikleurZwart.ToString());
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurRosé;
        public bool SelectedCheckBoxBloeikleurRosé
        {
            get { return _selectedCheckBoxBloeikleurRosé; }

            set
            {
                _selectedCheckBoxBloeikleurRosé = value;
               OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurRood;
        public bool SelectedCheckBoxBloeikleurRood
        {
            get { return _selectedCheckBoxBloeikleurRood; }

            set
            {
                _selectedCheckBoxBloeikleurRood = value;
               OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurOranje;
        public bool SelectedCheckBoxBloeikleurOranje
        {
            get { return _selectedCheckBoxBloeikleurOranje; }

            set
            {
                _selectedCheckBoxBloeikleurOranje = value;
               OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurLila;
        public bool SelectedCheckBoxBloeikleurLila
        {
            get { return _selectedCheckBoxBloeikleurLila; }

            set
            {
                _selectedCheckBoxBloeikleurLila = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurGrijs;
        public bool SelectedCheckBoxBloeikleurGrijs
        {
            get { return _selectedCheckBoxBloeikleurGrijs; }

            set
            {
                _selectedCheckBoxBloeikleurGrijs = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurGroen;
        public bool SelectedCheckBoxBloeikleurGroen
        {
            get { return _selectedCheckBoxBloeikleurGroen; }

            set
            {
                _selectedCheckBoxBloeikleurGroen = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurGeel;
        public bool SelectedCheckBoxBloeikleurGeel
        {
            get { return _selectedCheckBoxBloeikleurGeel; }

            set
            {
                _selectedCheckBoxBloeikleurGeel= value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurBlauw;
        public bool SelectedCheckBoxBloeikleurBlauw
        {
            get { return _selectedCheckBoxBloeikleurBlauw; }

            set
            {
                _selectedCheckBoxBloeikleurBlauw = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurViolet;
        public bool SelectedCheckBoxBloeikleurViolet
        {
            get { return _selectedCheckBoxBloeikleurViolet; }

            set
            {
                _selectedCheckBoxBloeikleurViolet = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurPaars;
        public bool SelectedCheckBoxBloeikleurPaars
        {
            get { return _selectedCheckBoxBloeikleurPaars; }

            set
            {
                _selectedCheckBoxBloeikleurPaars = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeikleurBruin;
        public bool SelectedCheckBoxBloeikleurBruin
        {
            get { return _selectedCheckBoxBloeikleurBruin; }

            set
            {
                _selectedCheckBoxBloeikleurBruin = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Binding Checkbox BloeiHoogte

        private bool _selectedCheckBoxBloeiHoogteJan;
        public bool SelectedCheckBoxBloeiHoogteJan
        {
            get { return _selectedCheckBoxBloeiHoogteJan; }

            set
            {
                _selectedCheckBoxBloeiHoogteJan = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteFeb;
        public bool SelectedCheckBoxBloeiHoogteFeb
        {
            get { return _selectedCheckBoxBloeiHoogteFeb; }

            set
            {
                _selectedCheckBoxBloeiHoogteFeb = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteMar;
        public bool SelectedCheckBoxBloeiHoogteMar
        {
            get { return _selectedCheckBoxBloeiHoogteMar; }

            set
            {
                _selectedCheckBoxBloeiHoogteMar = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteApr;
        public bool SelectedCheckBoxBloeiHoogteApr
        {
            get { return _selectedCheckBoxBloeiHoogteApr; }

            set
            {
                _selectedCheckBoxBloeiHoogteApr = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteMay;
        public bool SelectedCheckBoxBloeiHoogteMay
        {
            get { return _selectedCheckBoxBloeiHoogteMay; }

            set
            {
                _selectedCheckBoxBloeiHoogteMay = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteJun;
        public bool SelectedCheckBoxBloeiHoogteJun
        {
            get { return _selectedCheckBoxBloeiHoogteJun; }

            set
            {
                _selectedCheckBoxBloeiHoogteJun = value;
                OnPropertyChanged();
            }
        }
        private bool _selectedCheckBoxBloeiHoogteJul;
        public bool SelectedCheckBoxBloeiHoogteJul
        {
            get { return _selectedCheckBoxBloeiHoogteJul; }

            set
            {
                _selectedCheckBoxBloeiHoogteJul = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteAug;
        public bool SelectedCheckBoxBloeiHoogteAug
        {
            get { return _selectedCheckBoxBloeiHoogteAug; }

            set
            {
                _selectedCheckBoxBloeiHoogteAug = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteSep;
        public bool SelectedCheckBoxBloeiHoogteSep
        {
            get { return _selectedCheckBoxBloeiHoogteSep; }

            set
            {
                _selectedCheckBoxBloeiHoogteSep = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteOct;
        public bool SelectedCheckBoxBloeiHoogteOct
        {
            get { return _selectedCheckBoxBloeiHoogteOct; }

            set
            {
                _selectedCheckBoxBloeiHoogteOct = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteNov;
        public bool SelectedCheckBoxBloeiHoogteNov
        {
            get { return _selectedCheckBoxBloeiHoogteNov; }

            set
            {
                _selectedCheckBoxBloeiHoogteNov = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiHoogteDec;
        public bool SelectedCheckBoxBloeiHoogteDec
        {
            get { return _selectedCheckBoxBloeiHoogteDec; }

            set
            {
                _selectedCheckBoxBloeiHoogteDec = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Binding Checkbox Bloeit In

        private bool _selectedCheckBoxBloeitInJan;
        
        public bool SelectedCheckBoxBloeitInJan
        {
            get { return _selectedCheckBoxBloeitInJan; }

            set
            {
                _selectedCheckBoxBloeitInJan = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInFeb;
        public bool SelectedCheckBoxBloeitInFeb
        {
            get { return _selectedCheckBoxBloeitInFeb; }

            set
            {
                _selectedCheckBoxBloeitInFeb = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInMar;
        public bool SelectedCheckBoxBloeitInMar
        {
            get { return _selectedCheckBoxBloeitInMar; }

            set
            {
                _selectedCheckBoxBloeitInMar = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInApr;
        public bool SelectedCheckBoxBloeitInApr
        {
            get { return _selectedCheckBoxBloeitInApr; }

            set
            {
                _selectedCheckBoxBloeitInApr = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInMay;
        public bool SelectedCheckBoxBloeitInMay
        {
            get { return _selectedCheckBoxBloeitInMay; }

            set
            {
                _selectedCheckBoxBloeitInMay = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInJun;
        public bool SelectedCheckBoxBloeitInJun
        {
            get { return _selectedCheckBoxBloeitInJun; }

            set
            {
                _selectedCheckBoxBloeitInJun = value;
                OnPropertyChanged();
            }
        }
        private bool _selectedCheckBoxBloeitInJul;
        public bool SelectedCheckBoxBloeitInJul
        {
            get { return _selectedCheckBoxBloeitInJul; }

            set
            {
                _selectedCheckBoxBloeitInJul = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInAug;
        public bool SelectedCheckBoxBloeitInAug
        {
            get { return _selectedCheckBoxBloeitInAug; }

            set
            {
                _selectedCheckBoxBloeitInAug = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInSep;
        public bool SelectedCheckBoxBloeitInSep
        {
            get { return _selectedCheckBoxBloeitInSep; }

            set
            {
                _selectedCheckBoxBloeitInSep = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInOct;
        public bool SelectedCheckBoxBloeitInOct
        {
            get { return _selectedCheckBoxBloeitInOct; }

            set
            {
                _selectedCheckBoxBloeitInOct = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInNov;
        public bool SelectedCheckBoxBloeitInNov
        {
            get { return _selectedCheckBoxBloeitInNov; }

            set
            {
                _selectedCheckBoxBloeitInNov = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeitInDec;
        public bool SelectedCheckBoxBloeitInDec
        {
            get { return _selectedCheckBoxBloeiHoogteDec; }

            set
            {
                _selectedCheckBoxBloeiHoogteDec = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Binding checkboxes Bloeiwijzevorm

        private bool _selectedCheckBoxBloeiwijzeVorm1;
        public bool SelectedCheckBoxBloeiwijzeVorm1
        {
            get { return _selectedCheckBoxBloeiwijzeVorm1; }

            set
            {
                _selectedCheckBoxBloeiwijzeVorm1 = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiwijzeVorm2;
        public bool SelectedCheckBoxBloeiwijzeVorm2
        {
            get { return _selectedCheckBoxBloeiwijzeVorm2; }

            set
            {
                _selectedCheckBoxBloeiwijzeVorm2 = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiwijzeVorm3;
        public bool SelectedCheckBoxBloeiwijzeVorm3
        {
            get { return _selectedCheckBoxBloeiwijzeVorm3; }

            set
            {
                _selectedCheckBoxBloeiwijzeVorm3 = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiwijzeVorm4;
        public bool SelectedCheckBoxBloeiwijzeVorm4
        {
            get { return _selectedCheckBoxBloeiwijzeVorm4; }

            set
            {
                _selectedCheckBoxBloeiwijzeVorm4 = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiwijzeVorm5;
        public bool SelectedCheckBoxBloeiwijzeVorm5
        {
            get { return _selectedCheckBoxBloeiwijzeVorm5; }

            set
            {
                _selectedCheckBoxBloeiwijzeVorm5 = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxBloeiwijzeVorm6;
        public bool SelectedCheckBoxBloeiwijzeVorm6
        {
            get { return _selectedCheckBoxBloeiwijzeVorm6; }

            set
            {
                _selectedCheckBoxBloeiwijzeVorm6 = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}

