//Par Anthony Grenier 
//Mat: 2071623
using GestionFilms;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using tp1EVO.code;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Reflection;

namespace tp1EVO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Film> ListeDeFilms { get; set; } = new ObservableCollection<Film>(Film.LirefFilms());

        public static ObservableCollection<Acteur> ListeDActeurs { get; set; } = new ObservableCollection<Acteur>(Acteur.LirefActeurs());

        public static ObservableCollection<Categorie> ListeCategories { get; set; } = new ObservableCollection<Categorie>(Categorie.LirefCategories());


        public MainWindow()
        {
            InitializeComponent();

            // Charger la liste des acteurs en mémoire.
            FilmsDataGrid.ItemsSource = ListeDeFilms;
            var nom = txtNomCategorie.Text.ToLower();

            ChangerVisibiliteBoutton();

            DataContext = this;
            //evenements si les selections changent
            CategoriesListBox.SelectionChanged += CategoriesListBox_SelectionChanged;
            ActeursListBox.SelectionChanged += ActeursListBox_SelectionChanged;

            //on enleve les propiétés de film facultative
            EnleverPoster();
            EnleverActeurs();
            EnleverSynopsis();
            Remove_Categorie();
            MajNombreDeFilms();
        }

        /// <summary>
        /// Met a jour le nombre de film des acteurs
        /// </summary>
        private void MajNombreDeFilms()
        {
            foreach(Acteur acteur in ListeDActeurs)
            {
                acteur.SetNombreFilm(ListeDeFilms);
            }
            var view = CollectionViewSource.GetDefaultView(ActeursListBox.ItemsSource);
            if (view != null)
            {
                view.Refresh();
            }
        }

        //change la visibilité des bouttons selon si un item est selectionné
        private void ActeursListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangerVisibiliteBoutton();
        }

        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangerVisibiliteBoutton();
        }

        /// <summary>
        /// change la visibilité des bouttons selon si un item est selectionné dans chacun des onglets
        /// </summary>
        public void ChangerVisibiliteBoutton()
        {
            if(CategoriesListBox.SelectedItem == null)
            {
                btnModifierCat.Visibility = Visibility.Collapsed;
                btnSupprimerCat.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModifierCat.Visibility = Visibility.Visible;
                btnSupprimerCat.Visibility = Visibility.Visible;
            }


            if(ActeursListBox.SelectedItem == null)
            {
                btnModifierAct.Visibility = Visibility.Collapsed;
                btnSupprimerAct.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModifierAct.Visibility = Visibility.Visible;
                btnSupprimerAct.Visibility = Visibility.Visible;
            }

        }

        private void Click_Ajouter_Cat(object sender, RoutedEventArgs e)
        {
            AjouterCategorie();
        }
        /// <summary>
        /// Ajoute une categorie si le nom est valide
        /// </summary>
        public  void AjouterCategorie()
        {
            bool nomLibre = VerifierNomCat(txtNomCategorie.Text);

            if (nomLibre)
            {
                ListeCategories.Add(new Categorie(txtNomCategorie.Text));
                ReecrireFichierCat(ListeCategories);
            }
        }
        private void Click_Ajouter_Act(object sender, RoutedEventArgs e)
        {
            AjouterActeur();
        }

        /// <summary>
        /// Ajoute un acteur si le nom est valide
        /// </summary>
        public  void AjouterActeur()
        {
            if (!ListeDActeurs.Any(acteur => acteur.Nom == txtNomActeur.Text))
            {
                ListeDActeurs.Add(new Acteur(txtNomActeur.Text));
                ReecrireFichierAct(ListeDActeurs);
            }
            else
            {
                MessageBox.Show("Un acteur du nom " + txtNomActeur.Text + " Existe Déja", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }


        //verification des nom de categorie et des acteurs (si ils existent deja ou sont null)
        public bool VerifierNomCat(string nom)
        {
            if (!ListeCategories.Any(categorie => categorie.Nom == nom) && !string.IsNullOrEmpty(nom))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Le nom de la categorie existe deja ou n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public bool VerifierNomAct(string nom)
        {
            if (!ListeDActeurs.Any(acteur => acteur.Nom == nom) && !string.IsNullOrEmpty(nom))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Le nom de la categorie existe deja ou n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// modifie le nom de la categorie dans la fenetre et dans les films
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Modifier_Cat(object sender, RoutedEventArgs e)
        {
            if (CategoriesListBox.SelectedItem != null)
            {
                Categorie selectedCategory = (Categorie)CategoriesListBox.SelectedItem;
                string nouveauNom = txtNomCategorie.Text.Trim();
                bool nomLibre = VerifierNomCat(txtNomCategorie.Text);


                if (nomLibre)
                {
                    ModifierCategoriesDesFilms(selectedCategory.Nom, nouveauNom);
                    selectedCategory.Nom = nouveauNom;
                    ReecrireFichierCat(ListeCategories);
                    CategoriesListBox.Items.Refresh();
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// Modifie la categorie dans les films qui l'ont
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="nouveauNom"></param>
        private void ModifierCategoriesDesFilms(string nom, string nouveauNom)
        {
            foreach (Film film in ListeDeFilms)
            {
                film.ModifierCategorie(nom, nouveauNom);
            }
        }

        /// <summary>
        /// Verifie si la categorie peux etre supprimé et l'enleve si c'est le cas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Supprimer_Cat(object sender, RoutedEventArgs e)
        {
            if (CategoriesListBox.SelectedItem != null && CategoriesListBox.SelectedItem is Categorie categorie)
            {
                if (VerifierCategorieFilms(categorie.Nom))
                {
                    ListeCategories.Remove(categorie);
                    EffacerCategorieDesFilms(categorie.Nom);
                    ReecrireFichierCat(ListeCategories);
                }
                else
                {
                    MessageBox.Show("La categorie est la dernière d'un film.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }
        /// <summary>
        /// Verifie si un des film a cette categorie comme derniere
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>

        private bool VerifierCategorieFilms(string nom)
        {
            foreach (Film film in ListeDeFilms)
            {
                if (!film.VerifierCategorie(nom))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// efface la categorie de tous les films
        /// </summary>
        /// <param name="nom"></param>
        private void EffacerCategorieDesFilms(string nom)
        {
            foreach (Film film in ListeDeFilms)
            {
                film.EffacerCategorie(nom);
            }
        }
        /// <summary>
        /// Modifie le nom d'un acteur si ce nom est disponible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Modifier_Act(object sender, RoutedEventArgs e)
        {
            if (ActeursListBox.SelectedItem != null)
            {
                Acteur selectedAct = (Acteur)ActeursListBox.SelectedItem;
                string newName = txtNomActeur.Text.Trim();
                bool nomLibre = VerifierNomAct(txtNomActeur.Text);


                if (nomLibre)
                {
                    selectedAct.Nom = newName;
                    ReecrireFichierAct(ListeDActeurs);
                    ActeursListBox.Items.Refresh();
                }
                else
                {
                    return;
                }
            }
        }
        private void Click_Supprimer_Act(object sender, RoutedEventArgs e)
        {
            if (ActeursListBox.SelectedItem != null)
                ListeDActeurs.Remove(ActeursListBox.SelectedItem as Acteur);
            ReecrireFichierAct(ListeDActeurs);
        }
        // -----------------Gestion des propriétés de film facultative et leur affichage----------------- //


        private void CheckBoxPost_Checked(object sender, RoutedEventArgs e)
        {
            if (!FilmsDataGrid.Columns.Contains(DataGridPost))
            {
                FilmsDataGrid.Columns.Add(DataGridPost);
            }
        }

        private void CheckBoxCat_Checked(object sender, RoutedEventArgs e)
        {
            if (!FilmsDataGrid.Columns.Contains(DataGridCat))
            {
                FilmsDataGrid.Columns.Add(DataGridCat);
            }
        }
        private void CheckBoxSyn_Checked(object sender, RoutedEventArgs e)
        {
            if (!FilmsDataGrid.Columns.Contains(DataGridSyn))
            {
                FilmsDataGrid.Columns.Add(DataGridSyn);
            }
        }

        private void CheckBoxAct_Checked(object sender, RoutedEventArgs e)
        {
            if (!FilmsDataGrid.Columns.Contains(DataGridAct))
            {
                FilmsDataGrid.Columns.Add(DataGridAct);
            }
        }
        private void CheckBoxPost_Unchecked(object sender, RoutedEventArgs e)
        {
            EnleverPoster();
        }
        private void CheckBoxCat_Unchecked(object sender, RoutedEventArgs e)
        {
            Remove_Categorie();
        }

        private void CheckBoxSyn_Unchecked(object sender, RoutedEventArgs e)
        {
            EnleverSynopsis();
        }



        private void CheckBoxAct_Unchecked(object sender, RoutedEventArgs e)
        {
            EnleverActeurs();
        }

        private void EnleverActeurs()
        {
            if (FilmsDataGrid.Columns.Contains(DataGridAct))
            {
                FilmsDataGrid.Columns.Remove(DataGridAct);
            }
        }
        private void Remove_Categorie()
        {
            if (FilmsDataGrid.Columns.Contains(DataGridCat))
            {
                FilmsDataGrid.Columns.Remove(DataGridCat);
            }
        }
        private void EnleverSynopsis()
        {
            if (FilmsDataGrid.Columns.Contains(DataGridSyn))
            {
                FilmsDataGrid.Columns.Remove(DataGridSyn);
            }
        }
        private void EnleverPoster()
        {
            if (FilmsDataGrid.Columns.Contains(DataGridPost))
            {
                FilmsDataGrid.Columns.Remove(DataGridPost);
            }
        }
        // -----------------Gestion des propriétés de film facultative et leur affichage----------------- //


        // -----------------Gestion d'ouverture des modales----------------- //

        private void OuvrirModalAjouter()
        {
            ModalWindow modal = new ModalWindow(this)
            {
                Owner = this, // Set the main window as the owner
            };

            bool? result = modal.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Action validée !");
            }
        }
        private void OuvrirModalModifier()
        {
            if(FilmsDataGrid.SelectedItem is Film film)
            {
                ModalModifier modal = new ModalModifier(this, film)
                {
                    Owner = this, // Set the main window as the owner
                };

                bool? result = modal.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("Action validée !");
                }
            }
        }
        private void OuvrirModalSupprimer()
        {
            ModalSuppression modal = new ModalSuppression(this)
            {
                Owner = this, // Set the main window as the owner
            };

            bool? result = modal.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Action validée !");
            
            }
        }
        // -----------------Gestion d'ouverture des modales----------------- //

        // -----------------Gestion Click des bouttons modales----------------- //

        private void Click_Supprimer_Film(object sender, RoutedEventArgs e)
        {
            if(FilmsDataGrid.SelectedItem != null)
            {
                OuvrirModalSupprimer();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un film a supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_Modifier_Film(object sender, RoutedEventArgs e)
        {
            OuvrirModalModifier();
        }

        private void Click_Ajouter_Film(object sender, RoutedEventArgs e)
        {
            OuvrirModalAjouter();
        }
        // -----------------Gestion Click des bouttons modales----------------- //

        /// <summary>
        /// supression d'un film et reecriture du fichier et mise a jour du nbFilm des acteurs
        /// </summary>
        public void SupprimerFilm()
        {
            if (FilmsDataGrid.SelectedItem != null)
                ListeDeFilms.Remove(FilmsDataGrid.SelectedItem as Film);
            ReecrireFichierFilm(ListeDeFilms);
            MajNombreDeFilms();
        }

        /// <summary>
        /// Ajout d'un nouveau Film et reecriture du fichier si il ny a pas de film avec ce titre et mise a jour du nbFilm des acteurs
        /// </summary>
        /// <param name="film"></param>
        public bool AjouterFilm(Film film)
        {
            if(!ListeDeFilms.Any(f => f.Titre == film.Titre))
            {
                ListeDeFilms.Add(film);
                ReecrireFichierFilm(ListeDeFilms);
                MajNombreDeFilms();
                MessageBox.Show("Le film a été ajouté avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;

            }
            else
            {
                MessageBox.Show("Il y a deja un film de ce titre", "Erreur",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// On trouve le film a modifier dans la liste et on le remplace par les nouvelles données et mise a jour du nbFilm des acteurs
        /// </summary>
        /// <param name="nouveauFilm"></param>
        public void ModifierFilm(Film nouveauFilm)
        {
            int index = -1;
            for (int i = 0; i < ListeDeFilms.Count; i++)
            {
                // on trouve le film par son titre (pas sensible a la case)
                if (string.Equals(ListeDeFilms[i].Titre, nouveauFilm.Titre, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                MessageBox.Show("Film non trouvé dans la liste.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ListeDeFilms[index] = nouveauFilm;
            FilmsDataGrid.SelectedItem = nouveauFilm;

            MessageBox.Show("Le film a été modifié avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            ReecrireFichierFilm(ListeDeFilms);
            MajNombreDeFilms();
        }
        /// <summary>
        /// Permet La réécriture du ficher de film 
        /// </summary>
        /// <param name="films"></param>
        public static void ReecrireFichierFilm(ObservableCollection<Film> films)
        {
            using (StreamWriter file = File.CreateText(Film.getNomFichier()))

            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();
                foreach (var film in films)
                {
                    Film.EnregistrerListeFilms(writer, film, new JsonSerializer());
                }
                writer.WriteEndArray();
            }
        }
        /// <summary>
        /// Reecriture du fichier des categories
        /// </summary>
        /// <param name="categories"></param>
        public static void ReecrireFichierCat(ObservableCollection<Categorie> categories)
        {
            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            System.IO.File.WriteAllText(Categorie.getNomFichier(), json);
        }
        public static void ReecrireFichierAct(ObservableCollection<Acteur> acteurs)
        {
            string json = JsonConvert.SerializeObject(acteurs, Formatting.Indented);
            System.IO.File.WriteAllText(Acteur.getNomFichier(), json);
        }



        private void Click_Propos(object sender, RoutedEventArgs e)
        {
            APropos propos = new APropos()
            {
                Owner = this, 
            };

            propos.ShowDialog();
        }
    }
}