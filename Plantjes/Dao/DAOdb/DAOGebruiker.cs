using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Plantjes.Dao.DAOdb
{
    public class DAOGebruiker
    {
        private static readonly DAOGebruiker instance = new DAOGebruiker();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOGebruiker()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }
        
        public static DAOGebruiker Instance()
        {
            return instance;
        }

        public Gebruiker GetGebruikerWithEmail(string userEmail)
        {
            var gebruiker = context.Gebruikers.SingleOrDefault(g => g.Emailadres == userEmail);
            return gebruiker;

        }

        public void RegisterUser(string vivesNr, string firstName, string lastName, string rol, string emailadres, string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            var gebruiker = new Gebruiker()
            {
                Vivesnr = vivesNr,
                Voornaam = firstName,
                Achternaam = lastName,
                Rol = rol,
                Emailadres = emailadres,
                HashPaswoord = passwordHashed
            };
            context.Gebruikers.Add(gebruiker);
            _ = context.SaveChanges();
        }

        //written by kenny
        public List<Gebruiker> getAllGebruikers()
        {
            var gebruiker = context.Gebruikers.ToList();
            return gebruiker;
        }
        public bool CheckIfEmailAlreadyExists(string email)
        {
            bool result = false;
            if (instance.GetGebruikerWithEmail(email) == null)
            {
                result = true;
            }

            return result;
        }
    }
}
