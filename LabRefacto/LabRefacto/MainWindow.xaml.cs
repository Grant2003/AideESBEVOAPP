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

namespace LabRefacto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnCalculer_Click(object sender, RoutedEventArgs e)
        {
            bool validerMath = ValiderEntreeMaths(txtMaths.Text);
            bool validerInfo =  ValiderEntreeInfo(txtInfo.Text); 
            Session session = Session.principale;
            if(!validerMath || !validerInfo)
            {
                return;
            }

            if (rdbPrin.IsChecked == false)
            {
                session = Session.rattrapage;
            }
 

            Etudiant etu = new Etudiant(Convert.ToDouble(txtInfo.Text), Convert.ToDouble(txtMaths.Text), txtMat.Text, txtNom.Text,session);
            txbResume.Text = "Matricule: " + txtMat.Text + "\nNom: " + txtNom.Text + "\nMoyenne: " + etu.CalculerMoyenne();

        }

        private bool ValiderEntreeInfo(string info)
        {
            double resultat;

            if (!double.TryParse(info, out resultat))
            {
                MessageBox.Show("Erreur. Veuillez entrer une valeur numérique!");
                txtInfo.Focus();
                return false;
            }
            if (((Convert.ToDouble(info) > 10))
                || (Convert.ToDouble(info) < 0))
            {
                MessageBox.Show("Erreur. Veuillez entrer une valeur entre 0 et 10!");
                txtMaths.Focus();
                return false;
            }
            return true;
        }
        private bool ValiderEntreeMaths(string math)
        {
            double resultat;
            if (!double.TryParse(math, out resultat))
            {
                MessageBox.Show("Erreur. Veuillez entrer une valeur numérique!");
                txtMaths.Focus();
                return false;
            }
            if (((Convert.ToDouble(math) > 10))
                ||(Convert.ToDouble(math) < 0))
            {
                MessageBox.Show("Erreur. Veuillez entrer une valeur entre 0 et 10!");
                txtMaths.Focus();
                return false;
            }
            return true;
        }
    }
}