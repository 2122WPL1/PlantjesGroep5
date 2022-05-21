using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Dao;
using System.Windows;
using Plantjes.Dao.DAOdb;
using Plantjes.Models.Db;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace Plantjes.ViewModels
{
    public class ViewModelAbiotiek : ViewModelBase
    {
        //decl necessary components like DAO and ioc container
        private DAOPlant _plantId;
        private DAOAbiotiek _dao;
        private DAOAbiotiekMulti _daoMulti; //multi Abiotiek
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();
        private IDetailService _detailService = iocc.GetInstance<IDetailService>();
        private IAddAbiotiekService _addAbiotiekService = iocc.GetInstance<IAddAbiotiekService>();

        private IAddAbiotiekMultiService _addAbiotiekMultiService = iocc.GetInstance<IAddAbiotiekMultiService>();

        //decl user interface elements 
        private ObservableCollection<UIElement> _AbioControlsBezonning, _AbioControlsVochtbehoefte, _AbioControlsVoedingsbehoefte,
                _AbioControlsGrondsoort, _AbioControlsReactieAntagonischeOmg, _AbioControlsHabitat;

        private string _plantName;

        //J: property's to bind in the Abiotiek xaml
        public ObservableCollection<UIElement> AbioControlsBezonning
        {
            get { return _AbioControlsBezonning; }
            set { _AbioControlsBezonning = value; }
        }

        public ObservableCollection<UIElement> AbioControlsVochtbehoefte
        {
            get { return _AbioControlsVochtbehoefte; }
            set { _AbioControlsVochtbehoefte = value; }
        }

        public ObservableCollection<UIElement> AbioControlsVoedingsbehoefte
        {
            get { return _AbioControlsVoedingsbehoefte; }
            set { _AbioControlsVoedingsbehoefte = value; }
        }

        public ObservableCollection<UIElement> AbioControlsGrondsoort
        {
            get { return _AbioControlsGrondsoort; }
            set { _AbioControlsGrondsoort = value; }
        }

        

        public ObservableCollection<UIElement> AbioControlsReactieAntagonischeOmg
        {
            get { return _AbioControlsReactieAntagonischeOmg; }
            set { _AbioControlsReactieAntagonischeOmg = value; }
        }

        public ObservableCollection<UIElement> AbioControlsHabitat
        {
            get { return _AbioControlsHabitat; }
            set { _AbioControlsHabitat = value; }
        }

        public string plantName
        {
            get { return _plantName; }
            set { _plantName = value; }
        }

        //J: const
        public ViewModelAbiotiek(IDetailService detailservice, IAddAbiotiekService addBiotiekService, IAddAbiotiekMultiService addAbiotiekMultiService)
        {
            _addAbiotiekService = addBiotiekService;
            _addAbiotiekMultiService = addAbiotiekMultiService;


            _detailService = detailservice;
            this._dao = SimpleIoc.Default.GetInstance<DAOAbiotiek>();
            this._daoMulti = SimpleIoc.Default.GetInstance<DAOAbiotiekMulti>();
            CreateControlsBezonning();
            CreateControlsVochtbehoefte();
            CreateControlsVoedingsbehoefte();
            CreateControlsGrondsoort();
            CreateControlsReactieAntagonischeOmg();
            CreateControlsReactieAbioHabitat();




           // OpslaanAbiotiekCommand = new RelayCommand(AddAbiotiekClick);

        }
        public override void Load()
        {
            FillBasedOnPlant(_searchService.getSelectedPlant());
            plantName = FillLabelWithNamePlant(_searchService.getSelectedPlant());
        }


        



        #region J: Function to create contols(checkboxes) from what we know in the tables from database 
        private void CreateControlsBezonning()
        {
            AbioControlsBezonning = new ObservableCollection<UIElement>();

           

            foreach (AbioBezonning ab in _dao.getAllTypes())
            {
                RadioButton rbb = new RadioButton { Content = ab.Naam, Uid = $"{ab.Id}", GroupName = ab.GetType().ToString() };
                AbioControlsBezonning.Add(rbb);
            }
        }

        private void CreateControlsVochtbehoefte()
        {
            AbioControlsVochtbehoefte = new ObservableCollection<UIElement>();

            foreach (AbioVochtbehoefte av in _dao.getAllTypesVochtbehoefte())
            {
                RadioButton rbv = new RadioButton { Content = av.Vochtbehoefte, Uid = $"{av.Id}", GroupName = av.GetType().ToString() };
                AbioControlsVochtbehoefte.Add(rbv);
            }
        }

        private void CreateControlsVoedingsbehoefte()
        {
            AbioControlsVoedingsbehoefte = new ObservableCollection<UIElement>();

            foreach (AbioVoedingsbehoefte avb in _dao.getAllTypesVoedingsbehoefte())
            {
                RadioButton rbvb = new RadioButton { Content = avb.Voedingsbehoefte, Uid = $"{avb.Id}", GroupName = avb.GetType().ToString() };
                AbioControlsVoedingsbehoefte.Add(rbvb);
            }

        }

        private void CreateControlsGrondsoort()
        {
            AbioControlsGrondsoort = new ObservableCollection<UIElement>();

            foreach (AbioGrondsoort avb in _dao.getAllTypesGrondsoort())
            {
                RadioButton rbgs = new RadioButton { Content = avb.Grondsoort, Uid = $"{avb.Id}", GroupName = avb.GetType().ToString() };
                AbioControlsGrondsoort.Add(rbgs);
            }

        }

        private void CreateControlsReactieAntagonischeOmg()
        {
            AbioControlsReactieAntagonischeOmg = new ObservableCollection<UIElement>();

            foreach (AbioReactieAntagonischeOmg aa in _dao.getAllTypesOmgeving())
            {
                RadioButton rba = new RadioButton { Content = aa.Antagonie, Uid = $"{aa.Id}", GroupName = aa.GetType().ToString() };
                AbioControlsReactieAntagonischeOmg.Add(rba);
            }

        }

        private void CreateControlsReactieAbioHabitat()
        {
            AbioControlsHabitat = new ObservableCollection<UIElement>();

            foreach (AbioHabitat ah in _dao.getAllTypesHabitat())
            {
                CheckBox cbh = new CheckBox { Content = ah.Afkorting, Uid = $"{ah.Id}" };
                AbioControlsHabitat.Add(cbh);
            }

        }




        #region J: function to fill checkboxes based on the details from search result
        private void FillBasedOnPlant(Plant? plant)
        {
            if (plant == null) return;


            foreach (Abiotiek abio in plant.Abiotieks)
            {
                foreach (RadioButton rbAb in AbioControlsBezonning)
                {
                    if (abio.Bezonning != null && (rbAb as RadioButton).Content.ToString().ToLower() == abio.Bezonning.ToLower())
                    {
                        rbAb.IsChecked = true;
                    }
                }

                foreach (RadioButton rbAb in AbioControlsVochtbehoefte)
                {
                    if (abio.Vochtbehoefte != null && (rbAb as RadioButton).Content.ToString().ToLower() == abio.Vochtbehoefte.ToLower())
                    {
                        rbAb.IsChecked = true;
                    }
                }

                foreach (RadioButton rbAb in AbioControlsVoedingsbehoefte)
                {
                    if (abio.Voedingsbehoefte != null && (rbAb as RadioButton).Content.ToString().ToLower() == abio.Voedingsbehoefte.ToLower())
                    {
                        rbAb.IsChecked = true;
                    }
                }

                foreach (RadioButton rbAb in AbioControlsGrondsoort)
                {
                    if (abio.Grondsoort != null && (rbAb as RadioButton).Content.ToString().ToLower() == abio.Grondsoort.ToLower())
                    {
                        rbAb.IsChecked = true;
                    }
                }

                foreach (RadioButton rbAb in AbioControlsReactieAntagonischeOmg)
                {
                    if (abio.AntagonischeOmgeving != null && (rbAb as RadioButton).Content.ToString().ToLower() == abio.AntagonischeOmgeving.ToLower())
                    {
                        rbAb.IsChecked = true;
                    }
                }
            }

            foreach (AbiotiekMulti abioMulti in plant.AbiotiekMultis)
            {
                foreach (CheckBox rbAb in AbioControlsHabitat)
                {
                    if (abioMulti.Waarde != null && (rbAb as CheckBox).Content.ToString().ToLower() == abioMulti.Waarde.ToLower())
                    {
                        rbAb.IsChecked = true;

                    }
                }
            }
        }

        #endregion


       

        #region 
        //J: i took the code and split it into different functions for binding 
        //private void CreateControls()
        //{
        //AbioControls = new ObservableCollection<UIElement>();
        //foreach (AbioBezonning ab in _dao.getAllTypes())
        //{
        //    //content and name are propertys from AbioBezonning
        //    CheckBox cb = new CheckBox { Content = ab.Naam, Uid = $"{ab.Id}" };
        //    AbioControls.Add(cb);
        //}

        //foreach (AbioVochtbehoefte av in _dao.getAllTypesVochtbehoefte())
        //{
        //    CheckBox cbv = new CheckBox { Content = av.Vochtbehoefte, Uid = $"{av.Id}" };
        //    AbioControls.Add(cbv);
        //}

        //foreach (AbioVoedingsbehoefte avb in _dao.getAllTypesVoedingsbehoefte())
        //{
        //    CheckBox cbvb = new CheckBox { Content = avb.Voedingsbehoefte, Uid = $"{avb.Id}" };
        //    AbioControls.Add(cbvb);
        //}

        //foreach (AbioGrondsoort ag in _dao.getAllTypesGrondsoort())
        //{
        //    CheckBox cbg = new CheckBox { Content = ag.Grondsoort, Uid = $"{ag.Id}" };
        //    AbioControls.Add(cbg);
        //}

        //foreach (AbioReactieAntagonischeOmg aa in _dao.getAllTypesOmgeving())
        //{
        //    CheckBox cba = new CheckBox { Content = aa.Antagonie, Uid = $"{aa.Id}" };
        //    AbioControls.Add(cba);
        //}

        //foreach (AbioHabitat ah in _dao.getAllTypesHabitat())
        //{
        //    CheckBox cbh = new CheckBox { Content = ah.Afkorting, Uid = $"{ah.Id}" };
        //    AbioControls.Add(cbh);
        //}


        //}
        #endregion


        //geschreven door christophe, op basis van een voorbeeld van owen
        //#region CheckboxGrondsoort


        //private bool _selectedCheckBoxGrondsoortGB1;
        //public bool SelectedCheckBoxGrondsoortGB1
        //{
        //    get { return _selectedCheckBoxGrondsoortGB1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortGB1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortGB2;
        //public bool SelectedCheckBoxGrondsoortGB2
        //{
        //    get { return _selectedCheckBoxGrondsoortGB2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortGB2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortGB3;
        //public bool SelectedCheckBoxGrondsoortGB3
        //{
        //    get { return _selectedCheckBoxGrondsoortGB3; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortGB3 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOP1;
        //public bool SelectedCheckBoxGrondsoortOP1
        //{
        //    get { return _selectedCheckBoxGrondsoortOP1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOP1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOP1B;
        //public bool SelectedCheckBoxGrondsoortOP1B
        //{
        //    get { return _selectedCheckBoxGrondsoortOP1B; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOP1B = value;
        //        OnPropertyChanged();
        //    }
        //}


        //private bool _selectedCheckBoxGrondsoortOP2;
        //public bool SelectedCheckBoxGrondsoortOP2
        //{
        //    get { return _selectedCheckBoxGrondsoortOP2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOP2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOP2B;
        //public bool SelectedCheckBoxGrondsoortOP2B
        //{
        //    get { return _selectedCheckBoxGrondsoortOP2B; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOP2B = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOP3;
        //public bool SelectedCheckBoxGrondsoortOP3
        //{
        //    get { return _selectedCheckBoxGrondsoortOP3; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOP3 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOP3B;
        //public bool SelectedCheckBoxGrondsoortOP3B
        //{
        //    get { return _selectedCheckBoxGrondsoortOP3B; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOP3B = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortSH1;
        //public bool SelectedCheckBoxGrondsoortSH1
        //{
        //    get { return _selectedCheckBoxGrondsoortSH1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortSH1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortSH2;
        //public bool SelectedCheckBoxGrondsoortSH2
        //{
        //    get { return _selectedCheckBoxGrondsoortSH2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortSH2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortB1;
        //public bool SelectedCheckBoxGrondsoortB1
        //{
        //    get { return _selectedCheckBoxGrondsoortB1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortB1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortB2;
        //public bool SelectedCheckBoxGrondsoortB2
        //{
        //    get { return _selectedCheckBoxGrondsoortB2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortB2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortB3;
        //public bool SelectedCheckBoxGrondsoortB3
        //{
        //    get { return _selectedCheckBoxGrondsoortB3; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortB3 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortGR1;
        //public bool SelectedCheckBoxGrondsoortGR1
        //{
        //    get { return _selectedCheckBoxGrondsoortGR1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortGR1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortGR2;
        //public bool SelectedCheckBoxGrondsoortGR2
        //{
        //    get { return _selectedCheckBoxGrondsoortGR2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortGR2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortH1;
        //public bool SelectedCheckBoxGrondsoortH1
        //{
        //    get { return _selectedCheckBoxGrondsoortH1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortH1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortH2;
        //public bool SelectedCheckBoxGrondsoortH2
        //{
        //    get { return _selectedCheckBoxGrondsoortH2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortH2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortST1;
        //public bool SelectedCheckBoxGrondsoortST1
        //{
        //    get { return _selectedCheckBoxGrondsoortST1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortST1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortST2;
        //public bool SelectedCheckBoxGrondsoortST2
        //{
        //    get { return _selectedCheckBoxGrondsoortST2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortST2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortBR1;
        //public bool SelectedCheckBoxGrondsoortBR1
        //{
        //    get { return _selectedCheckBoxGrondsoortBR1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortBR1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortBR2;
        //public bool SelectedCheckBoxGrondsoortBR2
        //{
        //    get { return _selectedCheckBoxGrondsoortBR2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortBR2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortBR3;
        //public bool SelectedCheckBoxGrondsoortBR3
        //{
        //    get { return _selectedCheckBoxGrondsoortBR3; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortBR3 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOB1;
        //public bool SelectedCheckBoxGrondsoortOB1
        //{
        //    get { return _selectedCheckBoxGrondsoortOB1; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOB1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxGrondsoortOB2;
        //public bool SelectedCheckBoxGrondsoortOB2
        //{
        //    get { return _selectedCheckBoxGrondsoortOB2; }

        //    set
        //    {
        //        _selectedCheckBoxGrondsoortOB2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //#endregion

        //#region CheckboxStrategie

        //private bool _selectedCheckBoxStrategieC;
        //public bool SelectedCheckBoxStrategieC
        //{
        //    get { return _selectedCheckBoxStrategieC; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieC = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxStrategieCR;
        //public bool SelectedCheckBoxStrategieCR
        //{
        //    get { return _selectedCheckBoxStrategieCR; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieCR = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxStrategieR;
        //public bool SelectedCheckBoxStrategieR
        //{
        //    get { return _selectedCheckBoxStrategieR; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieR = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxStrategieRS;
        //public bool SelectedCheckBoxStrategieRS
        //{
        //    get { return _selectedCheckBoxStrategieRS; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieRS = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxStrategieS;
        //public bool SelectedCheckBoxStrategieS
        //{
        //    get { return _selectedCheckBoxStrategieS; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieS = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxStrategieSC;
        //public bool SelectedCheckBoxStrategieSC
        //{
        //    get { return _selectedCheckBoxStrategieSC; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieSC = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxStrategieCSR;
        //public bool SelectedCheckBoxStrategieCSR
        //{
        //    get { return _selectedCheckBoxStrategieCSR; }

        //    set
        //    {
        //        _selectedCheckBoxStrategieCSR = value;
        //        MessageBox.Show(SelectedCheckBoxStrategieCSR.ToString());
        //        OnPropertyChanged();
        //    }
        //}

        //#endregion

        //#region CheckboxVoedingsbehoefte

        //private bool _selectedCheckBoxVoedingsbehoefteArm;
        //public bool SelectedCheckBoxVoedingsbehoefteArm
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteArm; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteArm = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxVoedingsbehoefteArmMatig;
        //public bool SelectedCheckBoxVoedingsbehoefteArmMatig
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteArmMatig; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteArmMatig = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxVoedingsbehoefteMatig;
        //public bool SelectedCheckBoxVoedingsbehoefteMatig
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteMatig; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteMatig = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk;
        //public bool SelectedCheckBoxVoedingsbehoefteMatigVoedselrijk
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxVoedingsbehoefteVoedselrijk;
        //public bool SelectedCheckBoxVoedingsbehoefteVoedselrijk
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteVoedselrijk; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteVoedselrijk = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent;
        //public bool SelectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _selectedCheckBoxVoedingsbehoefteIndifferent;
        //public bool SelectedCheckBoxVoedingsbehoefteIndifferent
        //{
        //    get { return _selectedCheckBoxVoedingsbehoefteIndifferent; }

        //    set
        //    {
        //        _selectedCheckBoxVoedingsbehoefteIndifferent = value;
        //        OnPropertyChanged();
        //    }
        //}


        //#endregion

        //#region CheckboxVochtBehoefte

        ////private bool _selectedCheckBoxVochtbehoefteDroog;
        ////public bool SelectedCheckBoxVochtbehoefteDroog
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteDroog; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteDroog = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        ////private bool _selectedCheckBoxVochtbehoefteDroogFris;
        ////public bool SelectedCheckBoxVochtbehoefteDroogFris
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteDroogFris; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteDroogFris = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        ////private bool _selectedCheckBoxVochtbehoefteFris;
        ////public bool SelectedCheckBoxVochtbehoefteFris
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteFris; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteFris = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        ////private bool _selectedCheckBoxVochtbehoefteFrisVochtig;
        ////public bool SelectedCheckBoxVochtbehoefteFrisVochtig
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteFrisVochtig; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteFrisVochtig = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        ////private bool _selectedCheckBoxVochtbehoefteVochtig;
        ////public bool SelectedCheckBoxVochtbehoefteVochtig
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteVochtig; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteVochtig = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        ////private bool _selectedCheckBoxVochtbehoefteVochtigNat;
        ////public bool SelectedCheckBoxVochtbehoefteVochtigNat
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteVochtigNat; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteVochtigNat = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        ////private bool _selectedCheckBoxVochtbehoefteNat;
        ////public bool SelectedCheckBoxVochtbehoefteNat
        ////{
        ////    get { return _selectedCheckBoxVochtbehoefteNat; }

        ////    set
        ////    {
        ////        _selectedCheckBoxVochtbehoefteNat = value;
        ////        OnPropertyChanged();
        ////    }
        ////}

        //#endregion




        
        #endregion



        /// <summary>
        ///
        /// Click event to add Abiotic
        /// </summary>


        public void AddAbiotiekClick()
        {
            
            
    
            //the element which will be given into the database  - Imran
            string abioBezonning=null;

            string abioGrondsoort=null;

            string abioVochtBehoefte = null;
            string abioVoedingsBehoefte = null;
            string AbioReactieAntagonischeOmg = null;

            List<string> abioHabitat = new List<string>();

            //go over each element in generated radiobutton listbox and give back the element that is checked - Imran
            foreach (RadioButton item in AbioControlsBezonning)
            {
                if ((bool)item.IsChecked)
                {
                    
                    abioBezonning = item.Content.ToString();
                    
                }
            }


            foreach (RadioButton item in AbioControlsGrondsoort)
            {
                if ((bool)item.IsChecked)
                {
                    abioGrondsoort = item.Content.ToString();
                }
            }


            foreach (RadioButton item in AbioControlsVochtbehoefte)
            {
                if ((bool)item.IsChecked)
                {
                    abioVochtBehoefte = item.Content.ToString();
                }
            }

            foreach (RadioButton item in AbioControlsVoedingsbehoefte)
            {
                if ((bool)item.IsChecked)
                {
                    abioVoedingsBehoefte = item.Content.ToString();
                }
            }

            foreach (RadioButton item in AbioControlsReactieAntagonischeOmg)
            {
                if ((bool)item.IsChecked)
                {
                    AbioReactieAntagonischeOmg = item.Content.ToString();
                }
            }



            
            
            //looks at the checkboxes and for each item it will add it to the string list-Imran
                foreach (CheckBox item in AbioControlsHabitat)
            {
                if ((bool)item.IsChecked)
                {
                    abioHabitat.Add(item.Content.ToString()); 
                }
            }



            //the assigned variables shall be eventually addded to database - Imran
            _addAbiotiekService.AddAbiotiekButton(abioBezonning, abioGrondsoort, abioVochtBehoefte, abioVoedingsBehoefte, AbioReactieAntagonischeOmg);


            _addAbiotiekMultiService.AddAbiotiekMultiButton(abioHabitat);

            

        }


        

    }
}