using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tes4._3_Tier.DTO;
namespace Tes4._3_Tier.DAL
{
    class TreatmentDAL
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
        public List<TreatmentDTO> GetTreatments()
        {
            List<TreatmentDTO> treatments = new List<TreatmentDTO>();

            String query = "SELECT * FROM Treatment";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["treat_id"];
                String n = reader["treat_name"].ToString();
                String sym = reader["symptom"].ToString();
                TreatmentDTO p = new TreatmentDTO(id, n, sym);
                treatments.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return treatments;
        }
        public TreatmentDTO Insert(String n, String sy)
        {
            String query = string.Format("INSERT INTO Treatment(treat_name,symptom) VALUES " +
                "('{0}', '{1}')",
                 n, sy);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            TreatmentDTO p = new TreatmentDTO(id, n,sy);
            dbConn.Close();

            return p;
        }
        public void Update(int id, String name,String sy)
        {
            String query = string.Format(
                "UPDATE Treatment SET treat_name='{0}', symptom='{1}'"
                +
                "WHERE treat_id={2}",
                name,sy,id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public void Delete(int id)
        {
            String query = string.Format("DELETE FROM Treatment WHERE treat_id={0}", id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }


        public List<string> GetNameDisease_Suggestion()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.TreatmentSuggest";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String n = reader["treatment_name"].ToString();
                name_suggestion.Add(n);
            }

            reader.Close();

            dbConn.Close();

            return name_suggestion;
        }

        public List<string> GetSymptom_Suggestion()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.TreatmentSuggest";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String n = reader["symptom"].ToString();
                name_suggestion.Add(n);
            }

            reader.Close();

            dbConn.Close();

            return name_suggestion;
        }


        public string Get_Symtom(string dieukien)
        {
            String query = String.Format("SELECT * FROM TreatmentSuggest WHERE treatment_name LIKE '%{0}%'", dieukien);
            String quantity = "";
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                quantity = reader["symptom"].ToString();
            }
            reader.Close();

            dbConn.Close();

            return quantity;
        }


        public List<string> GetNameDisease_Owner()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.Treatment";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String n = reader["treat_name"].ToString();
                name_suggestion.Add(n);
            }

            reader.Close();

            dbConn.Close();

            return name_suggestion;
        }

        public List<string> GetSymptom_Owner()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.Treatment";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String n = reader["symptom"].ToString();
                name_suggestion.Add(n);
            }

            reader.Close();

            dbConn.Close();

            return name_suggestion;
        }
    }
}
