using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tes4._3_Tier.DAL
{
    class GeoInfo_DAL
    {
        private static MySqlConnection dbConn;
        //Connect to DataBase
        public void InitializeDB()
        {
            string connString = ConfigurationManager.AppSettings["GeoData"];

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

            Application.ApplicationExit += (sender, args) =>
            {
                if (dbConn != null)
                {
                    dbConn.Dispose();
                    dbConn = null;
                }
            };

        }

        public List<string> getProvince()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM GeoInfo.Province";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String n = reader["name"].ToString();
                name_suggestion.Add(n);
            }

            reader.Close();
            dbConn.Close();

            return name_suggestion;
        }
        public List<string> getDistrict(String dieukien)
        {
            List<string> name_District = new List<string>();
            int tp_id = 0;

            String query = String.Format("SELECT matp FROM GeoInfo.Province  WHERE name LIKE '%{0}%'", dieukien);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tp_id = Convert.ToInt32(reader["matp"]);
            }
            reader.Close();
            dbConn.Close();

            query = String.Format("SELECT name FROM GeoInfo.District  WHERE matp LIKE '%{0}%'", tp_id);
            cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String name = reader["name"].ToString();
                name_District.Add(name);
            }
            reader.Close();

            dbConn.Close();

            return name_District;
        }

        public List<string> getTown(String dieukien)
        {
            List<string> name_Town = new List<string>();
            int qh_id = 0;

            String query = String.Format("SELECT maqh FROM GeoInfo.District  WHERE name LIKE '%{0}%'", dieukien);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                qh_id = Convert.ToInt32(reader["maqh"]);
            }
            reader.Close();
            dbConn.Close();


            query = String.Format("SELECT name FROM GeoInfo.Town  WHERE maqh LIKE '%{0}%'", qh_id);
            cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                String name = reader["name"].ToString();
                name_Town.Add(name);
            }
            reader.Close();

            dbConn.Close();

            return name_Town;
        }


    }
}
