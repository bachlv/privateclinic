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
    class billDAL
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
        public int Insert(int medre_id)
        {
            String query = string.Format("INSERT INTO Bill(record_id) VALUES " +
                "('{0}')",
                 medre_id);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;
            dbConn.Close();
            return id;
        }

        public void Delete(int bill_ID)
        {
            String query = string.Format("Delete From Bill"
                +
                " WHERE bill_id={0}",
                bill_ID);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
        }


    }
}
