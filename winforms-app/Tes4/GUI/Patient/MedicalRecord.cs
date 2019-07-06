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
using Tes4._3_Tier.BUS;
using System.Globalization;

namespace Tes4.GUI.Patient
{
    public partial class MedicalRecord : DevExpress.XtraEditors.XtraForm
    {
        private static MySqlConnection dbConn;
        private MedicineBUS medBus;
        private PatientBUS patBus;
        private TreatmentBUS treatBus;
        private MedicalRecord_BUS medReBUS;
        private bill_BUS Bill_BUS;
        private billItem_BUS billItem_BUS;

        int patientID;
        int currentID_MedicalRecord;
        int currenntID_Bill;
        float medicine_total=0;
        public MedicalRecord()
        {
            InitializeComponent();
        }

        private void customInstaller1_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {}

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

        public void Insert_Value_Usage()
        { 
            Column4.Items.Add("Sáng sau khi ăn");
            Column4.Items.Add("Chiều sau khi ăn");
            Column4.Items.Add("Trưa sau khi ăn");
            Column4.Items.Add("Sáng trước khi ăn");
            Column4.Items.Add("Chiều trước khi ăn");
            Column4.Items.Add("Trưa trước khi ăn");
            Column4.Items.Add("Sáng chiều sau ăn");
            Column4.Items.Add("Sáng trưa sau ăn");
            Column4.Items.Add("Sáng chiều sau ăn");
        }
        private List<string> Medicine_Name_Data_Suggestion()
        {
            List<string> listDator = new List<string>();
            listDator = medBus.GetMedicines_Owner();
            return listDator;
        }

        private void get_patientnname()
        {
            List<string> lista = patBus.GetPatients_Name();
            nameCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            nameCmb.AutoCompleteCustomSource = data;
            nameCmb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            BindingSource bs = new BindingSource();
            bs.DataSource = lista;
            nameCmb.DataSource = bs;
        }
        private void get_disease()
        {
            List<string> lista = treatBus.GetNameDisease_Owner();
            BindingSource bs = new BindingSource();
            bs.DataSource = lista;
            disCmb.DataSource = bs;
        }
        private void get_symtom()
        {
            List<string> lista = treatBus.GetSymptom_Owner();
            symTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            symTxt.AutoCompleteCustomSource = data;
            symTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void MedicalRecord_Load(object sender, EventArgs e)
        {
            InitializeDB();
            Insert_Value_Usage();

            medBus = new MedicineBUS();
            patBus = new PatientBUS();
            treatBus = new TreatmentBUS();
            medReBUS = new MedicalRecord_BUS();
            Bill_BUS = new bill_BUS();
            billItem_BUS = new billItem_BUS();

            dateEdit1.DateTime = DateTime.Today;
            get_patientnname();
            get_disease();
            get_symtom();
            dateEdit1.Enabled = false;
            disCmb.SelectedIndex = -1;
            nameCmb.SelectedIndex = -1;
        }

        private void dtGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //Suggest medicine name 
            int columnIndex = dtGridView.CurrentCell.ColumnIndex;
            string text = dtGridView.Columns[columnIndex].HeaderText;
            if(text.Equals("Tên thuốc"))
            {
                TextBox auto_textBox = e.Control as TextBox;
                if(auto_textBox!=null)
                {
                    List<string> lista = Medicine_Name_Data_Suggestion();
                    auto_textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                    data.AddRange(lista.ToArray());
                    auto_textBox.AutoCompleteCustomSource = data;
                    auto_textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

            }
        }

        private void dtGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtGridView.CurrentRow;
            if (dtGridView.CurrentRow != null)
            {
                if (row.Cells["nameCol"].Value != null)
                {
                    row.Cells["unitTxt"].Value = medBus.get_Quantity(row.Cells["nameCol"].Value.ToString());
                    row.Cells["moneyTxt"].Value = (medBus.get_price((row.Cells["nameCol"].Value.ToString())));
                }
          
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow yourDataGridView = dtGridView.CurrentRow;
            if(yourDataGridView==null || dtGridView.Rows.Count==1)
            {
                MessageBox.Show("Please select rows for deleting", "Message", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa loại thuốc này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    dtGridView.Rows.RemoveAt(yourDataGridView.Index);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameCmb.Text))
            {
                ePro.SetError(nameCmb, "Vui lòng chọn bệnh nhân");
                return;
            }
            else
            {
                ePro.SetError(nameCmb, "");
                if (disCmb.SelectedIndex == -1)
                {
                    ePro.SetError(disCmb, "Vui lòng chọn bệnh");
                    return;
                }
                else
                {
                    ePro.SetError(disCmb, "");
                    if (string.IsNullOrWhiteSpace(symTxt.Text))
                    {
                        ePro.SetError(symTxt, "Vui lòng nhập triệu chứng");
                        return;
                    }
                    else
                    {
                        ePro.SetError(symTxt, "");
                    }
                }
            }
            // Insert into MedicalRecord DataBase
            currentID_MedicalRecord = medReBUS.Insert(patientID, disCmb.SelectedItem.ToString(), symTxt.Text, dateEdit1.Text);

            // Insert into bill DataBase
            currenntID_Bill = Bill_BUS.Insert(currentID_MedicalRecord);

            //Insert into bill_items database

            foreach (DataGridViewRow row in dtGridView.Rows)
            {
                //Get value from each row in dtGridView
                if (row.Cells[0].Value != null && dtGridView.RowCount != 0 && row.Cells[1].Value != null
                    && row.Cells[2].Value != null && row.Cells[3].Value!= null && row.Cells[4].Value!= null)
               {                 
                    string name = row.Cells[0].Value.ToString();
                    string unit = row.Cells[3].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells[1].Value);
                    string usage = row.Cells[2].Value.ToString();
                    float money = (float)row.Cells[4].Value;
                    billItem_BUS.Insert(currenntID_Bill, name, unit, quantity, usage, money);
                    medicine_total += money;
                }
                else
                {
                    if (row.Index==dtGridView.RowCount-1)
                    {
                        dtGridView.Rows.Clear();
                        nameCmb.Text = "";
                        disCmb.SelectedIndex = -1;
                        symTxt.Text = "";
                        return;
                    }
                    else
                    { 
                        MessageBox.Show("Vui lòng kiểm tra lại đơn thuốc bao gồm: THUỐC có trong kho, đơn vị sử dụng là SỐ, cách dùng", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Bill_BUS.Delete(currenntID_Bill);
                        medReBUS.Delete(currentID_MedicalRecord);
                        dtGridView.Rows.Clear();
                        nameCmb.Text = "";
                        disCmb.SelectedIndex = -1;
                        symTxt.Text = "";
                        return;
                    }
                }
         
            }

            //Update values of total in Bill dataBase
           // Bill_BUS.Update(currenntID_Bill, medicine_total);
            dtGridView.Rows.Clear();
            nameCmb.Text = "";
            disCmb.SelectedIndex = -1;
            symTxt.Text = "";

        }


        private void nameCmb_TextChanged(object sender, EventArgs e)
        {
            patientID = patBus.GetPatient_ID(nameCmb.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtGridView.Rows.Clear();
            nameCmb.Text = "";
            disCmb.SelectedIndex = -1;
            symTxt.Text = "";
        }
    }
}