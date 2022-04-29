using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
   
    public partial class Gebruiker
    {
        public Gebruiker()
        {
            UpdatePlants = new HashSet<UpdatePlant>();
        }

        public int Id { get; set; }
        public string Vivesnr { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int RolId { get; set; }
        public string Emailadres { get; set; }
        public DateTime? LastLogin { get; set; }
        public byte[] HashPaswoord { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<UpdatePlant> UpdatePlants { get; set; }


        //methode om student terug te retourneren
        public static Gebruiker FromLine(string line)
        {
            string[] vals = line.Split(";");
            Gebruiker student =  new Gebruiker()
            {
                Vivesnr = vals[0],
                Voornaam = vals[1],
                Achternaam = vals[2],
                Emailadres = vals[5]
            };
            return student;
        }
    }
}
