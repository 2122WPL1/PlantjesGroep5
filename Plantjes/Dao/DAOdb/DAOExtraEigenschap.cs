using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOExtraEigenschap
    {
        private static readonly DAOExtraEigenschap instance = new DAOExtraEigenschap();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOExtraEigenschap()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }

        public static DAOExtraEigenschap Instance()
        {
            return instance;
        }

        public List<ExtraEigenschap> GetAllExtraEigenschap()
        {
            var extraEigenschap = context.ExtraEigenschaps.ToList();
            return extraEigenschap;
        }
    }
}
