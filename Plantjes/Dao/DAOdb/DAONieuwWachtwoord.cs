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
        //Imran
        public DAONieuwWachtwoord() : base()
        {
            //ctor
        }


       

        //Gives back a  User based on ID -Imran
        public Gebruiker GetGebruikerWithId(int gebruikerId)
        {

         

            //looks for the first user that matches the id or returns a default value-I
            var gebruiker = Context.Gebruikers.SingleOrDefault(g=>g.Id.Equals(gebruikerId));


            return gebruiker;

        }

        

        //To register new Password which gets encrypted-I
        public void RegisterNewPassword(string password, int id)
        {
            //necessary components to Hash the password
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);



           //Call upon the function to get the user based on id-Imran
            var gebruiker = GetGebruikerWithId(id);
                
          



          
          
            
            //Update the database for the user-I
            Context.Gebruikers.Update(GetGebruikerWithId(id)).Entity.HashPaswoord = passwordHashed;
            
            Context.SaveChanges();
        }




    }
}