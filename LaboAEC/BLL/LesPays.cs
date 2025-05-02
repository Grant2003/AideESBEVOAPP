using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class LesPays
    {

        public static ObservableCollection<Pays> pays = new ObservableCollection<Pays>();
        public static void ChargerListeFilms()
        {
            DataTable dt = AccessDB.ConnecterBDPays();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                pays.Add(new Pays
                {
                    PaysNom = dt.Rows[i]["NomPays"].ToString(),
                });
            }
        }
    }
}
