using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao.DAOdb
{
    public class DAOAbiotiekMulti : DAOGeneric
    {
        public DAOAbiotiekMulti() : base()
        {
            //ctor
        }

        //Get a list of all the AbiotiekMulti types
        public List<AbiotiekMulti> GetAllAbiotieksMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filteren will take place in the ViewModel

            var abioMultiList = Context.AbiotiekMultis.ToList();

            return abioMultiList;
        }

        public IQueryable<AbiotiekMulti> filterAbiotiekMultiFromPlant(int selectedItem)
        {

            var selection = Context.AbiotiekMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }



        //de dao die toevoegd aan de database multiabiotiek
        public void AddPlantAbiotiekMulti(long PlantId, List<string> waarde)
        {

            //voor elke item in de lijst zal er een nieuwe abiotiek object gemaakt worden maar wel met dezelfde plantId fk (omdat het om de
            //zelfde plant gaat
            //Elke eigenschap is habitat dus geen rede om verder te zoeken
            foreach (string item in waarde)
            {
                AbiotiekMulti abioM = new AbiotiekMulti()
                {

                    PlantId = PlantId,
                    Waarde = item,

                    Eigenschap = "habitat"




                };

                //elke t
                //Update database Abiotieks -Imran
                Context.AbiotiekMultis.Add(abioM);




            }

          

            Context.SaveChanges();



        }
    }
}
