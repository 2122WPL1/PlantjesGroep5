using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels.Interfaces
{
    public interface IAddPlantService
    {

        public void AddPlantButton(string naam, TfgsvType type, TfgsvFamilie familie, TfgsvGeslacht geslacht, TfgsvSoort soort, TfgsvVariant variant);




    }
}
