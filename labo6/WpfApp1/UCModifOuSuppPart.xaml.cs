using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour UCAjoutPart.xaml
    /// </summary>
    public partial class UCModifOuSuppPart : UserControl
    {
        public static ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

        public UCModifOuSuppPart()
        {
            InitializeComponent();
            DataContext = this;

        }
        private void Click_Supprimer(object sender, RoutedEventArgs e)
        {
            int valMat = (cbMatricule.SelectedItem as Participant).Matricule;
            if (cbMatricule.SelectedItem != null)
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=participants;UID=root;PASSWORD=;");
                try
                {
                    conn.Open();

                    string cmdString = "DELETE FROM participant WHERE Matricule = @valMat";

                    MySqlCommand cmd = new MySqlCommand(cmdString, conn);

                    cmd.Parameters.AddWithValue("@valMat", valMat);

                    cmd.ExecuteReader();

                    participants.Remove(cbMatricule.SelectedItem as Participant);

                    MessageBox.Show("Le participant a été exécuté avec succès");
                    cbMatricule.Text = "";
                    tbPrenom.Text = "";
                    tbNom.Text = "";

                    cbGenre.SelectedIndex = -1;
                    cbNiveau.SelectedIndex = -1;

                    tbEmail.Text = "";

                    MainWindow mw = (MainWindow)Application.Current.MainWindow;
                    mw.gPrincipal.Children.Remove(mw.ContenuEcran);
                    mw.ContenuEcran = mw.GestPart;

                    // Changement du contenu du user control UCAjourPArt
                    Grid.SetRow(mw.ContenuEcran, 1);
                    mw.gPrincipal.Children.Add(mw.ContenuEcran);

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public int mat;
        public string no, pre, niv, ema;
        public char gen;
        public bool isAct;

        // Défénir d'une méthode qui insère le nouveau participant dans la BD
        private void Click_Modifier(object sender, RoutedEventArgs e)
        {

            int valMAt = (cbMatricule.SelectedItem as Participant).Matricule;
            string valNom = tbNom.Text;
            string valPrenom = tbPrenom.Text;
            char valGen = char.Parse(cbGenre.SelectedItem.ToString().Split(':')[1].TrimStart(' '));
            string valNiv = cbNiveau.SelectedItem.ToString().Split(':')[1].TrimStart(' ');
            string valEmail = tbEmail.Text;
            bool valIsact;

            if (CkIsActif.IsChecked == true)
            {
                valIsact = true;
            }
            else
            {
                valIsact = false;
            }


            MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=participants;UID=root;PASSWORD=;");
            try
            {
                conn.Open();

                string cmdString = $"UPDATE participant Set Nom = @valNom, Prenom = @valPrenom, Genre = @valGen, Niveau = @valNiv, Email = @valEmail, IsActif = valIsact WHERE Matricule = @valMAt";

                MySqlCommand cmd = new MySqlCommand(cmdString, conn);

                cmd.Parameters.AddWithValue("@valMAt", valMAt);
                cmd.Parameters.AddWithValue("@valNom", valNom);
                cmd.Parameters.AddWithValue("@valPrenom", valPrenom);
                cmd.Parameters.AddWithValue("@valGen", valGen);
                cmd.Parameters.AddWithValue("@valNiv", valNiv);
                cmd.Parameters.AddWithValue("@valEmail", valEmail);
                cmd.Parameters.AddWithValue("@valIsact", valIsact);

                MySqlDataReader myReader;

                myReader = cmd.ExecuteReader();

                MessageBox.Show("Données modifié avec succes");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            cbMatricule.Text = "";
            tbPrenom.Text = "";
            tbNom.Text = "";

            cbGenre.SelectedIndex = -1;
            cbNiveau.SelectedIndex = -1;

            tbEmail.Text = "";

            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.gPrincipal.Children.Remove(mw.ContenuEcran);
            mw.ContenuEcran = mw.GestPart;

            // Changement du contenu du user control UCAjourPArt
            Grid.SetRow(mw.ContenuEcran, 1);
        }

        private void Click_Annuler(object sender, RoutedEventArgs e)
        {
            cbMatricule.Text = "";
            tbPrenom.Text = "";
            tbNom.Text = "";

            cbGenre.SelectedIndex = -1;
            cbNiveau.SelectedIndex = -1;

            tbEmail.Text = "";

            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.gPrincipal.Children.Remove(mw.ContenuEcran);
            mw.ContenuEcran = mw.GestPart;

            // Changement du contenu du user control UCAjourPArt
            Grid.SetRow(mw.ContenuEcran, 1);
            mw.gPrincipal.Children.Add(mw.ContenuEcran);
        }


        private void onLoaded_event(object sender, RoutedEventArgs e)
        {
            participants = UCGestionPart.participants;
            cbMatricule.ItemsSource = participants;

            DataContext = this;
        }

        private void CbMat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbMatricule.SelectedItem is Participant participant)
            {
                tbNom.Text = participant.Nom;
                tbPrenom.Text = participant.Prenom;
                tbEmail.Text = participant.Email;

                mat = participant.Matricule;
                no =  participant.Nom;
                pre = participant.Prenom;
                niv = participant.Niveau;
                gen = participant.Genre;
                ema = participant.Email;
                isAct = participant.IsActif;
            }
        }
    }
}
