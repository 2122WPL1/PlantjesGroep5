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
    public class ViewModelHabitat : ViewModelBase
    {
        private DAOExtraPollenWaarde _daoPollen;
        private DAOExtraNectarWaarde _daoNectar;

        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _SearchService = iocc.GetInstance<ISearchService>();


        private ObservableCollection<UIElement> _Controls;

        public ObservableCollection<UIElement> CommControls
        {
            get { return _Controls; }
            set { _Controls = value; }
        }

        public override void Load()
        {
            FillBasedOnPlant(_SearchService.getSelectedPlant());
        }

        public ViewModelHabitat(IDetailService detailservice)
        {
            this._daoPollen = SimpleIoc.Default.GetInstance<DAOExtraPollenWaarde>();
            this._daoNectar = SimpleIoc.Default.GetInstance<DAOExtraNectarWaarde>();



            cmbPollenWaarde = new ObservableCollection<ExtraPollenwaarde>();
            cmbNectarWaarde = new ObservableCollection<ExtraNectarwaarde>();


            fillComboBoxPollenwaarde();
            fillComboBoxNectarwaarde();

        }

        //private void FillBasedOnPlant(Plant? plant)
        //{
        //    if (plant == null) return;


        //    foreach (Commensalisme comm in plant.Commensalismes)
        //    {
        //        //for each checkbox
        //        foreach (Control c in CommControls)
        //        {

        //            if (comm.Ontwikkelsnelheid != null && (c as CheckBox).Content.ToString().ToLower() == comm.Ontwikkelsnelheid.ToLower())
        //            {
        //                c.Background = Brushes.Blue;
        //            }


        //            if (comm.Strategie != null && (c as CheckBox).Content.ToString().ToLower() == comm.Vochtbehoefte.ToLower())
        //            {
        //                c.Background = Brushes.Blue;
        //            }


        //            if (comm.Social != null && (c as CheckBox).Content.ToString().ToLower() == comm.Voedingsbehoefte.ToLower())
        //            {
        //                c.Background = Brushes.LightBlue;
        //            }


        //            if (comm.Grondsoort != null && (c as CheckBox).Content.ToString().ToLower() == comm.Grondsoort.ToLower())
        //            {
        //                c.Background = Brushes.LightBlue;
        //            }


        //            if (abio.AntagonischeOmgeving != null && (c as CheckBox).Content.ToString().ToLower() == abio.AntagonischeOmgeving.ToLower())
        //            {
        //                c.Background = Brushes.LightBlue;
        //            }

        //        }

        //        foreach (AbiotiekMulti abioMulti in plant.AbiotiekMultis)
        //        {
        //            foreach (Control c in AbioControls)
        //            {
        //                if (abioMulti.Waarde != null && (c as CheckBox).Content.ToString().ToLower() == abioMulti.Waarde.ToLower())
        //                {
        //                    c.Background = Brushes.LightBlue;

        //                }
        //            }
        //        }
        //    }
        //}

        public ObservableCollection<ExtraPollenwaarde> cmbPollenWaarde { get; set; }
        public ObservableCollection<ExtraNectarwaarde> cmbNectarWaarde { get; set; }

        //geschreven door christophe, op basis van een voorbeeld van owen
        public void fillComboBoxPollenwaarde()
        {
            var list = _daoPollen.FillExtraPollenwaardes();

            foreach (var item in list)
            {

                cmbPollenWaarde.Add(item);

            }
        }

        private string _selectedPollenwaarde;

        public string SelectedPollenwaarde
        {
            get { return _selectedPollenwaarde; }
            set
            {
                _selectedPollenwaarde = value;
                OnPropertyChanged();

            }
        }

        public void fillComboBoxNectarwaarde()
        {
            var list = _daoNectar.FillExtraNectarwaardes();

            foreach (var item in list)
            {
                cmbNectarWaarde.Add(item);
            }
        }

        private string _selectedNectarwaarde;

        public string SelectedNectarwaarde
        {
            get { return _selectedNectarwaarde; }
            set
            {
                _selectedNectarwaarde = value;
                OnPropertyChanged();

            }
        }

        private string _selectedOntwikkelsnelheid;

        public string SelectedOntwikkelsnelheid
        {
            get { return _selectedOntwikkelsnelheid; }
            set
            {
                _selectedOntwikkelsnelheid = value;
                OnPropertyChanged();
            }
        }

        #region Binding checkboxen Habitat

        private string _selectedCheckBoxHabitat1;

        public string SelectedCheckBoxHabitat1
        {
            get { return _selectedCheckBoxHabitat1; }
            set
            {
                _selectedCheckBoxHabitat1 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxHabitat2;

        public string SelectedCheckBoxHabitat2
        {
            get { return _selectedCheckBoxHabitat2; }
            set
            {
                _selectedCheckBoxHabitat2 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxHabitat3;

        public string SelectedCheckBoxHabitat3
        {
            get { return _selectedCheckBoxHabitat3; }
            set
            {
                _selectedCheckBoxHabitat3 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxHabitat4;

        public string SelectedCheckBoxHabitat4
        {
            get { return _selectedCheckBoxHabitat4; }
            set
            {
                _selectedCheckBoxHabitat4 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxHabitat5;

        public string SelectedCheckBoxHabitat5
        {
            get { return _selectedCheckBoxHabitat5; }
            set
            {
                _selectedCheckBoxHabitat5 = value;
                MessageBox.Show(SelectedCheckBoxHabitat5.ToString());
                OnPropertyChanged();
            }
        }


        private string _selectedCheckBoxBezonningZ;

        public string SelectedCheckBoxBezonningZ
        {
            get { return _selectedCheckBoxBezonningZ; }
            set
            {
                _selectedCheckBoxBezonningZ = value;
               OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxBezonningZHS;

        public string SelectedCheckBoxBezonningZHS
        {
            get { return _selectedCheckBoxBezonningZHS; }
            set
            {
                _selectedCheckBoxBezonningZHS = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxBezonningHS;

        public string SelectedCheckBoxBezonningHS
        {
            get { return _selectedCheckBoxBezonningHS; }
            set
            {
                _selectedCheckBoxBezonningHS = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxBezonningHSS;

        public string SelectedCheckBoxBezonningHSS
        {
            get { return _selectedCheckBoxBezonningHSS; }
            set
            {
                _selectedCheckBoxBezonningHSS = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxBezonningS;

        public string SelectedCheckBoxBezonningS
        {
            get { return _selectedCheckBoxBezonningS; }
            set
            {
                _selectedCheckBoxBezonningS = value;
                OnPropertyChanged();
            }
        }
        

        private string _selectedCheckBoxSociabiliteitI;

        public string SelectedCheckBoxSociabiliteitI
        {
            get { return _selectedCheckBoxSociabiliteitI; }
            set
            {
                _selectedCheckBoxSociabiliteitI = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxSociabiliteitII;

        public string SelectedCheckBoxSociabiliteitII
        {
            get { return _selectedCheckBoxSociabiliteitII; }
            set
            {
                _selectedCheckBoxSociabiliteitII = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxSociabiliteitIII;

        public string SelectedCheckBoxSociabiliteitIII
        {
            get { return _selectedCheckBoxSociabiliteitIII; }
            set
            {
                _selectedCheckBoxSociabiliteitIII = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxSociabiliteitIV;

        public string SelectedCheckBoxSociabiliteitIV
        {
            get { return _selectedCheckBoxSociabiliteitIV; }
            set
            {
                _selectedCheckBoxSociabiliteitIV = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxSociabiliteitV;

        public string SelectedCheckBoxSociabiliteitV
        {
            get { return _selectedCheckBoxSociabiliteitV; }
            set
            {
                _selectedCheckBoxSociabiliteitV = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxBijvriendelijk;

        public string SelectedCheckBoxBijvriendelijk
        {
            get { return _selectedCheckBoxBijvriendelijk; }
            set
            {
                _selectedCheckBoxBijvriendelijk = value;
               OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxEetbaarKruidbaar;
        public string SelectedCheckBoxEetbaarKruidbaar
        {
            get { return _selectedCheckBoxEetbaarKruidbaar; }
            set
            {
                _selectedCheckBoxEetbaarKruidbaar = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxGeurend;
        public string SelectedCheckBoxGeurend
        {
            get { return _selectedCheckBoxGeurend; }
            set
            {
                _selectedCheckBoxGeurend = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxVlindervriendelijk;
        public string SelectedCheckBoxVlindervriendelijk
        {
            get { return _selectedCheckBoxVlindervriendelijk; }
            set
            {
                _selectedCheckBoxVlindervriendelijk = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBoxVorstgevoelig;
        public string SelectedCheckBoxVorstgevoelig
        {
            get { return _selectedCheckBoxVorstgevoelig; }
            set
            {
                _selectedCheckBoxVorstgevoelig = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox1;
        public string SelectedCheckBox1
        {
            get { return _selectedCheckBox1; }
            set
            {
                _selectedCheckBox1 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox2;
        public string SelectedCheckBox2
        {
            get { return _selectedCheckBox2; }
            set
            {
                _selectedCheckBox2 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox3;
        public string SelectedCheckBox3
        {
            get { return _selectedCheckBox3; }
            set
            {
                _selectedCheckBox3 = value;
                OnPropertyChanged();
            }
        }
        private string _selectedCheckBox4;
        public string SelectedCheckBox4
        {
            get { return _selectedCheckBox4; }
            set
            {
                _selectedCheckBox4 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox5;
        public string SelectedCheckBox5
        {
            get { return _selectedCheckBox5; }
            set
            {
                _selectedCheckBox5 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox6;
        public string SelectedCheckBox6
        {
            get { return _selectedCheckBox6; }
            set
            {
                _selectedCheckBox6 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox7;
        public string SelectedCheckBox7
        {
            get { return _selectedCheckBox7; }
            set
            {
                _selectedCheckBox7 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox8;
        public string SelectedCheckBox8
        {
            get { return _selectedCheckBox8; }
            set
            {
                _selectedCheckBox8 = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCheckBox9;
        public string SelectedCheckBox9
        {
            get { return _selectedCheckBox9; }
            set
            {
                _selectedCheckBox9 = value;
                MessageBox.Show(SelectedCheckBox9.ToString());
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
