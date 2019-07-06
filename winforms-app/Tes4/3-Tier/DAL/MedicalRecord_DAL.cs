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
    class MedicalRecord_DAL
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
        public int Insert (int padId, String dis, String sym, String dat)
        {
            String query = string.Format("INSERT INTO MedicalRecord(patient_id,disease,symptom,TreatDate) VALUES " +
                "('{0}', '{1}','{2}','{3}')",
                 padId,dis,sym, dat);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;    
            dbConn.Close();
            return id;
        }
        public void Delete(int med_ID)
        {
            String query = string.Format("Delete From MedicalRecord"
                +
                " WHERE record_id={0}",
                med_ID);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
        }


    }
}
