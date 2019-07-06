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
namespace Tes4.GUI.Management
{
    public partial class Diseases : DevExpress.XtraEditors.XtraForm
    {
        private TreatmentDTO currentTreat;
        private TreatmentBUS treatBus;
        int function_check; // 1- insert ; 2 -update
        int flag; //did we selecte our row
        int id_checked;

        public Diseases()
        {
            InitializeComponent();
        }
        private void Diseases_load(object sender, EventArgs e)
        {
            //LOCK OUR TEXT BOXES
            Block_or_Unlock_Textbox(false);
            Hide_Show_Buttons(false);
            currentTreat = new TreatmentDTO();
            treatBus = new TreatmentBUS();
            currentTreat = null;
            LoadAll();       
            name_suggestion();
            quantity_suggestion();
        }
        private void Block_or_Unlock_Textbox(bool value)
        {
            nameTxt.Enabled = value;
            symTxt.Enabled = value;
        }
        private void Hide_Show_Buttons(bool value)
        {
            btnCancel.Enabled = value;
            btnSave.Enabled = value;
            listView.Enabled = !value;
        }
        private void Enable_Uneable_Function(bool value)
        {
            btnDelete.Enabled = value;
            btnAdd.Enabled = value;
            btnUpdate.Enabled = value;
        }

        private void LoadAll()
        {
            List<TreatmentDTO> treatments = treatBus.GetTreatments();

            listView.Items.Clear();

            foreach (TreatmentDTO pa in treatments)
            {

                ListViewItem item = new ListViewItem(new String[] { pa.treat_id.ToString(), pa.name, pa.symptom});
                item.Tag = pa;
                listView.Items.Add(item);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem item = listView.SelectedItems[0];
                currentTreat = (TreatmentDTO)item.Tag;

                int id = id_checked = currentTreat.treat_id;
                String n = currentTreat.name;
                String syn = currentTreat.symptom;
                nameTxt.Text = n;
                symTxt.Text = syn;
                flag = 1;
            }
        }
  
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //BLOCK UNUSING BUTTON 
            if (flag == 1)
            {
                nameTxt.Text = "";
                symTxt.Text = "";
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
                MessageBox.Show("Rỗng!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (currentTreat == null)
            {
                MessageBox.Show("Vui lòng chọn bệnh!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                MessageBox.Show("Rỗng!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (currentTreat == null)
            {
                MessageBox.Show("Bệnh chưa được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa bệnh này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                treatBus.Delete(id_checked);
            }
            nameTxt.Text = "";
            symptom.Text = "";
            LoadAll();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            String n = nameTxt.Text;
            String syn = symTxt.Text;
            int t = 0; //check our input

            if (string.IsNullOrWhiteSpace(nameTxt.Text))
            {
                ePro.SetError(nameTxt, "Vui lòng nhập tên bệnh");
                return;
            }

            else
            {
                ePro.SetError(nameTxt, "");
                if (string.IsNullOrWhiteSpace(symTxt.Text))
                {
                    ePro.SetError(symTxt, "Vui lòng nhập biểu hiện bệnh");
                    return;
                }
                else

                {
                    ePro.SetError(symTxt, "");
                }
            }
            switch (function_check)
            {
                case 1:
                    //Check privacy
                    foreach (ListViewItem item in listView.Items)
                    {
                        if(item.SubItems[1].Text == n)
                        {
                            MessageBox.Show("Tên bệnh đã có trong hệ thống hãy kiểm tra lại?", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            nameTxt.Text = "";
                            symTxt.Text = "";
                            return;
                        }
                    }
                    currentTreat = treatBus.Insert(n, syn);
                    break;
                case 2:
                    //Check privacy
                    treatBus.Update(id_checked, n, syn);
                    break;
            }

            currentTreat = null;
            nameTxt.Text = "";
            symTxt.Text = "";
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
            Block_or_Unlock_Textbox(false);

            currentTreat = null;
            nameTxt.Text = "";
            symTxt.Text = "";

            //Deal with Error Provider
            ePro.SetError(nameTxt, "");
            ePro.SetError(symTxt, "");          
        }
       
           
        private List<string> NameDiseaseData()
        {

            List<string> listDator = new List<string>();
            listDator = treatBus.GetNameDisease_Suggestion();
            return listDator;
        }
        private void name_suggestion()
        {
            List<string> lista = NameDiseaseData();
            nameTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            nameTxt.AutoCompleteCustomSource = data;
            nameTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        //Symptom Suggestions
        
        private List<string> SymtomData()
        {
             List<string> listDator = new List<string>();
            listDator = treatBus.GetSymptom_Suggestion();
            return listDator;
        }
        private void quantity_suggestion()
        {
            List<string> lista = SymtomData();
            symTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            data.AddRange(lista.ToArray());
            symTxt.AutoCompleteCustomSource = data;
            symTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(nameTxt.Text))
            symTxt.Text = treatBus.Get_Symtom(nameTxt.Text);
        }
    }
}