//Par Anthony Grenier 
//Mat: 2071623
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GestionFilms
{
    public class Acteur
    {
        #region Static
        //MODIFICATION POUR LA QUESTION 2 ajout du nom de fichier pour éviter les erreurs
        private const string NOM_FICHIER_CATEGORIES = @"..\donnees\fActeurs.json";

        public static string getNomFichier()
        {
            return NOM_FICHIER_CATEGORIES;
        }

        private static List<Acteur> ListeActeurs = new List<Acteur>();

        /// <summary>
        /// Fait la lecture du fichier de données et conserver la liste d'acteurs.
        /// </summary>
        /// <remarks>
        /// Si le fichier est inexistant, vide ou corrompu, une liste vide est implémentée.
        /// </remarks>
        public static List<Acteur> LirefActeurs()
        {
            try
            {
                //MODIFICATION POUR LA QUESTION 1 On utilise newtonsoft.Json pour convertir le Json en liste d'acteurs

                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\donnees\fActeurs.json");
                string jsonData = File.ReadAllText(jsonPath);

                return ListeActeurs = JsonConvert.DeserializeObject<List<Acteur>>(jsonData) ?? new List<Acteur>(); // Return an empty list if deserialization returns null
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur a eu lieux durant la lecture du fichier Json: " + ex.Message);
                return ListeActeurs = new List<Acteur>(); 
            }
        }

        /// <summary>
        /// Enregistre la liste d'acteurs dans le fichier de données.
        /// </summary>
        /// <remarks>
        /// Le contenu existant est écrasé à l'écriture.
        /// Si le fichier n'existe pas, il sera créé.
        /// </remarks>
        public static void EnregistrerListeActeurs()
        {
            using (StreamWriter sw = new StreamWriter("fActeurs.txt"))
            {
                foreach (Acteur element in ListeActeurs)
                {
                    sw.WriteLine(element.Nom);
                }
            }
        }

        /// <summary>
        /// Permet d'obtenir la liste d'acteurs. 
        /// </summary>
        /// <returns>Une liste d'acteurs.</returns>
        public static List<Acteur> ObtenirListe()
        {
            return ListeActeurs;
        }

        /// <summary>
        /// Permet d'indiquer si un acteur fait partie de la liste des acteurs en mémoire.
        /// </summary>
        /// <param name="acteurVise">L'acteur à vérifier.</param>
        /// <returns>True si l'acteur se trouve dans la liste, false sinon.</returns>
        public static bool EstValide(string acteurVise)
        {
            bool estValide = false;
            int i = 0;

            while (!estValide && i < ListeActeurs.Count)
            {
                if (ListeActeurs[i].Nom == acteurVise)
                {
                    estValide = true;
                }

                i++;
            }

            return estValide;
        }
        //MODIFICATION POUR LA QUESTION 2
        /// <summary>
        /// Permet de mettre a jour le nombre de film auquel a participé l'acteur dans une liste de film 
        /// </summary>
        /// <param name="ListeDeFilms"></param>
        public void SetNombreFilm(ObservableCollection<Film> ListeDeFilms)
        {
            nbFilms = 0;
            foreach (Film film in ListeDeFilms)
            {
                if (film.ListeActeurs.Any(a => a.Nom == this.Nom))
                {
                    nbFilms++;
                }
            }
        }
        #endregion

        public string Nom { get; set; }
        //MODIFICATION POUR LA QUESTION 2 Permet de compter les films
        [JsonIgnore]
        public int nbFilms { get; set; }

        /// <summary>
        /// Constructeur de la classe Acteur.
        /// </summary>
        /// <param name="nom">Le nom de l'acteur.</param>
        public Acteur(string nom)
        {
            Nom = nom;
           // MODIFICATION POUR LA QUESTION 2 parametre initialisé a zero par défaut
            nbFilms = 0;
        }
        public override string ToString()
        {
            return Nom;
        }
    }
}
