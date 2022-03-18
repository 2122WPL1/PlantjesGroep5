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

        public static plantenContext _context;

        //public plantenContext Context { get { return instance._context; } }




        public void SaveChanges();


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        { }
        
    }
}
