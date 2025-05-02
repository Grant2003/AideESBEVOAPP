using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Films
    {
        public static ObservableCollection<Film> films = new ObservableCollection<Film>();
        public static ObservableCollection<int> annees = new ObservableCollection<int>();
        public static void ChargerListeFilms()
        {
            DataTable dt = AccessDB.ConnecterBDFilm();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                films.Add(new Film
                {
                    Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                    Titre = dt.Rows[i]["Titre"].ToString(),
                    Annee = Convert.ToInt32(dt.Rows[i]["Annee"]),
                    Pays = dt.Rows[i]["Pays"].ToString()
                });
            }
        }
        public static void ListeAnnees()
        {
            foreach(int annee in films.Select(f => f.Annee).Distinct())
            {
                annees.Add(annee);
            }
        }


    }
}
