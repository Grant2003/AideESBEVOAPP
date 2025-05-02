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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog(string titre, string question, string ReponseParDefault = "")
        {
            InitializeComponent();

            lblQuestion.Content = question;
            txtReponse.Text = ReponseParDefault;
            Title = titre;

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtReponse.SelectAll();
            txtReponse.Focus();
        }

        private void BtnDialogOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Response
        {
            get { return txtReponse.Text; }
        }

        private void BtnDialogCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
