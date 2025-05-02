using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class AccessDB
    {
        public static DataTable ConnecterBDFilm()
        {

            MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=lesfilms;UID=root;PASSWORD=;");

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from film", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet(); // using System.data

                adapter.Fill(ds, "film");

                var dt = ds.Tables["film"];


                return dt;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public static DataTable ConnecterBDPays()
        {

            MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=lesfilms;UID=root;PASSWORD=;");

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from pays", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet(); // using System.data

                adapter.Fill(ds, "pays");

                var dt = ds.Tables["pays"];


                return dt;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
