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
using Tes4._3_Tier.DTO;
using Tes4.GUI.PrintBill;

namespace Tes4.GUI.Patient
{
    public partial class Bill : DevExpress.XtraEditors.XtraForm
    {
        private static MySqlConnection dbConn;
        private int id;
        int patient_id, bill_id;
        string name_pa, add, dob, sym, treat, gender;
        float Sum,treat_cost;
        List<bill_ItemDTO> list_bill_item = new List<bill_ItemDTO>();
        public Bill()
        {
            InitializeComponent();
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

        private void Bill_Load(object sender, EventArgs e)
        {
            InitializeDB();
            GetData();
        }
        public void GetData()
        {
            DataTable dt = new DataTable();
           
            String query = String.Format("SELECT bill_id as 'Mã bệnh nhân',name as 'Tên bệnh nhân', TreatDate as 'Ngày khám', treatment as 'Tiền khám', medicine as 'Tiền thuốc' From MedicalRecord, Bill, Patient where MedicalRecord.patient_id = Patient.patient_id " +
                "and Bill.record_id= MedicalRecord.record_id");

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;           
            }
            dbConn.Close();
        }

        private void prinntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows !=null)
            {
                //variable 
                
                id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                String query1 = String.Format("SELECT * FROM PriClinic.BillItem where bill_id={0}",id);
                MySqlCommand cmd = new MySqlCommand(query1, dbConn);
                dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string Me_name = reader["item_name"].ToString();
                    string Me_us = reader["item_usage"].ToString();
                    string Me_ut = reader["unit"].ToString();
                    int Me_quan = Convert.ToInt32(reader["quantity"]);
                    bill_ItemDTO Item = new bill_ItemDTO(Me_name, Me_ut, Me_quan, Me_us);
                    list_bill_item.Add(Item);
                }
                reader.Close();
                dbConn.Close();
                String query2 = String.Format("SELECT Patient.patient_id,bill_id,Patient.name,address,birthday,gender,symptom,disease,total,treatment  From MedicalRecord, Bill, Patient where MedicalRecord.patient_id = Patient.patient_id " +
                    "and Bill.record_id= MedicalRecord.record_id and bill_id={0}",id);
                dbConn.Open();
                cmd = new MySqlCommand(query2, dbConn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patient_id= Convert.ToInt32(reader["patient_id"]);
                    bill_id = Convert.ToInt32(reader["bill_id"]);
                    name_pa = reader["name"].ToString();
                    add = reader["address"].ToString();
                    dob = reader["birthday"].ToString();
                    gender = reader["gender"].ToString();
                    sym = reader["symptom"].ToString();
                    treat = reader["disease"].ToString();
                    Sum = (float)reader["total"];
                    treat_cost = (float)reader["treatment"];
                }
                reader.Close();
                dbConn.Close();
                using (Reader read = new Reader())
                {                   
                    read.PrintInvoice(patient_id, bill_id, name_pa, add, dob, sym, treat, gender, Sum, list_bill_item,treat_cost);
                    read.ShowDialog();
                }
            }

        }
    }
}