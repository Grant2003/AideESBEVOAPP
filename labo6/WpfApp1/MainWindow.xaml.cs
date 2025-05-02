using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
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
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Propriété et objet charger le UserControl dans MainWindow
        public UserControl ContenuEcran {  get; set; }
        public UCGestionPart GestPart = new UCGestionPart();

        // Instanciation d'un UserControl pour UCIconPart
        public UserControl ContenuColonne2 { get; set; }
        public UCIconPart UCIcone = new UCIconPart();


        public MainWindow()
        {
            InitializeComponent();

            ContenuEcran = GestPart;
            Grid.SetRow(ContenuEcran, 0);
            Grid.SetColumn(ContenuEcran, 0);

            gPrincipal.Children.Add(ContenuEcran);

            ContenuColonne2 = UCIcone;
            Grid.SetRow(ContenuColonne2 , 0);
            Grid.SetColumn(ContenuColonne2, 2);

            gPrincipal.Children.Add(ContenuColonne2);
        }
    }
}
