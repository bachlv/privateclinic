using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Tes4.GUI.Report
{
    public partial class Medicine_Used : DevExpress.XtraEditors.XtraForm
    {
        private static MySqlConnection dbConn;
        public Medicine_Used()
        {
            InitializeComponent();
            InitializeDB();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            String query = String.Format("SELECT item_name, unit, sum(quantity) as tong, count(item_name) as solandung FROM PriClinic.BillItem, Bill, MedicalRecord" +
            " where MedicalRecord.record_id = Bill.record_id and Bill.bill_id = BillItem.bill_id and TreatDate like '%-{0}-%'"+
            " group by item_name, unit"+
            " order by tong DESC", comboBox1.Text);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string item_name = reader["item_name"].ToString();
                string unit = reader["unit"].ToString();
                string sum = reader["tong"].ToString();
                string used_count = reader["solandung"].ToString();
                ListViewItem item = new ListViewItem(new String[] { item_name,unit,sum,used_count});
                listView1.Items.Add(item);
              
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            reader.Close();

            dbConn.Close();

        }



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
    }
}