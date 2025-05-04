//Par Anthony Grenier 
//Mat: 2071623
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using Newtonsoft.Json;
using tp1EVO.code;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace GestionFilms
{
    public class Film
    {
        private const string NOM_FICHIER_CATEGORIES = @"..\donnees\fFilms.json";

        public static string getNomFichier()
        {
            return NOM_FICHIER_CATEGORIES;
        }
        public string Titre { get; set; }

        public int Annee { get; set; }

        public string Realisateur { get; set; }

        public int Duree { get; set; } // Durée en minutes

        //MODIFICATION POUR LA QUESTION 3 la liste deviens une liste de categories
        public List<Categorie> ListeCategories { get; set; }

        public string CheminAffiche { get; set; }

        [JsonIgnore]
        public string VraiCheminAffiche { get; private set; }
        public string Synopsis { get; set; }

        public List<Acteur> ListeActeurs { get; set; }

        /// <summary>
        /// Fait la lecture du fichier de données et retourne une liste de films.
        /// </summary>
        /// <remarks>
        /// Si le fichier est inexistant, vide ou corrompu, la méthode retourne une liste vide.
        /// </remarks>
        /// <returns>Une liste de films.</returns>
        /// 
        /// //MODIFICATION POUR LA QUESTION 1 Ajout d'un try:catch pour la gestion des erreurs
        public static List<Film> LirefFilms()
        {
            try
            {
                //MODIFICATION POUR LA QUESTION 1 On utilise newtonsoft.Json pour convertir le Json en objets temporaires
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NOM_FICHIER_CATEGORIES);
                string jsonData = File.ReadAllText(jsonPath);

                var tempList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

                List<Film> ListeFilms = new List<Film>();

                foreach (var tempFilm in tempList)
                {
                    //MODIFICATION POUR LA QUESTION 1 On transforme l'objet en objes de type Film sans la liste d'acteurs

                    Film film = new Film(
                        (string)tempFilm.Titre,
                        (int)tempFilm.Annee,
                        (string)tempFilm.Realisateur,
                        (int)tempFilm.Duree,
                        (string)tempFilm.CheminAffiche,
                        (string)tempFilm.Synopsis
                    );
                    //MODIFICATION POUR LA QUESTION 3 la liste de categorie est initialisé ici

                    film.ListeCategories = new List<Categorie>();

                    foreach (var categorieNom in tempFilm.ListeCategories)
                    {
                        film.ListeCategories.Add(new Categorie(categorieNom.ToString()));

                    }

                    //MODIFICATION POUR LA QUESTION 1 On ajoute les liste d'acteurs au film
                    film.ListeActeurs = new List<Acteur>();

                    foreach (var acteurNom in tempFilm.ListeActeurs)
                    {
                        film.ListeActeurs.Add(new Acteur(acteurNom.ToString()));

                    }

                    ListeFilms.Add(film);
                }

                return ListeFilms;
            }
            catch (Exception ex) //MODIFICATION POUR LA QUESTION 1 gestion des erreurs de fichiers
            {
                Console.WriteLine("Une erreur a eu lieux durant la lecture du fichier Json: " + ex.Message);
                return new List<Film>();
            }
        }

        /// <summary>
        /// Enregistre la liste de films reçue dans le fichier de données.
        /// </summary>
        /// <remarks>
        /// Le contenu existant est écrasé à l'écriture.
        /// Si le fichier n'existe pas, il sera créé.
        /// </remarks>
        /// <param name="lst">La liste de films qui doit être enregistrée dans le fichier.</param>
        public static void EnregistrerListeFilms(JsonWriter writer, Film value, JsonSerializer serializer)
        {
            var film = new JObject
        {
            { "Titre", value.Titre },
            { "Annee", value.Annee },
            { "Realisateur", value.Realisateur },
            { "Duree", value.Duree },
            { "ListeCategories", new JArray(value.ListeCategories.Select(cat => cat.Nom)) }, // conversion de categorie a liste de strings
            { "CheminAffiche", value.CheminAffiche },
            { "Synopsis", value.Synopsis },
            { "ListeActeurs", new JArray(value.ListeActeurs.Select(act => act.Nom)) } // covnersion d'acteurs a liste de strings
        };

            film.WriteTo(writer);
        }

        /// <summary>
        /// Contructeur de la classe Film
        /// </summary>
        /// <param name="titre">Le titre du film.</param>
        /// <param name="annee">L'année de sortie du film.</param>
        /// <param name="realisateur">Nom du réalisateur du film.</param>
        /// <param name="duree">La durée du film en minutes.</param>
        /// <param name="categories">La catégorie du film.</param>
        /// <param name="cheminAffiche">Le chemin d'accès vers le fichier de l'affiche du film.</param>
        /// <param name="synopsis">Le synopsis du film.</param>
        /// <param name="acteurs">La liste des acteurs du film.</param>
        /// <remarks>
        /// La gestion des catégories d'un film se fait par les méthodes de la classe.
        /// </remarks>
        ///MODIFICATION POUR LA QUESTION 3 : Remplacement de Categorie par Categories et changement de type pour une liste

        public Film(string titre, int annee, string realisateur, int duree, List<Categorie> categories, string cheminAffiche, string synopsis, List<Acteur> acteurs)
        {
            Titre = titre;
            Annee = annee;
            Realisateur = realisateur;
            Duree = duree;
            ListeCategories = categories;
            CheminAffiche = cheminAffiche;
            VraiCheminAffiche = Regex.Replace(cheminAffiche, @"[/\\]+", "/"); //On efface les // par un /
            Synopsis = synopsis;
            ListeActeurs = acteurs;
        }
        //MODIFICATION POUR LA QUESTION 3 : Remplacement de Categories par une liste de Categories  et retrait du parametre

        public Film(string titre, int annee, string realisateur, int duree, string cheminAffiche, string synopsis)
        {
            Titre = titre;
            Annee = annee;
            Realisateur = realisateur;
            Duree = duree;
            //MODIFICATION POUR LA QUESTION 3 : Remplacement de Categorie par une liste de Categories
            ListeCategories = new List<Categorie>();
            CheminAffiche = cheminAffiche; 
            VraiCheminAffiche = Regex.Replace(cheminAffiche, @"[/\\]+", "/"); //On efface les // par un /
            Synopsis = synopsis;
            ListeActeurs = new List<Acteur>();
        }

        /// <summary>
        /// Permet d'ajouter un acteur au film.
        /// </summary>
        /// <param name="nouvelActeur">Le nouvel acteur.</param>
        public void AjouterActeur(Acteur nouvelActeur)
        {
            if (Acteur.EstValide(nouvelActeur.Nom))
            {
                ListeActeurs.Add(nouvelActeur);
            }
        }

        /// <summary>
        /// Permet de retirer un acteur du film.
        /// </summary>
        /// <param name="ancienActeur">L'acteur à retirer.</param>
        public void RetirerActeur(Acteur ancienActeur)
        {
            ListeActeurs.Remove(ancienActeur);
        }

        //MODIFICATION POUR LA QUESTION 2
        /// <summary>
        /// Permet de vérifier si le film a un acteur précis.
        /// </summary>
        /// <param name="acteur">L'acteur à vérifier.</param>
        /// <returns>True si le film a l'acteur, false sinon.</returns>
        public bool AActeur(Acteur acteur)
        {
            return ListeActeurs.Contains(acteur);
        }
        //MODIFICATION POUR LA QUESTION 2
        /// <summary>
        /// Permet d'effacer la categorie de la liste si elle est présente
        /// </summary>
        /// <param name="nom"></param>
        public void EffacerCategorie(string nom)
        {
            var categorieToRemove = this.ListeCategories.FirstOrDefault(c => c.Nom == nom);
            if (categorieToRemove != null)
            {
                this.ListeCategories.Remove(categorieToRemove);
            }
        }
        //MODIFICATION POUR LA QUESTION 2
        /// <summary>
        /// Permet de modifier le nom d'une categorie si elle est dans la liste
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="nouveauNom"></param>
        public void ModifierCategorie(string nom, string nouveauNom)
        {
            var categorieAModifier = this.ListeCategories.FirstOrDefault(c => c.Nom == nom);
            if (categorieAModifier != null)
            {
                categorieAModifier.Nom = nouveauNom;
            }
        }
        //MODIFICATION POUR LA QUESTION 2
        /// <summary>
        /// Permet de Verifier Si la categorie qui va être enlevé est présente et la dernière du film
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        public bool VerifierCategorie(string nom)
        {
            if (ListeCategories.Any(c => c.Nom == nom) && ListeCategories.Count <= 1)
            {

                return false;

            }

            return true;
        }

    }
}
