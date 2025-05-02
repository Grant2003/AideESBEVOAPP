using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class Participant: INotifyPropertyChanged
    {

        private const string NOM_FICHIER = "fichierParticipants.json";

        // Méthode qui permet de lire le fichier json et de le déserialiser
        //public static void ChargerFichier()
        //{
        //    string json = File.ReadAllText(NOM_FICHIER);

        //    UCGestionPart.participants = JsonConvert.DeserializeObject<ObservableCollection<Participant>>(json);
        //}


        public void NotifyProprertyChanged(string proprerty)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(proprerty));
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        private int matricule;
        public int Matricule 
        {
            get { return matricule;}
            set
            {
                if (this.matricule != value)
                { 
                    this.matricule = value;
                    this.NotifyProprertyChanged("Matricule");
                }
            }
        }

        private string prenom;
        public string Prenom
        {
            get { return prenom; }
            set
            {
                if (this.prenom != value)
                {
                    this.prenom = value;
                    this.NotifyProprertyChanged("Prenom");
                }
            }
        }

        private string nom;
        public string Nom
        {
            get { return nom; }
            set
            {
                if (this.nom != value)
                {
                    this.nom = value;
                    this.NotifyProprertyChanged("Nom");
                }
            }
        }
        private char genre;
        public char Genre
        {
            get { return genre; }
            set
            {
                if (this.genre != value)
                {
                    this.genre = value;
                    this.NotifyProprertyChanged("Genre");
                }
            }
        }
        private string niveau;
        public string Niveau
        {
            get { return niveau; }
            set
            {
                if (this.niveau != value)
                {
                    this.niveau = value;
                    this.NotifyProprertyChanged("Niveau");
                }
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.NotifyProprertyChanged("Email");
                }
            }
        }
        private bool isActif;
        public bool IsActif
        {
            get { return isActif; }
            set
            {
                if (this.isActif != value)
                {
                    this.isActif = value;
                    this.NotifyProprertyChanged("IsActif");
                }
            }
        }

    }

    public enum Genre { M, F}

    public enum Niveau 
    { 
        Débutant,
        Intermédiaire,
        Professionnel
    }
}
