﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels;

namespace Plantjes.ViewModels
{
    //geschreven door kenny adhv een voorbeeld van roy
    //herschreven door kenny voor gebruik met ioc
    public class ViewModelRepo
    {   //singleton pattern
        private static SimpleIoc iocc = SimpleIoc.Default;
        //private static ViewModelRepo instance;

        private Dictionary<string, ViewModelBase> _viewModels = new Dictionary<string, ViewModelBase>();
       
        private ViewModelNameResult viewModelNameResult = iocc.GetInstance<ViewModelNameResult>();
        private ViewModelRegister viewModelRegister = iocc.GetInstance<ViewModelRegister>();
        private ViewModelCommensalisme viewModelHabitat = iocc.GetInstance<ViewModelCommensalisme>();
        private ViewModelFenotype viewModelBloom = iocc.GetInstance<ViewModelFenotype>();
        private ViewModelAbiotiek viewModelGrow = iocc.GetInstance<ViewModelAbiotiek>();
        private ViewModelAppearance viewModelAppearance = iocc.GetInstance<ViewModelAppearance>();
        private ViewModelGrooming viewModelGrooming = iocc.GetInstance<ViewModelGrooming>();
        private ViewModelImages viewModelImages = iocc.GetInstance<ViewModelImages>();
        private ViewModelRequest viewModelRequest = iocc.GetInstance<ViewModelRequest>();
        private ViewModelUserManagement viewModelUserManagement = iocc.GetInstance<ViewModelUserManagement>();

        

        //werkt niet, vast iets met de IOC container
        private ViewModelChangePassword viewModelChangePassword = iocc.GetInstance<ViewModelChangePassword>();
        

        public ViewModelRepo()
        {
            //hier een extra lijn code per user control
            _viewModels.Add("VIEWNAME", viewModelNameResult);
            _viewModels.Add("VIEWHABITAT", viewModelHabitat);
            _viewModels.Add("VIEWBLOOM", viewModelBloom);
            _viewModels.Add("VIEWGROW", viewModelGrow);
            _viewModels.Add("VIEWAPPEARANCE", viewModelAppearance);
            _viewModels.Add("VIEWGROOMING",viewModelGrooming);
            _viewModels.Add("VIEWREGISTER", viewModelRegister);
            _viewModels.Add("VIEWIMAGES", viewModelImages);
            _viewModels.Add("VIEWREQUEST", viewModelRequest);
            _viewModels.Add("VIEWUSERMANAGEMENT", viewModelUserManagement);
            _viewModels.Add("CHANGEPASSWORD", viewModelChangePassword);

           
        }
        //
        public ViewModelBase GetViewModel(string modelName)
        {
            ViewModelBase result;
            var ok = this._viewModels.TryGetValue(modelName, out result);
            return ok ? result : null;
        }
    }

}
