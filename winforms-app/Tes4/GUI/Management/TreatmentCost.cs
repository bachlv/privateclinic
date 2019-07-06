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

namespace Tes4.GUI.Management
{
    public partial class TreatmentCost : DevExpress.XtraEditors.XtraForm
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
        public TreatmentCost()
        {
            InitializeComponent();
            InitializeDB();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(treatcostTxt.Text))
            {
                errorProvider1.SetError(treatcostTxt, "Ô nhập trống");
                    return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(treatcostTxt.Text, "^[0-9]*$"))
            {
                errorProvider1.SetError(treatcostTxt, "Phải đảm bảo là CÁC CHỮ SỐ");
            }
            else
            {
                errorProvider1.SetError(treatcostTxt, "");
                String query = string.Format("ALTER TABLE  Bill ALTER treatment SET DEFAULT '{0}'",
                treatcostTxt.Text);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                dbConn.Open();
                cmd.ExecuteNonQuery();
                dbConn.Close();
                MessageBox.Show("Thônng báo", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treatcostTxt_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(treatcostTxt.Text, "^[0-9]*$"))
            {
                errorProvider1.SetError(treatcostTxt, "Phải đảm bảo là CÁC CHỮ SỐ");
            }
            else
                errorProvider1.SetError(treatcostTxt, "");


        }
    }
}