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
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
using Tes4._3_Tier.BUS;
using System.Globalization;

namespace Tes4.GUI.Management
{
    public partial class PillManagement : DevExpress.XtraEditors.XtraForm
    {
        private MedicineDTO currentMedi;
        private MedicineBUS medBus;
        int function_check; // 1- insert ; 2 -update
        int flag; //did we selecte our row
        int id_checked;

        public PillManagement()
        {
            InitializeComponent();
        }
        private void PillManagement_Load(object sender, EventArgs e)
        {
            //LOCK OUR TEXT BOXES
            Block_or_Unlock_Textbox(false);
            Hide_Show_Buttons(false);
            currentMedi = new MedicineDTO();
            medBus = new MedicineBUS();
            currentMedi = null;
            LoadAll();
            name_suggestion();
            quantity_suggestion();
        }

        private void Block_or_Unlock_Textbox(bool value)
        {
            nameTxt.Enabled = value;
            quantTxt.Enabled = value;
            priceTxt.Enabled = value;
        }

        //Hide or Show buttons which inclues: Cancel and Save
        private void Hide_Show_Buttons(bool value)
        {
            btnCancel.Enabled = value;
            btnSave.Enabled = value;
            listView.Enabled = !value;
        }

        //Enable Or Unable functions: Add, Update, Delete
        private void Enable_Uneable_Function(bool value)
        {
            btnDelete.Enabled = value;
            btnAdd.Enabled = value;
            btnUpdate.Enabled = value;
        }

        //Viualize our Data from listView to text boxes
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem item = listView.SelectedItems[0];
                currentMedi = (MedicineDTO)item.Tag;

                int id = id_checked = currentMedi.medicine_id;
                String n = currentMedi.name;
                String quan = currentMedi.quantity;

                String price = currentMedi.price.ToString();
                nameTxt.Text = n;
                quantTxt.Text = quan;

                priceTxt.Text = price;
                flag = 1;
            }
        }

        //Fill our data into list View
        private void LoadAll()
        {
            List<MedicineDTO> medicines = medBus.GetMedicines();
            
            listView.Items.Clear();

            foreach (MedicineDTO pa in medicines)
            {

                ListViewItem item = new ListViewItem(new String[] { pa.medicine_id.ToString(), pa.name, pa.quantity, pa.price.ToString()});
                item.Tag = pa;
                listView.Items.Add(item);
            }
        }

        //Add , Update, Delete,Save,Cacncel Clicked
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //BLOCK UNUSING BUTTON 
            if (flag == 1)
            {
                nameTxt.Text = "";
                quantTxt.Text = "";
                priceTxt.Text = "";
               
                flag = 0;
            }
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);

            //Choose functionn
            function_check = 1;

            //
            Hide_Show_Buttons(true);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count == 0)
            {
                MessageBox.Show("Rỗng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (currentMedi == null)
            {
                MessageBox.Show("Vui lòng chọn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Rỗng!");
                return;
            }
            if (currentMedi == null)
            {
                MessageBox.Show("Vui lòng chọn thuốc!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa loại thuốc này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                
                medBus.Delete(id_checked);
            }
            nameTxt.Text = "";
            quantTxt.Text = "";
            priceTxt.Text = "";
          
            LoadAll();
        }       
        private void btnSave_Click(object sender, EventArgs e)
        {
            String n =nameTxt.Text;
            String quan = quantTxt.Text;
         
            String price = priceTxt.Text.ToString();
            int t = 0; //check our input

            if (string.IsNullOrWhiteSpace(nameTxt.Text))
            {
                ePro.SetError(nameTxt, "Vui lòng nhập tên thuốc");
                return;
            }

            else
            {
                ePro.SetError(nameTxt, "");
                if (string.IsNullOrWhiteSpace(quantTxt.Text))
                {
                    ePro.SetError(quantTxt, "Vui lòng chọn đơn vị của thuốc");
                    return;
                }
                else

                {
                    ePro.SetError(quantTxt, "");
                        if (string.IsNullOrWhiteSpace(priceTxt.Text))
                        {

                            ePro.SetError(priceTxt, "Vui lòng nhập giá tiền");
                            return;
                        }
                        else
                            ePro.SetError(priceTxt, "");

                    
                }
            }

            switch (function_check)
            {
                case 1:
                    //Check privacy
                    foreach (ListViewItem item in listView.Items)
                    {
                        if (item.SubItems[1].Text == n)
                        {
                            MessageBox.Show("Tên bệnh đã có trong hệ thống hãy kiểm tra lại?", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            nameTxt.Text = "";
                            quantTxt.Text = "";
                            priceTxt.Text = "";
                            return;
                        }
                    }
                    currentMedi = medBus.Insert(n,quan, float.Parse(price, CultureInfo.InvariantCulture.NumberFormat));

                    break;
                case 2:
                    //Check privacy


                    medBus.Update(id_checked, n, quan, float.Parse(price, CultureInfo.InvariantCulture.NumberFormat));

                    break;
            }

            currentMedi = null;
            nameTxt.Text = "";
            quantTxt.Text = "";
            priceTxt.Text = "";

            //
            Enable_Uneable_Function(true);
            Hide_Show_Buttons(false);
            Block_or_Unlock_Textbox(false);

            //
            LoadAll();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

            Enable_Uneable_Function(true);
            Hide_Show_Buttons(false);
            currentMedi = null;
            nameTxt.Text = "";
            quantTxt.Text = "";
            priceTxt.Text = "";
            

            //Deal with Error Provider
            ePro.SetError(nameTxt, "");
            ePro.SetError(quantTxt, "");
            ePro.SetError(priceTxt, "");
           


            Block_or_Unlock_Textbox(false);

        }

        //Name Suggestion
        private List<string> NameData()
        {
            List<MedicineDTO> medicines = medBus.GetMedicines();

            List<string> listDator = new List<string>();
            listDator = medBus.GetMedicines_Suggestion();
            return listDator;
        }
        private void name_suggestion()
        {
            List<string> lista = NameData();
            nameTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            nameTxt.AutoCompleteCustomSource = data;
            nameTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        //Quantity Suggestion
        private List<string> QuantityData()
        {
            List<string> listDator = new List<string>();

            listDator.Add("viên");
            listDator.Add("Gói");
            listDator.Add("Chai");
            listDator.Add("Kem bôi");

            return listDator;
        }
        private void quantity_suggestion()
        {
            List<string> lista = QuantityData();
            quantTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            quantTxt.AutoCompleteCustomSource = data;
            quantTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }
       
    }
   

}