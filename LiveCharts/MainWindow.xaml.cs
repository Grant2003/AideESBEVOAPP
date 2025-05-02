using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using ProjetLiveCharts;

namespace LiveCharts
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Bilan> _bilan;
        private SeriesCollection _sc;
        public string[] Labels { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private Bilan _selectedBilan;

        public static ObservableCollection<Bilan> ChargerFichier()
        {
            string pathFichier = @"BilanActivites.json";
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathFichier);
            string jsonData = File.ReadAllText(jsonPath);

            return JsonConvert.DeserializeObject<ObservableCollection<Bilan>>(jsonData) ?? new ObservableCollection<Bilan>();
        }

        public ObservableCollection<Bilan> Bilan
        {
            get { return _bilan; }
            set
            {
                if (_bilan != value)
                {
                    _bilan = value;
                    OnPropertyChanged(nameof(Bilan));
                }
            }
        }


        public SeriesCollection SC
        {
            get { return _sc; }
            set
            {
                if (_sc != value)
                {
                    _sc = value;
                    OnPropertyChanged(nameof(SC));
                }
            }
        }

        public Bilan SelectedBilan
        {
            get { return _selectedBilan; }
            set
            {
                if (_selectedBilan != value)
                {
                    _selectedBilan = value;
                    OnPropertyChanged(nameof(SelectedBilan));
                }
            }
        }

        public MainWindow()
        {
            Bilan = ChargerFichier();
            InitializeComponent();

            SC = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Chiffre d'affaire",
                    Values = new ChartValues<double>(Bilan.Select(b => (double)b.ChiffreAffaire))
                },
                new LineSeries
                {
                    Title = "Marge Brute",
                    Values = new ChartValues<double>(Bilan.Select(b => (double)b.MargeBrute)),
                    PointGeometry = DefaultGeometries.Circle,
                    Fill = Brushes.Transparent
                }
            };

            Labels = Bilan.Select(b => b.Mois).ToArray();
            DataContext = this;
            SelectedBilan = Bilan.FirstOrDefault();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedBilan = listView.SelectedItem as Bilan;

            if (selectedBilan != null)
            {
                txtBoxMois.Text = selectedBilan.Mois;
                txtBoxCA.Text = selectedBilan.ChiffreAffaire.ToString();
                txtBoxMB.Text = selectedBilan.MargeBrute.ToString();
                txtBoxTM.Text = selectedBilan.TauxMarge.ToString();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBilan != null)
            {
                SelectedBilan.Mois = txtBoxMois.Text;
                SelectedBilan.ChiffreAffaire = float.TryParse(txtBoxCA.Text, out float ca) ? ca : 0;
                SelectedBilan.MargeBrute = float.TryParse(txtBoxMB.Text, out float mb) ? mb : 0;

                SelectedBilan.TauxMarge = SelectedBilan.MargeBrute / SelectedBilan.ChiffreAffaire;
                txtBoxTM.Text = SelectedBilan.TauxMarge.ToString("P2");

                UpdateChartData();

                MessageBox.Show($"Bilan for {SelectedBilan.Mois} updated!");
            }
            else
            {
                MessageBox.Show("No Bilan selected!");
            }
        }





        private void UpdateChartData()
        {
            SC[0].Values = new ChartValues<double>(Bilan.Select(b => (double)b.ChiffreAffaire));
            SC[1].Values = new ChartValues<double>(Bilan.Select(b => (double)b.MargeBrute));

            Labels = Bilan.Select(b => b.Mois).ToArray();
        }





        // Method to notify property changes
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
