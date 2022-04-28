using Plantjes.Dao;
using Plantjes.Dao.DAOdb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels.Interfaces
{
    public interface IDAOGeneric : INotifyPropertyChanged
    {
        public static DAOGeneric instance = new DAOGeneric();

        protected static plantenContext Context;
        //static over alles gaat bestaand en geen instance/ object
        //plantenContext is partial => static instance aanmaken maar is geen echt instance




        public void SaveChanges();


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        { }
        
    }
}
