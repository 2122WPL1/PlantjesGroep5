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

namespace Plantjes.ViewModels
{
    public class ViewModelAbiotiek : ViewModelBase
    {
        private DAOPlant _plantId;
        private DAOAbiotiek _dao;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();
        private IDetailService _detailService = iocc.GetInstance<IDetailService>();

        private ObservableCollection<UIElement> _AbioControlsBezonning, _AbioControlsVochtbehoefte, _AbioControlsVoedingsbehoefte, 
                _AbioControlsGrondsoort, _AbioControlsReactieAntagonischeOmg, _AbioControlsHabitat;

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

        //J: const
        public ViewModelAbiotiek(IDetailService detailservice)
        {
            _detailService = detailservice;
            this._dao = SimpleIoc.Default.GetInstance<DAOAbiotiek>();
            CreateControlsBezonning();
            CreateControlsVochtbehoefte();
            CreateControlsVoedingsbehoefte();
            CreateControlsGrondsoort();
            CreateControlsReactieAntagonischeOmg();
            CreateControlsReactieAbioHabitat();

            //CreateControls();
        }
        public override void Load()
        {
            FillBasedOnPlant(_searchService.getSelectedPlant());
        }

        
        #region J: Function to create contols(checkboxes) from what we know in the tables from database 
        private void CreateControlsBezonning()
        {
            AbioControlsBezonning = new ObservableCollection<UIElement>();

            foreach (AbioBezonning ab in _dao.getAllTypes())
            {
                CheckBox cb = new CheckBox { Content = ab.Naam, Uid = $"{ab.Id}" };
                AbioControlsBezonning.Add(cb);
            }
        }

        private void CreateControlsVochtbehoefte()
        {
            AbioControlsVochtbehoefte = new ObservableCollection<UIElement>();

            foreach (AbioVochtbehoefte av in _dao.getAllTypesVochtbehoefte())
            {
                CheckBox cbv = new CheckBox { Content = av.Vochtbehoefte, Uid = $"{av.Id}" };
                AbioControlsVochtbehoefte.Add(cbv);
            }
        }

        private void CreateControlsVoedingsbehoefte()
        {
            AbioControlsVoedingsbehoefte = new ObservableCollection<UIElement>();

            foreach (AbioVoedingsbehoefte avb in _dao.getAllTypesVoedingsbehoefte())
            {
                CheckBox cbvb = new CheckBox { Content = avb.Voedingsbehoefte, Uid = $"{avb.Id}" };
                AbioControlsVoedingsbehoefte.Add(cbvb);
            }

        }

        private void CreateControlsGrondsoort()
        {
            AbioControlsGrondsoort = new ObservableCollection<UIElement>();

            foreach (AbioVoedingsbehoefte avb in _dao.getAllTypesVoedingsbehoefte())
            {
                CheckBox cbvb = new CheckBox { Content = avb.Voedingsbehoefte, Uid = $"{avb.Id}" };
                AbioControlsGrondsoort.Add(cbvb);
            }

        }

        private void CreateControlsReactieAntagonischeOmg()
        {
            AbioControlsReactieAntagonischeOmg = new ObservableCollection<UIElement>();

            foreach (AbioReactieAntagonischeOmg aa in _dao.getAllTypesOmgeving())
            {
                CheckBox cba = new CheckBox { Content = aa.Antagonie, Uid = $"{aa.Id}" };
                AbioControlsReactieAntagonischeOmg.Add(cba);
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


        #region J: function to fill checkboxes based on the details from search result
        private void FillBasedOnPlant(Plant? plant)
        {
            if (plant == null) return;


            foreach (Abiotiek abio in plant.Abiotieks)
            {
                foreach (CheckBox c in AbioControlsBezonning)
                {
                    if (abio.Bezonning != null && (c as CheckBox).Content.ToString().ToLower() == abio.Bezonning.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }

                foreach (CheckBox c in AbioControlsVochtbehoefte)
                {
                    if (abio.Vochtbehoefte != null && (c as CheckBox).Content.ToString().ToLower() == abio.Vochtbehoefte.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }

                foreach (CheckBox c in AbioControlsVoedingsbehoefte)
                {
                    if (abio.Voedingsbehoefte != null && (c as CheckBox).Content.ToString().ToLower() == abio.Voedingsbehoefte.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }

                foreach (CheckBox c in AbioControlsGrondsoort)
                {
                    if (abio.Grondsoort != null && (c as CheckBox).Content.ToString().ToLower() == abio.Grondsoort.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }

                foreach (CheckBox c in AbioControlsReactieAntagonischeOmg)
                {
                    if (abio.AntagonischeOmgeving != null && (c as CheckBox).Content.ToString().ToLower() == abio.AntagonischeOmgeving.ToLower())
                    {
                        c.IsChecked = true;
                    }
                }
            }

            foreach (AbiotiekMulti abioMulti in plant.AbiotiekMultis)
            {
                foreach (CheckBox c in AbioControlsHabitat)
                {
                    if (abioMulti.Waarde != null && (c as CheckBox).Content.ToString().ToLower() == abioMulti.Waarde.ToLower())
                    {
                        c.IsChecked = true;

                    }
                }
            }








            //for each checkbox
            //foreach (Control c in AbioControls)
            //{

            //if (abio.Bezonning != null && (c as CheckBox).Content.ToString().ToLower() == abio.Bezonning.ToLower())
            //{
            //    c.Background = Brushes.LightBlue;
            //}


            //if (abio.Vochtbehoefte != null && (c as CheckBox).Content.ToString().ToLower() == abio.Vochtbehoefte.ToLower())
            //{
            //    c.Background = Brushes.LightBlue;
            //}


            //if (abio.Voedingsbehoefte != null && (c as CheckBox).Content.ToString().ToLower() == abio.Voedingsbehoefte.ToLower())
            //{
            //    c.Background = Brushes.LightBlue;
            //}


            //if (abio.Grondsoort != null && (c as CheckBox).Content.ToString().ToLower() == abio.Grondsoort.ToLower())
            //{
            //    c.Background = Brushes.LightBlue;
            //}


            //if (abio.AntagonischeOmgeving != null && (c as CheckBox).Content.ToString().ToLower() == abio.AntagonischeOmgeving.ToLower())
            //{
            //    c.Background = Brushes.LightBlue;
            //}

            //}

            //foreach (AbiotiekMulti abioMulti in plant.AbiotiekMultis)
            //{
            //    foreach (Control c in AbioControls)
            //    {
            //        if (abioMulti.Waarde != null && (c as CheckBox).Content.ToString().ToLower() == abioMulti.Waarde.ToLower())
            //        {
            //            c.Background = Brushes.LightBlue;

            //        }
            //    }
            //}
        }
    }
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



}
