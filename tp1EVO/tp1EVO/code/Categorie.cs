//Par Anthony Grenier 
//Mat: 2071623
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace tp1EVO.code
{
    public class Categorie 
    {
        private const string NOM_FICHIER_CATEGORIES = @"..\donnees\fCategories.json";

        public static string getNomFichier()
        {
            return NOM_FICHIER_CATEGORIES;
        }
        private static List<Categorie> ListeCategories = new List<Categorie>();
        public string Nom { get; set; }

        public Categorie(string nom)
        {
            Nom = nom;
        }
        public static List<Categorie> LirefCategories()
        {
            try
            {
                //MODIFICATION POUR LA QUESTION 1 On utilise newtonsoft.Json pour convertir le Json en liste d'acteurs

                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NOM_FICHIER_CATEGORIES);
                string jsonData = File.ReadAllText(jsonPath);

                return ListeCategories = JsonConvert.DeserializeObject<List<Categorie>>(jsonData) ?? new List<Categorie>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur a eu lieux durant la lecture du fichier Json: " + ex.Message);
                return ListeCategories = new List<Categorie>();
            }
        }

    }
}
