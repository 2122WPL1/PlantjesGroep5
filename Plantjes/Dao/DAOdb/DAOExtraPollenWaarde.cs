using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOExtraPollenWaarde
    {
        private static readonly DAOExtraPollenWaarde instance = new DAOExtraPollenWaarde();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOExtraPollenWaarde()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOExtraPollenWaarde Instance()
        {
            return instance;
        }

        public List<ExtraPollenwaarde> FillExtraPollenwaardes()
        {
            var selection = context.ExtraPollenwaardes.ToList();
            return selection;
        }

        
    }
}
