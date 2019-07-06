using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; //library
namespace WindowsFormsApp1
{
    public partial class Patient_Data : Form
    {
        private patient currentPatient;
        string gender;
        int function_check; // 1- insert ; 2 -update
        int flag; //did we selecte our row
        public Patient_Data()
        {
            InitializeComponent();

        }
        private void Block_or_Unlock_Textbox(bool value)
        {
            txtName.Enabled = value;
            AddrsTxt.Enabled = value;
            dobTxt.Enabled = value;
            GenderTxt.Enabled = value;
            phoneTxt.Enabled = value;

        }
        //Hide or Show buttons which inclues: Cancel and Save
        private void Hide_Buttons()
        {
            btnCancel.Hide();
            btnSave.Hide();
        }
        private void Show_Button()
        {
            btnCancel.Show();
            btnSave.Show();
        }

        // Enable or uneable our functios via input's value
        private void Enable_Uneable_Function(bool value)
        {
            btnLoad.Enabled = value;
            btnDelete.Enabled = value;
            btnAdd.Enabled = value;
            btnUpdate.Enabled = value;
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            patient.InitializeDB();
            //LOCK OUR TEXT BOXES
            Block_or_Unlock_Textbox(false);
            Hide_Buttons();
        }
        //Block_Or_Unlock_Text throughout input of value 
       
        private void LoadAll()
        {
            List<patient> patients = patient.GetPatients();

            listView.Items.Clear();
            
            foreach (patient pa in patients)
            {

                ListViewItem item = new ListViewItem(new String[] { pa.patient_id.ToString(), pa.name, pa.birthday,pa.gender,pa.addres,pa.phone });
                item.Tag = pa;
                listView.Items.Add(item);
              

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //BLOCK UNUSING BUTTON 
            if(flag==1)
            {
                txtName.Text = "";
                dobTxt.Text = "";
                AddrsTxt.Text = "";
                phoneTxt.Text = "0";
                GenderTxt.SelectedIndex = -1;
                flag = 0;
            }
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);
            
            //Choose functionn
            function_check = 1;

            //
            Show_Button();
      
        }


        private void GenderTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GenderTxt.SelectedIndex != -1)
            gender = GenderTxt.SelectedItem.ToString();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadAll();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);


            //
            function_check = 2;

            Show_Button();
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (listView.Items.Count == 0)
            {
                MessageBox.Show("Empty!");
                return;
            }
            if (currentPatient == null)
            {
                MessageBox.Show("No user selected!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa bệnh nhân này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { currentPatient.Delete(); }
            txtName.Text = "";
            dobTxt.Text = "";
            AddrsTxt.Text = "";
            phoneTxt.Text = "0";
            GenderTxt.SelectedIndex = -1;
            LoadAll();
        }
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem item = listView.SelectedItems[0];
                currentPatient = (patient)item.Tag;

                int id = currentPatient.patient_id;
                String n = currentPatient.name;
                String dob = currentPatient.birthday;
                String gender= currentPatient.gender;
                String address = currentPatient.addres;
                String Phone = currentPatient.phone;
                txtName.Text = n;
                if (gender == "Male")
                    GenderTxt.SelectedIndex = 0;
                else
                    GenderTxt.SelectedIndex = 1;
                dobTxt.Value = DateTime.ParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture); ;
                AddrsTxt.Text = address;
                phoneTxt.Text = Phone;
                flag = 1;
            }

        }

       

        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            /* Xử lí 3 TH : Nhập sai quy định, nhập xong xóa, nhập tìm lần sau */
            if ( !string.IsNullOrEmpty(searchTxt.Text) && System.Text.RegularExpressions.Regex.IsMatch(searchTxt.Text, "^[0-9]*$")) //Check when we delete our word b/c our function is checked our typing one by one
            {
                ePro.SetError(searchTxt, "");
                List<patient> patients = patient.FastQuery(Convert.ToInt32(searchTxt.Text));

                listView.Items.Clear();

                foreach (patient pa in patients)
                {

                    ListViewItem item = new ListViewItem(new String[] { pa.patient_id.ToString(), pa.name, pa.birthday, pa.gender, pa.addres });
                    item.Tag = pa;
                    listView.Items.Add(item);

                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchTxt.Text))
                    ePro.SetError(searchTxt, "");
                else
                {
                    ePro.SetError(searchTxt, "Nhập ID là số");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String n = txtName.Text;
            String dob = dobTxt.Text;
            String addres = AddrsTxt.Text;
            String phoneNum = phoneTxt.Text;
            int t = 0; //check our input

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ePro.SetError(txtName, "Vui lòng nhập tên");
                return;
            }

            else
            {
                ePro.SetError(txtName, "");
                if (GenderTxt.SelectedIndex == -1)
                {
                    ePro.SetError(GenderTxt, "Vui lòng chon giới tính");
                    return;
                }
                else
                    
                {
                    ePro.SetError(GenderTxt, "");
                   
                    if (!((phoneTxt.Text).Length == 10 && phoneTxt.Text[0] == '0'))
                    {
                        ePro.SetError(phoneTxt, "Kiểm tra thông tin có '0' ở đầu và đủ 10 chữ số");
                        return;
                    }

                    else
                    {
                        ePro.SetError(phoneTxt, "");
                        if (string.IsNullOrWhiteSpace(AddrsTxt.Text))
                        {
                           
                            ePro.SetError(AddrsTxt, "Vui lòng nhập địa chỉ");
                            return;
                        }
                        else
                            ePro.SetError(AddrsTxt, "");

                    }
                }
            }
                           
                        
                    
                
            
            switch (function_check)
            {
                case 1:
                    //Check privacy

                    currentPatient = patient.Insert(n, dob, gender, addres,phoneNum);
              
                        break;
                case 2:
                    //Check privacy
                    currentPatient.Update(n, dob, gender, addres,phoneNum);
                 
                    break;
            }

            currentPatient = null;
            txtName.Text = "";
            dobTxt.Text = "";
            AddrsTxt.Text = "";
            phoneTxt.Text = "0";
            GenderTxt.SelectedIndex = -1;
           
            //
            Enable_Uneable_Function(true);
            Hide_Buttons();
            Block_or_Unlock_Textbox(false);

            //Set Error Provider to False
           
            
           
            //
            LoadAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Enable_Uneable_Function(true);
            Hide_Buttons();
            currentPatient = null;
            txtName.Text = "";
            dobTxt.Text = "";
            AddrsTxt.Text = "";
            phoneTxt.Text = "0";
            GenderTxt.SelectedIndex = -1;

            //Deal with Error Provider
            ePro.SetError(txtName, "");
            ePro.SetError(GenderTxt, "");
            ePro.SetError(AddrsTxt, "");
            ePro.SetError(phoneTxt, "");


            Block_or_Unlock_Textbox(false);
        }

        //Erro deteching
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^[a-zA-Z ]*$";
            Match result = Regex.Match(txtName.Text, pattern);
            if (!result.Success)
            {
                ePro.SetError(txtName, "Tên phải toàn chữ");
            }
            else
                ePro.SetError(txtName, "");
        }
    }
}
