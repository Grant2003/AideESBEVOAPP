//Par Anthony Grenier 
//Mat: 2071623
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
using System.Windows.Shapes;

namespace tp1EVO
{
    /// <summary>
    /// Interaction logic for ModalSuppression.xaml
    /// </summary>
    public partial class ModalSuppression : Window
    {
        private MainWindow _mainWindow;

        public ModalSuppression(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

        }
        private void Click_BtnAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_btnConfirmerSuppr(object sender, RoutedEventArgs e)
        {
            _mainWindow.SupprimerFilm();
            this.Close();

        }

    }
}
