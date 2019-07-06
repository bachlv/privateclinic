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
    public partial class PatienInfo : DevExpress.XtraEditors.XtraForm
    {
        private PatientDTO currentPatient;
        private PatientBUS ptnBus;
        private GeoInfo_BUS geoBUS;
        string gender;
        int function_check; // 1- insert ; 2 -update
        int flag; //did we selecte our row
        int id_checked;
        public PatienInfo()
        {
            InitializeComponent();
        }

        
        private void Block_or_Unlock_Textbox(bool value)
        {
            txtName.Enabled = value;
            provinceTxt.Enabled = value;
            districtTxt.Enabled = value;
            townTxt.Enabled = value;
            dobTxt.Enabled = value;
            genderTx.Enabled = value;
            phoneTxt.Enabled = value;

        }
        //Hide or Show buttons which inclues: Cancel and Save
        private void Hide_Show_Buttons(bool value)
        {
            btnCancel.Enabled = value;
            btnSave.Enabled = value;
            listView.Enabled = !value;
        }
        // Enable or uneable our functios via input's value
        private void Enable_Uneable_Function(bool value)
        {
            btnLoad.Enabled = value;
            btnDelete.Enabled = value;
            btnAdd.Enabled = value;
            btnUpdate.Enabled = value;
        }

        private void PatienInfo_Load(object sender, EventArgs e)
        {
            //LOCK OUR TEXT BOXES
            Block_or_Unlock_Textbox(false);
            Hide_Show_Buttons(false);
            currentPatient = new PatientDTO();
            ptnBus = new PatientBUS();
            geoBUS = new GeoInfo_BUS();
            currentPatient = null;
            Province_Suggest();
            provinceTxt.SelectedIndex = -1;
            townTxt.Text="";
            districtTxt.Text ="";
    }
        private void LoadAll()
        {
            List<PatientDTO> patients = ptnBus.GetPatients();

            listView.Items.Clear();

            foreach (PatientDTO pa in patients)
            {

                ListViewItem item = new ListViewItem(new String[] { pa.patient_id.ToString(), pa.name, pa.birthday, pa.gender, pa.addres, pa.phone });
                item.Tag = pa;
                listView.Items.Add(item);

            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
      
       

        //button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //BLOCK UNUSING BUTTON 
            if (flag == 1)
            {
                txtName.Text = "";
                dobTxt.Text = "";
                provinceTxt.Text = "";
                districtTxt.Text = "";
                townTxt.Text = "";
                phoneTxt.Text = "0";
                genderTx.SelectedIndex = -1;
                flag = 0;
            }
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);

            //Choose functionn
            function_check = 1;

            //
            Hide_Show_Buttons(true);

        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadAll();

        }
        private void btnUpdate_Click(object sender, EventArgs e)
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
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);


            //
            function_check = 2;

            Hide_Show_Buttons(true);


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
            { ;
                ptnBus.Delete(id_checked);
            }
            txtName.Text = "";
            dobTxt.Text = "";
            provinceTxt.Text = "";
            districtTxt.Text = "";
            townTxt.Text = "";
            phoneTxt.Text = "0";
            genderTx.SelectedIndex = -1;
            LoadAll();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            String n = txtName.Text;
            String dob = dobTxt.Text;
            String addres = provinceTxt.Text +","+ districtTxt.Text+"," + townTxt.Text;
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
                if (genderTx.SelectedIndex == -1)
                {
                    ePro.SetError(genderTx, "Vui lòng chon giới tính");
                    return;
                }
                else

                {
                    ePro.SetError(genderTx, "");

                    if (!((phoneTxt.Text).Length == 10 && phoneTxt.Text[0] == '0'))
                    {
                        ePro.SetError(phoneTxt, "Kiểm tra thông tin có '0' ở đầu và đủ 10 chữ số");
                        return;
                    }

                    else
                    {
                        ePro.SetError(phoneTxt, "");
                        if (string.IsNullOrWhiteSpace(provinceTxt.Text))
                        {

                            ePro.SetError(provinceTxt, "Vui lòng nhập địa chỉ");
                            return;
                        }
                        else
                            ePro.SetError(provinceTxt, "");

                    }
                }
            }
            switch (function_check)
            {
                case 1:
                    //Check privacy

                    currentPatient = ptnBus.Insert(n, dob, gender, addres, phoneNum);
                    ePro.SetError(txtName,"");
                    break;
                case 2:
                    //Check privacy


                    ptnBus.Update(id_checked, n, dob, gender, addres, phoneNum);

                    break;
            }

            currentPatient = null;
            txtName.Text = "";
            dobTxt.Text = "";
            provinceTxt.Text = "";
            townTxt.Text = "";
            districtTxt.Text = "";
            phoneTxt.Text = "0";
            genderTx.SelectedIndex = -1;
            
            //
            Enable_Uneable_Function(true);
            Hide_Show_Buttons(false);
            Block_or_Unlock_Textbox(false);

            //Set Error Provider to False

            

            //
            LoadAll();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

            Enable_Uneable_Function(true);
            Hide_Show_Buttons(false);
            currentPatient = null;
            txtName.Text = "";
            dobTxt.Text = "";
            provinceTxt.Text = "";
            townTxt.Text = "";
            districtTxt.Text = "";
            phoneTxt.Text = "0";
            genderTx.SelectedIndex = -1;

            //Deal with Error Provider
            ePro.SetError(txtName, "");
            ePro.SetError(genderTx, "");
            ePro.SetError(provinceTxt, "");
            ePro.SetError(phoneTxt, "");


            Block_or_Unlock_Textbox(false);

        }

        //event
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem item = listView.SelectedItems[0];
                currentPatient = (PatientDTO)item.Tag;

                int id = id_checked =currentPatient.patient_id;
                String n = currentPatient.name;
                String dob = currentPatient.birthday;
                String gender = currentPatient.gender;
                String address = currentPatient.addres;
                String Phone = currentPatient.phone;
                txtName.Text = n;
                if (gender == "Nam")
                    genderTx.SelectedIndex = 0;
                else
                    genderTx.SelectedIndex = 1;
                if (!string.IsNullOrEmpty(dob))
                {
                    dobTxt.DateTime = DateTime.Parse(dob);
                }
                string[] Addess_token = address.Split(',');
                provinceTxt.Text = Addess_token[0];
                districtTxt.Text = Addess_token[1];
                townTxt.Text = Addess_token[2];
                phoneTxt.Text = Phone;
                flag = 1;
            }
        }
        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            /* Xử lí 3 TH : Nhập sai quy định, nhập xong xóa, nhập tìm lần sau */
            if (!string.IsNullOrEmpty(searchTxt.Text) && System.Text.RegularExpressions.Regex.IsMatch(searchTxt.Text, "^[0-9]*$")) //Check when we delete our word b/c our function is checked our typing one by one
            {
                ePro.SetError(searchTxt, "");
                List<PatientDTO> patients = ptnBus.FastQuery(Convert.ToInt32(searchTxt.Text));

                listView.Items.Clear();

                foreach (PatientDTO pa in patients)
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
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string pattern = "^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶ" +
            "ẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợ" +
            "ụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s]+$";
            Match result = Regex.Match(txtName.Text, pattern);
            if (!result.Success && !string.IsNullOrWhiteSpace(txtName.Text))
            {
                ePro.SetError(txtName, "Tên phải toàn chữ");
            }
            else
                ePro.SetError(txtName, "");
        }
        private void phoneTxt_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneTxt.Text, "^[0-9]*$"))
            {
                ePro.SetError(phoneTxt, "SDT phải toàn số");
            }
            else
                ePro.SetError(phoneTxt, "");
        }
        private void GenderTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (genderTx.SelectedIndex != -1)
                gender = genderTx.SelectedItem.ToString();
        }
        private void hghghghToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

        //Province, District, Town autoComplete
        private void Province_Suggest()
        {
            List<string> lista = geoBUS.getProvince();
            provinceTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            provinceTxt.AutoCompleteCustomSource = data;
            provinceTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            BindingSource bs = new BindingSource();
            bs.DataSource = lista;
            provinceTxt.DataSource = bs;
        }
 

        private void provinceTxt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(provinceTxt.Text))
            {
                List<string> lista = geoBUS.getDistrict(provinceTxt.Text);

                districtTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                data.AddRange(lista.ToArray());
                districtTxt.AutoCompleteCustomSource = data;
                districtTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                BindingSource bs = new BindingSource();
                bs.DataSource = lista;
                districtTxt.DataSource = bs;
            }
        }

        private void districtTxt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(districtTxt.Text))
            {
                List<string> lista = geoBUS.getTown(districtTxt.Text);
                townTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                data.AddRange(lista.ToArray());
                townTxt.AutoCompleteCustomSource = data;
                townTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                BindingSource bs = new BindingSource();
                bs.DataSource = lista;
                townTxt.DataSource = bs;
            }
        }
    }
}