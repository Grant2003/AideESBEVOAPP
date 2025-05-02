using System.Collections.ObjectModel;
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
using BLL;
namespace LabAEC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Film> films = new ObservableCollection<Film>();
        public static ObservableCollection<Pays> pays = new ObservableCollection<Pays>();


        public MainWindow()
        {
            InitializeComponent();
            dg_Film.ItemsSource = Films.films;
            cbPays.ItemsSource = LesPays.pays;

            DataContext = this;

            Films.ChargerListeFilms();
            LesPays.ChargerListeFilms();
            Films.ListeAnnees();

            dg_Film.ItemsSource = Films.films;
            cbMatricule.ItemsSource = Films.films;
            cbPays.ItemsSource = LesPays.pays;
            foreach( int annee in Films.annees)
            {
                cbAnnee.Items.Add(annee);
            }
            cbMatricule.SelectionChanged += CbMatricule_SelectionChanged;
            cbAnnee.SelectionChanged += CbAnnee_SelectionChanged; ;
            cbPays.SelectionChanged += CbPays_SelectionChanged; ;

        }

        private void CbAnnee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAnnee.SelectedItem is int selectedAnnee)
            {
                lb_Année.ItemsSource = Films.films.Where(f => f.Annee == selectedAnnee).ToList();
            }
            else
            {
                lb_Année.ItemsSource = Films.films;
            }
        }

        private void CbPays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPays.SelectedItem is Pays selectedPays)
            {
                lb_Pays.ItemsSource = Films.films.Where(f => f.Pays == selectedPays.PaysNom).ToList();
            }
            else
            {
                lb_Pays.ItemsSource = Films.films;
            }
        }

        private void CbMatricule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMatricule.SelectedItem is Film selectedFilm)
            {
                tbTitre.Text = selectedFilm.Titre;
                tbPays.Text = selectedFilm.Pays;
                tbAnnée.Text = selectedFilm.Annee.ToString();
            }
        }
    }
}