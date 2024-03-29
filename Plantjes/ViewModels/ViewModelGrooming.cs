﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels;
using Plantjes.Dao.DAOdb;
using GalaSoft.MvvmLight.Ioc;
using System.Windows;
using Plantjes.Models.Db;
using System.Windows.Controls;

namespace Plantjes.ViewModels
{
    public class ViewModelGrooming : ViewModelBase
    {
        private DAOBeheerMaand _dao;
        private ObservableCollection<UIElement> _beheermaandCollection;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();
        BeheerMaand beheermaandObject = new BeheerMaand();
        List<string> subListForMonths = new List<string>();
        
        private string _plantName;

        public ObservableCollection<UIElement> BeheermaandCollection
        {
            get { return _beheermaandCollection; }
            set { _beheermaandCollection = value; }
        }
        public string plantName
        {
            get { return _plantName; }
            set { _plantName = value; }
        }

        public ViewModelGrooming(IDetailService detailservice)
        {
            this._dao = SimpleIoc.Default.GetInstance<DAOBeheerMaand>();
            GetMonthsFromBeheerMaand();
            CreateControlsMaanden();
            plantName = FillLabelWithNamePlant(_searchService.getSelectedPlant());

            cmbBeheerdaad = new ObservableCollection<string>();
            fillComboBoxBeheerdaad();
        }

        public override void Load()
        {
            plantName = FillLabelWithNamePlant(_searchService.getSelectedPlant());
        }


        #region J: function to get only the months and not all the properties from beheermaand  
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
        #endregion

        private void CreateControlsMaanden()
        {
            BeheermaandCollection = new ObservableCollection<UIElement>();

            foreach (var prop in subListForMonths)
            {
                CheckBox cbm = new CheckBox { Content = prop.ToString() };
                BeheermaandCollection.Add(cbm);
            }

        }






        public ObservableCollection<string> cmbBeheerdaad { get; set; }

        public void fillComboBoxBeheerdaad()
        {
            var list = _dao.FillBeheerdaad().ToList();

            
                foreach (var item in list)
                {
                    //if (item != null)
                    //{
                        cmbBeheerdaad.Add(item.Beheerdaad);
                    //}
                    
                }
            
            
        }

        private string _selectedBeheerdaad;

        public string SelectedBeheer_Maand
        {
            get { return _selectedBeheerdaad; }
            set
            {
                _selectedBeheerdaad = value;
                OnPropertyChanged();

            }
        }

        #region Binding checkboxen Beheerdaad maand

        private bool _selectedCheckBoxJan;

        public bool SelectedCheckBoxJan
        {
            get { return _selectedCheckBoxJan; }

            set
            {
                _selectedCheckBoxJan = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxFeb;
        public bool SelectedCheckBoxFeb
        {
            get { return _selectedCheckBoxFeb; }

            set
            {
                _selectedCheckBoxFeb = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxMar;
        public bool SelectedCheckBoxMar
        {
            get { return _selectedCheckBoxMar; }

            set
            {
                _selectedCheckBoxMar = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxApr;
        public bool SelectedCheckBoxApr
        {
            get { return _selectedCheckBoxApr; }

            set
            {
                _selectedCheckBoxApr = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxMay;
        public bool SelectedCheckBoxFMay
        {
            get { return _selectedCheckBoxMay; }

            set
            {
                _selectedCheckBoxMay = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxJun;
        public bool SelectedCheckBoxJun
        {
            get { return _selectedCheckBoxJun; }

            set
            {
                _selectedCheckBoxJun = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxJul;
        public bool SelectedCheckBoxJul
        {
            get { return _selectedCheckBoxJul; }

            set
            {
                _selectedCheckBoxJul = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxAug;
        public bool SelectedCheckBoxAug
        {
            get { return _selectedCheckBoxAug; }

            set
            {
                _selectedCheckBoxAug = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxSep;
        public bool SelectedCheckBoxSep
        {
            get { return _selectedCheckBoxSep; }

            set
            {
                _selectedCheckBoxSep = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxOct;
        public bool SelectedCheckBoxOct
        {
            get { return _selectedCheckBoxOct; }

            set
            {
                _selectedCheckBoxOct = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxNov;
        public bool SelectedCheckBoxNov
        {
            get { return _selectedCheckBoxNov; }

            set
            {
                _selectedCheckBoxNov = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedCheckBoxDec;
        public bool SelectedCheckBoxDec
        {
            get { return _selectedCheckBoxDec; }

            set
            {
                _selectedCheckBoxDec = value;
                OnPropertyChanged();
            }
        }












        #endregion
    }
}
