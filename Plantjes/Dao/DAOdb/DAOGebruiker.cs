using Microsoft.EntityFrameworkCore;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XSystem.Security.Cryptography;

namespace Plantjes.Dao.DAOdb
{
    public class DAOGebruiker : DAOGeneric
    {
        public DAOGebruiker() : base()
        {
            //ctor
        }

        public Gebruiker GetGebruikerWithEmail(string userEmail)
        {

            //var gebruiker = Context.Gebruikers.SingleOrDefault(g => g.Emailadres == userEmail);

            var gebruiker = Context.Gebruikers.Include(g => g.Rol).SingleOrDefault(g => g.Emailadres == userEmail);

            return gebruiker;
        }


        public List<Gebruiker> getAllGebruikers()
        {
            var gebruiker = Context.Gebruikers.ToList();
            return gebruiker;
        }

        public void RegisterUser(string vivesNr, string firstName, string lastName, int rolid, string emailadres, string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            var gebruiker = new Gebruiker()
            {
                Vivesnr = vivesNr,
                Voornaam = firstName,
                Achternaam = lastName,
                RolId = rolid,
                Emailadres = emailadres,
                HashPaswoord = passwordHashed
            };
            Context.Gebruikers.Add(gebruiker);
            Context.SaveChanges();
        }

        
        public bool CheckIfEmailAlreadyExists(string email)
        {
            bool result = false;
            if (GetGebruikerWithEmail(email) == null)
            {
                result = true;
            }

            return result;
        }

     



        public void AddStudents(List<Gebruiker> students)
        {
            // is vivesnr er al in?
            // zo ja, negeer gebruiker,
            // zo niet, voeg toe.


            foreach (var student in students)
            {
                if (student.Vivesnr != null && student.Vivesnr.Length <= 7 && !Context.Gebruikers.Any(s => s.Emailadres == student.Emailadres))
                {
                    student.Vivesnr = student.Vivesnr.PadLeft(7, '0');
                    student.Vivesnr = student.Vivesnr.PadLeft(8, 'r');

                    var password = student.Vivesnr;
                    var passwordBytes = Encoding.ASCII.GetBytes(password);
                    var md5Hasher = new MD5CryptoServiceProvider();
                    var passwordHashed = md5Hasher.ComputeHash(passwordBytes);
                    student.HashPaswoord = passwordHashed;

                    student.RolId = 1;
                    if (student.Vivesnr.Length == 8 && student.Vivesnr.Contains("r"))
                    {
                        Context.Gebruikers.Add(student);
                        Context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Vives nummer is ongeldig.");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Vives nummer is al in gebruik.");
                }
            }

            
            //if (string.IsNullOrWhiteSpace(student.Vivesnr))
            //{
                
            //}

            //For faults handling: 
            //if (student.Vivesnr.Length == 7)
            //{
            //    student.Vivesnr += "r";

            //}
            //if (
            //{
            //    while (student.Vivesnr.Length < 7)
            //    {
            //        student.Vivesnr += "0";
            //        student.Vivesnr += "r";
            //    }
            //}

        }
    }
}
