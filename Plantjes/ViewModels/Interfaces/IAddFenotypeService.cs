using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels.Interfaces
{
    public interface IAddFenotypeService
    {
        public void AddFenotypeButton(int fenoBladgrootte, string fenoBladvorm, string fenoRatioBloeiBlad, string fenoSpruitfenologie, string fenoBloeiwijze, string fenoHabitus, string fenoLevensvorm);
    }
}
