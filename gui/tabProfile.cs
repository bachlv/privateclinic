using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace PriClinic
{
    public partial class tabProfile : UserControl
    {
        private patient currentPatient;
        string gender;
        int flag = 0; /*0-that's mean we don't query our data , and 1 is yes*/
        public tabProfile()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            patient.InitializeDB();

        }
        private void LoadAll()
        {
            List<patient> patients = patient.GetPatients();

            listView.Items.Clear();

            foreach (patient pa in patients)
            {

                ListViewItem item = new ListViewItem(new String[] { pa.idPatient.ToString(), pa.Name, pa.DOB, pa.Gender, pa.Address });
                item.Tag = pa;
                listView.Items.Add(item);
                comboBox1.Items.Add(pa.Name);

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String n = txtName.Text;
            String dob = dobTxt.Text;
            String addres = AddrsTxt.Text;

            if (flag == 1)
            {
                txtName.Text = "";
                dobTxt.Text = "";
                AddrsTxt.Text = "";
                GenderTxt.SelectedIndex = -1;
                flag = 0;
            }
            else
            {
                if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(dobTxt.Text) || String.IsNullOrEmpty(AddrsTxt.Text))
                {
                    MessageBox.Show("It was empty here!");
                    return;
                }
                currentPatient = patient.Insert(n, dob, gender, addres);
                currentPatient = null;
                txtName.Text = "";
                dobTxt.Text = "";
                AddrsTxt.Text = "";
                GenderTxt.SelectedIndex = -1;
                LoadAll();
            }
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
            String n = txtName.Text;
            String dob = dobTxt.Text;
            String addres = AddrsTxt.Text;


            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(dobTxt.Text) || String.IsNullOrEmpty(AddrsTxt.Text))
            {
                MessageBox.Show("It's empty");
                return;
            }

            currentPatient.Update(n, dob, gender, addres);
            currentPatient = null;
            LoadAll();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem item = listView.SelectedItems[0];
                currentPatient = (patient)item.Tag;

                int id = currentPatient.idPatient;
                String n = currentPatient.Name;
                String dob = currentPatient.DOB;
                String gender = currentPatient.Gender;
                String address = currentPatient.Address;

                txtName.Text = n;
                if (gender == "Male")
                    GenderTxt.SelectedIndex = 0;
                else
                    GenderTxt.SelectedIndex = 1;
                dobTxt.Value = DateTime.ParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                AddrsTxt.Text = address;
                flag = 1; //we did query our data
            }
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



            currentPatient.Delete();
            txtName.Text = "";
            dobTxt.Text = "";
            AddrsTxt.Text = "";
            GenderTxt.SelectedIndex = -1;
            LoadAll();
        }

        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTxt.Text)) //Check when we delete our word b/c our function is checked our typing one by one
            {
                List<patient> patients = patient.Query(Convert.ToInt32(searchTxt.Text));

                listView.Items.Clear();

                foreach (patient pa in patients)
                {

                    ListViewItem item = new ListViewItem(new String[] { pa.idPatient.ToString(), pa.Name, pa.DOB, pa.Gender, pa.Address });
                    item.Tag = pa;
                    listView.Items.Add(item);

                }
            }
        }

    }
}
