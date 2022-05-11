using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Plantjes.Models.Db;
using XAct;

namespace Plantjes.Dao.DAOdb
{
    public class DAONieuwWachtwoord : DAOGeneric
    {

        public DAONieuwWachtwoord() : base()
        {
            //ctor
        }


        


        public Gebruiker GetGebruikerWithId(int gebruikerId)
        {

            //var gebruiker = Context.Gebruikers.SingleOrDefault(g => g.Emailadres == userEmail);

            // var gebruiker = Context.Gebruikers.Include(g => g.Rol).SingleOrDefault(g => g.Id == userEmail);


            //var gebruiker = Context.Gebruikers.SingleOrDefault(g=>g.Id.Equals(gebruikerId));
            
            var gebruiker = Context.Gebruikers.SingleOrDefault(g=>g.Id.Equals(gebruikerId));


            return gebruiker;

        }

        


        public void RegisterNewPassword(string password, int id)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);



            // hier gebeurt de zogezegde database verandering maar ik moet nog het idee krijgen
            //dat hetzelfde gebruiker is , er is hier nog niets
            //--

            //id wordt nu doorgegeven
            var gebruiker = GetGebruikerWithId(id);
                
          


            //var gebruiker = GetGebruikerWithPasswordl();



            //Hier moet de registratie gebeuren , maar ik moet de gebruiker bij me ophalen


            //Context.Gebruikers.Update()

            //Context.Gebruikers.Add(gebruiker);

            //nu eerst met de Id de database updaten in de DAO

           

            //in de context contacteer ik de database en update het, maar hoe?




            //Context.Gebruikers.EntityType;

          

            Context.Gebruikers.Update(GetGebruikerWithId(id)).Entity.HashPaswoord = passwordHashed;

            Context.SaveChanges();
        }




    }
}