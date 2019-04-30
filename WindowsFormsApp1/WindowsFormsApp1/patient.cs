using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    class patient
    {
        private const String SERVER = "localhost";
        private const String DATABASE = "sys";
        private const String UID = "root";
        private const String PASSWORD = "LacLS1810";
        private static MySqlConnection dbConn;

        public int idPatient { get; private set; }

        public String Name { get; private set; }

        public String DOB { get; private set; }

        public String Gender { get; private set; }

        public String Address { get; private set; }

        private patient(int idP, String n, String dob, String ge, String addres)
        {
            idPatient = idP;
            Name = n;
            DOB = dob;
            Gender = ge;
            Address = addres;
        }

         public static void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connString = builder.ToString();

            builder = null;

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
        public static List<patient> GetPatients()
        {
            List<patient> patients = new List<patient>();

            String query = "SELECT * FROM patient";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["idPatient"];
                String n = reader["Name"].ToString();
                String yob = reader["YOB"].ToString();
                String gender = reader["Gender"].ToString();
                String Addr = reader["Address"].ToString();
                patient p = new patient(id, n, yob,gender,Addr);

                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public static patient Insert(String n, String yob, String gender, String Addr)
        {
            String query = string.Format("INSERT INTO patient(Name,YOB,Gender,Address) VALUES ('{0}', '{1}','{2}','{3}')",n,yob,gender,Addr);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            patient p = new patient(id, n, yob, gender, Addr);

            dbConn.Close();

            return p;

        }
        public void Update(String n, String yob, String gender, String Addr)
        {
            String query = string.Format("UPDATE patient SET Name='{0}', YOB='{1}',Gender='{2}',Address='{3}' WHERE idPatient={4}", n,yob,gender,Addr,idPatient);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public void Delete()
        {
            String query = string.Format("DELETE FROM patient WHERE idPatient={0}", idPatient);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public static List<patient> Query(int searching)
        {
            List<patient> patients = new List<patient>();

            String query = String.Format("SELECT * FROM patient WHERE idPatient LIKE '%{0}%'", searching);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["idPatient"];
                String n = reader["Name"].ToString();
                String yob = reader["YOB"].ToString();
                String gender = reader["Gender"].ToString();
                String Addr = reader["Address"].ToString();
                patient p = new patient(id, n, yob, gender, Addr);

                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        } 
    }
}
