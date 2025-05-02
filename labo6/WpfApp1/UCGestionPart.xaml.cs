
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Policy;
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
    /// Logique d'interaction pour UCGestionPart.xaml
    /// </summary>
    public partial class UCGestionPart : UserControl
    {
        public static ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

        // Instanciation dMun objet UCAjoutPart pour le charger à la place du contenu de UCGestionPart
        private UCAjoutPart EcrAjoutPart = new UCAjoutPart();
        private UCModifOuSuppPart EcrModSupp = new UCModifOuSuppPart();



        public UCGestionPart()
        {
            InitializeComponent();

            //Participant.ChargerFichier();

            dgParticipants.ItemsSource = participants;

            DataContext = this;

            ConnecterBD();


        
        }

        public void ConnecterBD()
        {
            // Connexion locale sur WampServer
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=participants;UID=root;PASSWORD=;");

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from participant", conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet(); // using System.data

                adapter.Fill(ds, "participant");

                var dt = ds.Tables["participant"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    participants.Add(new Participant
                    {
                        Matricule = (int)dt.Rows[i]["Matricule"],
                        Nom = dt.Rows[i]["Nom"].ToString(),
                        Prenom = dt.Rows[i]["Prenom"].ToString(),
                        Genre = char.Parse(dt.Rows[i]["Genre"].ToString()),
                        Niveau = dt.Rows[i]["Niveau"].ToString(),
                        Email = dt.Rows[i]["Email"].ToString(),
                        IsActif = (bool)dt.Rows[i]["IsActif"]
                    });
                }

                dgParticipants.ItemsSource = participants;
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


     private void BtnAjouterPart_Click(object sender, RoutedEventArgs e)
        {
            //participants.Add(new Participant() { Matricule = 11, Prenom = "Thomas", Nom = "Cliche", Genre = 'M', Niveau = "Professionnel", Email = "Tomcliche@gmail.com", IsActif = true });


     //       InputDialog inputDialog0 = new InputDialog("Ajout", "Veuillez saisir le matricule", "0000000");
     //       int matriculeAj = 0;

     //       if (inputDialog0.ShowDialog() == true)
     //       {
     //           matriculeAj = int.Parse(inputDialog0.Response);
     //       }

     //       InputDialog inputDialog1 = new InputDialog("Ajout", "Veuillez saisir le prénom", "John");
     //       string prenomAj = "";

     //       if (inputDialog1.ShowDialog() == true)
     //       {
     //           prenomAj = inputDialog1.Response;
     //       }

     //       InputDialog inputDialog2 = new InputDialog("Ajout", "Veuillez saisir le nom", "Doe");
     //       string nomAj = "";

     //       if (inputDialog2.ShowDialog() == true)
     //       {
     //           nomAj = inputDialog2.Response;
     //       }

     //       InputDialog inputDialog3 = new InputDialog("Ajout", "Veuillez saisir le genre", "M/F");
     //       char genreAj = '\0';

     //       if (inputDialog3.ShowDialog() == true)
     //       {
     //           genreAj = Convert.ToChar(inputDialog3.Response);
     //       }

     //       InputDialog inputDialog4 = new InputDialog("Ajout", "Veuillez saisir le niveau", "Débutant/Intermédiraire/Professionnel");
     //       string niveauAj = "";

     //       if (inputDialog4.ShowDialog() == true)
     //       {
     //           niveauAj = inputDialog4.Response;
     //       }

     //       InputDialog inputDialog5 = new InputDialog("Ajout", "Veuillez saisir le email", "aaaa@cstj.qc.ca");
     //       string emailAj = "";

     //       if (inputDialog5.ShowDialog() == true)
     //       {
     //           emailAj = inputDialog5.Response;
     //       }

     //       participants.Add(new Participant()
     //       {
     //           Matricule = matriculeAj,
     //           Prenom = prenomAj,
     //           Nom = nomAj,
     //           Genre = genreAj,
     //           Niveau = niveauAj,
     //           Email = emailAj
     //       });

     //       ReecrireFichier();

            // Décharger de la grille principal de mainwindow mainwindow le contenu actuel (le contenu de UCGestionPart)
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.gPrincipal.Children.Remove(mw.ContenuEcran);

            // Changement du contenu du user control UCAjourPArt
            mw.ContenuEcran = EcrModSupp;
            Grid.SetRow(mw.ContenuEcran, 1);
            mw. gPrincipal.Children.Add(mw.ContenuEcran);

          }

        private void BtnModifierPart_Click(object sender, RoutedEventArgs e)
        {
            
            int valMAt = (dgParticipants.SelectedItem as Participant).Matricule;
            string valNom = (dgParticipants.SelectedItem as Participant).Nom;
            string valPrenom = (dgParticipants.SelectedItem as Participant).Prenom;
            string valNiv = (dgParticipants.SelectedItem as Participant).Niveau;
            char valGen = (dgParticipants.SelectedItem as Participant).Genre;
            string valEmail = (dgParticipants.SelectedItem as Participant).Email;
            bool valIsact = (dgParticipants.SelectedItem as Participant).IsActif;

            if (mat!=valMAt)
            {
                MessageBox.Show("Il n'est pas possible de modifier le matricule du participant");
                 (dgParticipants.SelectedItem as Participant).Matricule = mat;
                 (dgParticipants.SelectedItem as Participant).Nom =no;
                 (dgParticipants.SelectedItem as Participant).Prenom =pre;
                 (dgParticipants.SelectedItem as Participant).Niveau =niv;
                 (dgParticipants.SelectedItem as Participant).Genre =gen;
                 (dgParticipants.SelectedItem as Participant).Email =ema;
                 (dgParticipants.SelectedItem as Participant).IsActif =isAct;
            }
            else
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=gestionparticipants;UID=root;PASSWORD=;");
                try
                {
                    conn.Open();

                    string cmdString = $"UPDATE participant Set Matricule = @valMAt, Nom = @valNom, Prenom = @valPrenom, Genre = @valGen, Niveau = @valNiv, Email = @valEmail, IsActif = valIsact WHERE Matricule = @valMAt";

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
            }
        }

        // Variables qui garderont les valeurs initiales des champs de la ligne sélectionnée dans l DataGrid
        public int mat;
        public string no, pre, niv, ema;
        public char gen;
        public bool isAct;
        
        private void LigneDg_dblClick(object sender, MouseButtonEventArgs e)
        {
            mat = (dgParticipants.SelectedItem as Participant).Matricule;
            no = (dgParticipants.SelectedItem as Participant).Nom;
            pre = (dgParticipants.SelectedItem as Participant).Prenom;
            niv = (dgParticipants.SelectedItem as Participant).Niveau;
            gen = (dgParticipants.SelectedItem as Participant).Genre;
            ema = (dgParticipants.SelectedItem as Participant).Email;
            isAct = (dgParticipants.SelectedItem as Participant).IsActif;
        }

        private void BtnModSup_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.gPrincipal.Children.Remove(mw.ContenuEcran);

            // Changement du contenu du user control UCAjourPArt
            mw.ContenuEcran = EcrModSupp;
            Grid.SetRow(mw.ContenuEcran, 1);
            mw.gPrincipal.Children.Add(mw.ContenuEcran);
        }

        private void BtnSuppPart_Click(object sender, RoutedEventArgs e)
        {
            int valMat = (dgParticipants.SelectedItem as Participant).Matricule;
            if (dgParticipants.SelectedItem != null)
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=partipants;UID=root;PASSWORD=;");
                try
                {
                    conn.Open();

                    string cmdString = "DELETE FROM Participant WHERE Matricule = @valMat";

                    MySqlCommand cmd = new MySqlCommand(cmdString, conn);

                    cmd.Parameters.AddWithValue("@valMat", valMat);

                    cmd.ExecuteReader();

                    participants.Remove(dgParticipants.SelectedItem as Participant);

                    MessageBox.Show("Le participant a été exécuté avec succès");

                }catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void BtnListView_Click(object sender, RoutedEventArgs e)
        {
            ListViewUI listViewUI = new ListViewUI();
            listViewUI.Show();
        }

        private void BtnQuitter_Click(object sender, RoutedEventArgs e)
        {
           // ReecrireFichier();
        }

        

        //public static void ReecrireFichier()
        //{
        //    string json = JsonConvert.SerializeObject(participants, Formatting.Indented);

        //    System.IO.File.WriteAllText("fichierParticipants.json", json);
        //}

    }
}
