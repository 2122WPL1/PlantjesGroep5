using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plantjes.ViewModels
{

    //erover van inotify... 
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                Task.Run(() => PropertyChanged(this, new PropertyChangedEventArgs(propName)));
            }
        }

        public virtual void Load() { }

        public string FillLabelWithNamePlant(Plant? plant)
        {
            string message = String.Empty;

            if (plant != null)
            {
                message = $"plant: {plant.Fgsv}";
            }

            return message;


        }





    }
}
