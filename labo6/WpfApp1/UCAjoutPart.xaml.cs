using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    public partial class UCAjoutPart : UserControl
    {

        public UCAjoutPart()
        {
            InitializeComponent();
        }

        private void Click_Valider(object sender, RoutedEventArgs e)
        {

            int ma = int.Parse(tbMatricule.Text);
            string no = tbNom.Text;
            string pr = tbPrenom.Text;
            char gn = char.Parse(cbGenre.SelectedItem.ToString().Split(':')[1].TrimStart(' '));
            string ni = cbNiveau.SelectedItem.ToString().Split(':')[1].TrimStart(' ');
            string em = tbEmail.Text;

            int isact;
            bool isactBool;

            if (CkIsActif.IsChecked == true)
            {
                isact = 1;
                isactBool = true;
            }
            else{
                isact = 0;
                isactBool = false;
            }



            UCGestionPart.participants.Add(new Participant()
            {
                Matricule = ma,
                Prenom = pr,
                Nom = no,
                Genre = gn,
                Niveau = ni,
                Email = em,
                IsActif = isactBool,

            });

            // Appel d'une méthode qui insère le nouveau participant dans la BD
            InserNouvPartDB(ma, pr, no, gn, ni, em, isact);

            // On décharge le contenu de UCAjoutPart de la fenêtre MainWindow
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.gPrincipal.Children.Remove(mw.ContenuEcran);

            // On charge le contenu de la UCGestionPart dans la fenêtre MainWindow
            mw.ContenuEcran = mw.GestPart;
            Grid.SetRow(mw.GestPart, 1);
            mw.gPrincipal.Children.Add(mw.ContenuEcran);

            tbMatricule.Text = "";
            tbPrenom.Text = "";
            tbNom.Text = "";

            cbGenre.SelectedIndex = -1;
            cbNiveau.SelectedIndex = -1;

            tbEmail.Text = "";

            //UCGestionPart.ReecrireFichier();
        }

        // Défénir d'une méthode qui insère le nouveau participant dans la BD
        public void InserNouvPartDB(int ma, string pr, string no, char gn, string ni, string em, int isact)
        {
            // La connexion à la base de données sur le serveur local
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=participants;UID=root;PASSWORD=;");

            try
            {
                conn.Open();

                string cmdString = $"INSERT INTO Participant(Matricule, Nom, Prenom, Genre, Niveau, Email, IsActif) VALUES ({ma}, '{no}', '{pr}', '{gn}', '{ni}', '{em}', {isact})";

                MySqlCommand cmd = new MySqlCommand(cmdString, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Données du nouveau participant ajoutées avec succes");
            }
            catch (MySqlException ex) 
            {
                MessageBox.Show(ex.Message);
            }finally
            {
                conn.Close();
            }
        }
    }
}
