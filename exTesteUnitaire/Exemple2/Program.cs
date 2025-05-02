using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple2
{
    public class Program
    {
        public string Login(string UserId, string Password)
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Password))
            {
                return "L'identifiant ou le mot de passe ne doit pas être vide.";
            }
            else
            {
                if (UserId == "Admin" && Password == "Admin")
                {
                    return "Bienvenue Admin.";
                }
                return "Identifiant ou mot de passe incorrecte.";
            }
        }
        public List<Employe> LesEmployes()
        {
            List<Employe> li = new List<Employe>
            {
                new Employe
                {
                    Id = 100,
                    Nom = "Jacques",
                    Genre = "male",
                    Salaire = 40000
                },
                new Employe
                {
                    Id = 101,
                    Nom = "Maria",
                    Genre = "Female",
                    Salaire = 50000
                },
                new Employe
                {
                    Id = 103,
                    Nom = "Bogdan",
                    Genre = "male",
                    Salaire = 40000
                },
                new Employe
                {
                    Id = 104,
                    Nom = "Frank",
                    Genre = "male",
                    Salaire = 23000
                },
                new Employe
                {
                    Id = 105,
                    Nom = "Isabelle",
                    Genre = "Female",
                    Salaire = 80000
                },
                new Employe
                {
                    Id = 106,
                    Nom = "Robert",
                    Genre = "male",
                    Salaire = 670000
                }
            };
            return li;
        }

        public List<Employe> GetDetails(int id)
        {
            List<Employe> li1 = new List<Employe>();
            Program p = new Program();
            var li = p.LesEmployes();
            foreach (var x in li)
            {
                if (x.Id == id)
                {
                    li1.Add(x);
                }
            }
            return li1;
        }

        static void Main() { }
    }
}