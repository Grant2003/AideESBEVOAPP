//Fait par Anthony grenier le 10 mars 2025
using GestionFilms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        private MainWindow _mainWindow;
        private string SelectedPosterPath = "";


        public ModalWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            this.DataContext = _mainWindow;
        }
        private void Click_BtnAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// On assure le bon nombre de catégories, enelevant tout item en haut de 4
        /// </summary>
        private void ListBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

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
        /// <summary>
        /// On Selectionne le chemin du poster dans le dossier poster, 
        /// </summary>
        private void BtnSelectPoster_Click(object sender, RoutedEventArgs e)
        {
            string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string affichesFolder = Path.Combine(solutionDir, "affiches");
            if (!Directory.Exists(affichesFolder))
            {
                MessageBox.Show("Le dossier "+ affichesFolder+" n'existe pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); // gère de potentiels erreurs de dossier et du déboggage
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = affichesFolder,
                Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", // Permet seulement les images
                Title = "Sélectionner une affiche de film"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;

                PosterPreview.Source = new BitmapImage(new Uri(selectedFile));

                SelectedPosterPath = selectedFile;
            }
        }
        /// <summary>
        /// On Ajoute le film et gère les erreurs de saise de l'utilisateur
        /// </summary>
        private void Click_BtnAjouterFilm(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBoxActeurs.SelectedItems.Count >= 1 && ListBoxCategories.SelectedItems.Count >= 1) // Verification du nombre minimale de catégories et d'acteurs
                {
                    if (!int.TryParse(TxtBoxAnnee.Text, out int annee) ||
                        !int.TryParse(TxtBoxDuration.Text, out int duree))
                    {
                        MessageBox.Show("Veuillez entrer des valeurs valides pour l'année et la durée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); //gestion de mauvaise valeurs INTEGER
                        return;
                    }
                    if(TxtBoxTitre.Text == "" || TxtBoxReal.Text =="" || TxtBoxSynopsis.Text == "" || PosterPreview.Source?.ToString() == "")
                    {
                        MessageBox.Show("Veuillez entrer des valeurs valides pour les champs textuelles", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); //gestion de manque de valeur pour les strings
                        return;
                    }

                    List<Categorie> categories = ListBoxCategories.SelectedItems.Cast<Categorie>().ToList(); // creation de la liste d'acteur et categorie
                    List<Acteur> acteurs = ListBoxActeurs.SelectedItems.Cast<Acteur>().ToList();

                    Film nouveauFilm = new Film( // creation du film
                        TxtBoxTitre.Text,
                        annee,
                        TxtBoxReal.Text,
                        duree,
                        PosterPreview.Source?.ToString() ?? "",
                        TxtBoxSynopsis.Text
                    );

                    nouveauFilm.ListeCategories = categories;
                    nouveauFilm.ListeActeurs = acteurs;

                    if (_mainWindow.AjouterFilm(nouveauFilm))
                    {
                        this.Close();
                    }
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
