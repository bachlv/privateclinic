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
    class query_medicine_patient_DAL
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
        public List<query_medic_patientDTO> Query(string dieukien)
        {
            List<query_medic_patientDTO> patients = new List<query_medic_patientDTO>();

            String query = String.Format("SELECT * FROM Patient, MedicalRecord WHERE MedicalRecord.patient_id = Patient.patient_id and " + dieukien);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["patient_id"]);
                String name = reader["name"].ToString();
                String dis = reader["disease"].ToString();
                String sym = reader["symptom"].ToString();
                String date = reader["TreatDate"].ToString();
                String ph = reader["phone"].ToString();
                query_medic_patientDTO p = new query_medic_patientDTO(id, name, date, dis, sym, ph);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public List<query_medic_patientDTO> LoadAll()
        {
            List<query_medic_patientDTO> patients = new List<query_medic_patientDTO>();

            String query = String.Format("SELECT MedicalRecord.patient_id,name,TreatDate,disease, symptom,phone FROM PriClinic.MedicalRecord, Patient where MedicalRecord.patient_id = Patient.patient_id;");

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["patient_id"]);
                String name  = reader["name"].ToString();
                String dis = reader["disease"].ToString();
                String sym = reader["symptom"].ToString();
                String date = reader["TreatDate"].ToString();
                String ph = reader["phone"].ToString();
                query_medic_patientDTO p = new query_medic_patientDTO(id, name, date,dis, sym,ph);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }


    }
}
