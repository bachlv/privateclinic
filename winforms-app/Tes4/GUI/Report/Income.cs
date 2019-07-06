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
using DevExpress.XtraCharts;
using Tes4.GUI.Report;
namespace Tes4.GUI.Report
{
    public partial class Income : DevExpress.XtraEditors.XtraForm
    {
        private static MySqlConnection dbConn;
        public List<SeriesPoint> series1;
        public Income()
        {
            InitializeComponent();
            InitializeDB();
            series1 = new List<SeriesPoint>();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            String query = String.Format("SELECT TreatDate,count(distinct patient_id) as sobenhnhan, sum(total) as doanhthu "+
            " FROM PriClinic.Bill, MedicalRecord "+
            " where Bill.record_id = MedicalRecord.record_id  and TreatDate like '%-{0}-%'" +
            " group by TreatDate"
            , comboBox1.Text);
            
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string Treat_date = reader["TreatDate"].ToString();
                string num_patient = reader["sobenhnhan"].ToString();
                string sum = reader["doanhthu"].ToString();
               
                ListViewItem item = new ListViewItem(new String[] { Treat_date, num_patient, sum});
                listView1.Items.Add(item);
                series1.Add(new SeriesPoint(Treat_date, float.Parse(sum)));
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        
            reader.Close();
            dbConn.Close();
            if (listView1.Items.Count == 0 && comboBox1.SelectedIndex!= -1)
            {
                MessageBox.Show("Không có dữ liệu ở tháng vừa chọn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.SelectedIndex = -1;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tháng cần vẽ mô hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Reader reader = new Reader(series1);
                reader.ShowDialog();
            }
        }


    }
}