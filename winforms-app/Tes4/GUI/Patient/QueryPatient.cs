using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
using Tes4._3_Tier.BUS;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Tes4
{
    public partial class QueryPatient : DevExpress.XtraEditors.XtraForm
    {
        private query_patient_medi_BUS ptnBus;
        public QueryPatient()
        {
            InitializeComponent();
            ptnBus = new query_patient_medi_BUS();
            LoadAll();
           
        }
        private void LoadAll()
        {
            List<query_medic_patientDTO> patients = ptnBus.LoadAll();

            listView.Items.Clear();

            foreach (query_medic_patientDTO pa in patients)
            {

                ListViewItem item = new ListViewItem(new String[] {pa.patient_name, pa.date, pa.Disease, pa.symptom});
                item.Tag = pa;
                listView.Items.Add(item);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

  

        private void idQuery_TextChanged(object sender, EventArgs e)
        {
            /* Xử lí 3 TH : Nhập sai quy định, nhập xong xóa, nhập tìm lần sau */
            if (!string.IsNullOrEmpty(idQuery.Text) && System.Text.RegularExpressions.Regex.IsMatch(idQuery.Text, "^[0-9]*$")) //Check when we delete our word b/c our function is checked our typing one by one
            {
                erroP2.SetError(idQuery, "");
            }
            else
            {
                if (string.IsNullOrEmpty(idQuery.Text))
                    erroP2.SetError(idQuery, "");
                else
                {
                    erroP2.SetError(idQuery, "Nhập ID là số");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string id = idQuery.Text;
            string ten = nameQuery.Text;
            string pNb = numQuery.Text;

            listView.Items.Clear();
          
            if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(ten) && string.IsNullOrWhiteSpace(pNb))
            {
                MessageBox.Show("Các ô nhập trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
              
                string dieuKien = "MedicalRecord.patient_id like '%" + id + "%' and";
                dieuKien += " name like '%" + ten + "%' and";
                dieuKien += " phone like '%" + pNb + "%'";
                List<query_medic_patientDTO> patients = ptnBus.Query(dieuKien);

                

                foreach (query_medic_patientDTO pa in patients)
                {

                    ListViewItem item = new ListViewItem(new String[] { pa.patient_name, pa.date, pa.Disease, pa.symptom });
                    item.Tag = pa;
                    listView.Items.Add(item);
                }
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (listView.Items.Count == 0)
                {
                    MessageBox.Show("Không có kết quả", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idQuery.Text = null;
                    nameQuery.Text = null;
                    numQuery.Text = null;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            idQuery.Text = null;
            nameQuery.Text = null;
            numQuery.Text = null;
        }
    }
}