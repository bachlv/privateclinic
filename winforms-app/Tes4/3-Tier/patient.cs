using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    class patient
    {
        private const String SERVER = "35.185.184.170";
        private const String DATABASE = "PriClinic";
        private const String UID = "lac";
        private const String PASSWORD = "laclaclaclaclac";
        private static MySqlConnection dbConn;

        public int patient_id { get; private set; }

        public String name { get; private set; }

        public String birthday { get; private set; }

        public String gender { get; private set; }

        public String addres { get; private set; }

        public String phone { get; private set; }

        private patient(int idP, String n, String dob, String ge, String addr, String pnNum)
        {
            patient_id = idP;
            name = n;
            birthday = dob;
            gender = ge;
            addres = addr;
            phone = pnNum;
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
                String Addr = reader["addres"].ToString();
                String phone = reader["phone"].ToString();
                patient p = new patient(id, n, yob,gender,Addr,phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public static patient Insert(String n,String yob, String gender, String Addr,String pnNum)
        {
            String query = string.Format("INSERT INTO Patient(name,birthday,gender,addres,phone) VALUES ('{0}', '{1}','{2}','{3}','{4}')", 
                n,yob,gender,Addr, pnNum);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            patient p = new patient(id, n, yob, gender, Addr, pnNum);

            dbConn.Close();

            return p;

        }
    
        public void Update(String n, String yob, String gender, String Addr, String pnNum)
         { 
            String query = string.Format("UPDATE Patient SET name='{0}', birthday='{1}',gender='{2}',addres='{3}',phone='{4}' WHERE patient_id={5}",
                n,yob,gender,Addr,pnNum, patient_id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public void Delete()
        {
            String query = string.Format("DELETE FROM Patient WHERE patient_id={0}", patient_id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
        public static List<patient> FastQuery(int searching)
        {
            List<patient> patients = new List<patient>();

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
                String Addr = reader["addres"].ToString();
                String phone = reader["phone"].ToString();
                patient p = new patient(id, n, yob, gender, Addr,phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public static List<patient> QueryByName(string name)
        {
            List<patient> patients = new List<patient>();

            String query = String.Format("SELECT * FROM Patient WHERE name LIKE '%{0}%'", name);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["patient_id"];
                String n = reader["name"].ToString();
                String yob = reader["birthday"].ToString();
                String gender = reader["gender"].ToString();
                String Addr = reader["addres"].ToString();
                String phone = reader["phone"].ToString();
                patient p = new patient(id, n, yob, gender, Addr, phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
        public static List<patient> QueryByPhone(string phoneNum)
        {
            List<patient> patients = new List<patient>();

            String query = String.Format("SELECT * FROM Patient WHERE phone LIKE '%{0}%'", phoneNum);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["patient_id"];
                String n = reader["name"].ToString();
                String yob = reader["birthday"].ToString();
                String gender = reader["gender"].ToString();
                String Addr = reader["addres"].ToString();
                String phone = reader["phone"].ToString();
                patient p = new patient(id, n, yob, gender, Addr, phone);
                patients.Add(p);
            }

            reader.Close();

            dbConn.Close();

            return patients;
        }
    }
}
