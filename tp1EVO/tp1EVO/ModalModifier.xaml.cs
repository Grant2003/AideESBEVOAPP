//Par Anthony Grenier 
//Mat: 2071623
using GestionFilms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using tp1EVO.code;

namespace tp1EVO
{
    /// <summary>
    /// Interaction logic for ModalModifier.xaml
    /// </summary>
    public partial class ModalModifier : Window
    {
        private MainWindow _mainWindow;
        private Film filmAModifier;
        
        public static ObservableCollection<Acteur> ListeDActeurs { get; set; } = new ObservableCollection<Acteur>();

        public static ObservableCollection<Categorie> ListeCategories { get; set; } = new ObservableCollection<Categorie>();
        public ModalModifier(MainWindow mainWindow, Film film)
        {

            InitializeComponent();
            _mainWindow = mainWindow;
            //On a besoin d'une liste des items local pour pouvoir les comparés avec ceux du film
            ListeDActeurs =  new ObservableCollection<Acteur>(Acteur.LirefActeurs());
            ListeCategories =  new ObservableCollection<Categorie>(Categorie.LirefCategories());
            ListBoxActeurs.ItemsSource = ListeDActeurs;
            ListBoxCategories.ItemsSource = ListeCategories;
            this.DataContext = _mainWindow;


            filmAModifier = film;
            SetupFields();
        }
        private void Click_BtnAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// On gere le bon nombre de categories selectionnées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is ListBox listBox)
            {
                if (listBox.SelectedItems.Count > 4)
                {
                    MessageBox.Show("Vous ne pouvez sélectionner que 4 catégories au maximum.", "Limite atteinte", MessageBoxButton.OK, MessageBoxImage.Warning);

                    listBox.SelectedItems.Remove(e.AddedItems[0]);
                }
                else if (listBox.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner au moins une catégorie.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        /// <summary>
        /// On met les valeurs du film a modifier dans les champs correspondants
        /// </summary>
        private void SetupFields()

        {
            ListBoxCategories.SelectedItems.Clear();
            //On met les categories du film comme séléctionné
            foreach (var categorie in filmAModifier.ListeCategories)
            {
                var matchingCategorie = ListBoxCategories.Items
                                     .Cast<Categorie>()
                                     .FirstOrDefault(a => a.Nom == categorie.Nom);

                if (matchingCategorie != null)
                {
                    ListBoxCategories.SelectedItems.Add(matchingCategorie);
                }
            }
            //On met les cateurs du film comme séléctionné
            ListBoxActeurs.SelectedItems.Clear();
            foreach (var acteur in filmAModifier.ListeActeurs)
            {
                var matchingActeur = ListBoxActeurs.Items
                                     .Cast<Acteur>()
                                     .FirstOrDefault(a => a.Nom == acteur.Nom);

                if (matchingActeur != null)
                {
                    ListBoxActeurs.SelectedItems.Add(matchingActeur);
                }
            }
            TxtBoxAnnee.Text = filmAModifier.Annee.ToString();
            TxtBoxReal.Text = filmAModifier.Realisateur;
            TxtBoxDuration.Text = filmAModifier.Duree.ToString();
            TxtBoxSynopsis.Text = filmAModifier.Synopsis;
        }

        private void Click_BtnModifierFilm(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBoxActeurs.SelectedItems.Count >= 1 && ListBoxCategories.SelectedItems.Count >= 1)
                {
                    // validation des entrées INTEGER
                    if (!int.TryParse(TxtBoxAnnee.Text, out int annee) ||
                        !int.TryParse(TxtBoxDuration.Text, out int duree))
                    {
                        MessageBox.Show("Veuillez entrer des valeurs valides pour l'année et la durée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                    // validation des entrées String

                    if (TxtBoxReal.Text =="" || TxtBoxSynopsis.Text =="")
                    {
                        MessageBox.Show("Veuillez entrer des valeurs valides pour les champs textuelles", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    List<Categorie> categories = ListBoxCategories.SelectedItems.Cast<Categorie>().ToList();
                    List<Acteur> acteurs = ListBoxActeurs.SelectedItems.Cast<Acteur>().ToList();

                    //creation du nouveau film
                    Film nouveauFilm = new Film(
                        filmAModifier.Titre,
                        annee,
                        TxtBoxReal.Text,
                        duree,
                        filmAModifier.CheminAffiche,
                        TxtBoxSynopsis.Text
                    );

                    nouveauFilm.ListeCategories = categories;
                    nouveauFilm.ListeActeurs = acteurs;

                    //on envoie le film modifié
                    _mainWindow.ModifierFilm(nouveauFilm);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Veuillez choisir au moin 1 acteur et 1 categorie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Le film n'a pas pu etre ajouté");
            }
        }
    }
}
