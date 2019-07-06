using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using System.Windows.Forms;
using System.Configuration;

namespace Tes4._3_Tier.DAL
{
    class MedicineDAL
    {
        private static MySqlConnection dbConn;

        public void InitializeDB()
        {
            string connString = ConfigurationManager.AppSettings["MySQL"];

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
        public List<MedicineDTO> GetPatients()
        {
            List<MedicineDTO> medicines = new List<MedicineDTO>();

            String query = "SELECT * FROM Medicine";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["med_id"];
                String n = reader["name"].ToString();
                String quantity = reader["quantity"].ToString();
                float price = (float)reader["price"];
                MedicineDTO p = new MedicineDTO(id, n, quantity, price);
                medicines.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return medicines;
        }
        public List<string> GetMedicines_Suggestion()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.MedicineSupply";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String n = reader["med_name"].ToString();
                name_suggestion.Add(n);
            }

            reader.Close();

            dbConn.Close();

            return name_suggestion;
        }
        public List<string> GetMedicines_Owner()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.Medicine";

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
        public MedicineDTO Insert(String n, String quantity, float price)
        {
            String query = string.Format("INSERT INTO Medicine(name,quantity,price) VALUES " +
                "('{0}', '{1}','{2}')",
                 n, quantity, price);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;
            MedicineDTO p = new MedicineDTO(id, n, quantity, price);
            dbConn.Close();
            return p;

        }
        public void Update(int id, String n, String quantity, float price)
        {
            String query = string.Format(
                "UPDATE Medicine SET name='{0}', quantity='{1}',price='{2}'"
                +
                "WHERE med_id={3}",
                n, quantity, price, id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public void Delete(int id)
        {
            String query = string.Format("DELETE FROM Medicine WHERE med_id={0}", id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public String get_Quantity(String NameOfPill)
        {
            String query = String.Format("SELECT * FROM Medicine WHERE name LIKE '%{0}%'", NameOfPill);
            String quantity = "";
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                quantity = reader["quantity"].ToString();
            }
            reader.Close();

            dbConn.Close();

            return quantity;
        }
        public float get_price(String NameOfPill)
        {
            String query = String.Format("SELECT * FROM Medicine WHERE name LIKE '%{0}%'", NameOfPill);
            float quantity=0;
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                quantity = (float)reader["price"];
            }
            reader.Close();

            dbConn.Close();

            return quantity;
        }

    }
}
