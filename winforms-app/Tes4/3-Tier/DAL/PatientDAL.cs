using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Tes4._3_Tier.DTO;

namespace Tes4._3_Tier.DAL
{
    class PatientDAL
    {
     
        private static MySqlConnection dbConn;

        public  void InitializeDB()
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
        public  List<PatientDTO> GetPatients()
        {
            List<PatientDTO> patients = new List<PatientDTO>();

            String query = "SELECT * FROM Patient";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["patient_id"];
                String n = reader["name"].ToString();
                String yob = reader["birthday"].ToString();
                String gender = reader["gender"].ToString();
                String Addr = reader["address"].ToString();
                String phone = reader["phone"].ToString();
                PatientDTO p = new PatientDTO(id, n, yob, gender, Addr, phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public PatientDTO Insert(String n, String yob, String gender, String Addr, String pnNum)
        {
            String query = string.Format("INSERT INTO Patient(name,birthday,gender,address,phone) VALUES ('{0}', '{1}','{2}','{3}','{4}')",
                 n, yob, gender, Addr, pnNum);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            PatientDTO p = new PatientDTO(id, n, yob, gender, Addr, pnNum);
            dbConn.Close();

            return p;

        }

        public void Update(int id,String n, String yob, String gender, String Addr, String pnNum)
        {
            String query = string.Format("UPDATE Patient SET name='{0}', birthday='{1}',gender='{2}',address='{3}',phone='{4}' " +
                "WHERE patient_id={5}",
                n, yob, gender, Addr, pnNum,id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }

        public void Delete(int id)
        {
            String query = string.Format("DELETE FROM Patient WHERE patient_id={0}",id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }

        public  List<PatientDTO> FastQuery(int searching)
        {
            List<PatientDTO> patients = new List<PatientDTO>();

            String query = String.Format("SELECT * FROM Patient WHERE patient_id LIKE '%{0}%'", searching);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["patient_id"];
                String n = reader["name"].ToString();
                String yob = reader["birthday"].ToString();
                String gender = reader["gender"].ToString();
                String Addr = reader["address"].ToString();
                String phone = reader["phone"].ToString();
                PatientDTO p = new PatientDTO(id, n, yob, gender, Addr, phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }

        //join table với medical record để trả lại kết quả
        public List<PatientDTO> Query(string dieukien)
        {
            List<PatientDTO> patients = new List<PatientDTO>();
          
            String query = String.Format("SELECT * FROM Patient WHERE "+dieukien);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["patient_id"];
                String n = reader["name"].ToString();
                String yob = reader["birthday"].ToString();
                String gender = reader["gender"].ToString();
                String Addr = reader["address"].ToString();
                String phone = reader["phone"].ToString();
                PatientDTO p = new PatientDTO(id, n, yob, gender, Addr, phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public List<string> GetPatients_Name()
        {
            List<string> name_suggestion = new List<string>();

            String query = "SELECT * FROM PriClinic.Patient";

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

        public int GetPatient_ID(string name)
        {
            int ID = 0;

            String query = String.Format("SELECT * FROM Patient WHERE name LIKE '%{0}%'", name);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                ID = Convert.ToInt32(reader["patient_id"]);
               
            }

            reader.Close();

            dbConn.Close();

            return ID;
        }


    }
}
