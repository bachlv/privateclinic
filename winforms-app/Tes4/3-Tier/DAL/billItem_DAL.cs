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
    class billItem_DAL
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
        public void Insert(int billId, String name ,String un, int quan,String use, float pr)
        {
            String query = string.Format("INSERT INTO BillItem(bill_id,item_name,unit,quantity,item_usage,price) VALUES " +
             "({0}, '{1}','{2}',{3},'{4}',{5})",
              billId, name, un, quan,use,pr);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
            
        }
    }
}
