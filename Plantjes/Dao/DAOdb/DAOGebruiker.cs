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

            //For every student in the list of students you've read in db: 

            foreach (var student in students)

            {

                //Check if vivesnr is not null, has less or equal as 7 chars & if any of the student's email is already registered in db, NOT TRUE 

                //then we excute the new list, otherwise, not  

                if (student.Vivesnr != null && student.Vivesnr.Length <= 7 && !Context.Gebruikers.Any(s => s.Emailadres == student.Emailadres))

                {

                    //vivesnr isn't reached 7 chars yet: then we increase vivesnr to 7 chars with some 0's, starting from the left side 

                    student.Vivesnr = student.Vivesnr.PadLeft(7, '0');

                    student.Vivesnr = student.Vivesnr.PadLeft(8, 'r');

                    //first pw of a student is their r nr, so we will hash this to a hashed password 

                    var password = student.Vivesnr;

                    var passwordBytes = Encoding.ASCII.GetBytes(password);

                    var md5Hasher = new MD5CryptoServiceProvider();

                    var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

                    student.HashPaswoord = passwordHashed;

                    //give directly rolid = 1 = student for this list of student 

                    student.RolId = 1;

                    //check if vivesnr has 8 chars and contains r 

                    //We still have to work on here: some external students have a vivesnr starts with s *** 

                    if (student.Vivesnr.Length == 8 && (student.Vivesnr.Contains("r")))

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

                    MessageBox.Show($"Vives nummer: {student.Vivesnr} is al in gebruik.");

                }

            }

        }

    }
}

