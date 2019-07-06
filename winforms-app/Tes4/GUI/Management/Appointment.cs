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
    public partial class Appointment : DevExpress.XtraEditors.XtraForm
    {
        private AppointmentDTO currentAppt;
        private AppointmentBUS apptBus;
        int function_check; // 1- insert ; 2 -update
        int flag; //did we selecte our row
        int id_checked;
        public Appointment()
        {
            InitializeComponent();
        }

        private void Appointment_Load(object sender, EventArgs e)
        {
            //LOCK OUR TEXT BOXES
            Block_or_Unlock_Textbox(false);
            Hide_Show_Buttons(false);
            currentAppt = new AppointmentDTO();
            apptBus = new AppointmentBUS();
            currentAppt = null;
            LoadAll();
        }

        private String getGender(int gend) { return gend == 0 ? "Nữ " : "Nam"; }

        private String getSlotTime(int st)
        {
            switch (st)
            {
                case 0: return "9:00 - 10:00";
                case 1: return "10:00 - 11:00";
                case 2: return "11:00 - 12:00";
                case 3: return "12:00 - 13:00";
                case 4: return "13:00 - 14:00";
                case 5: return "14:00 - 15:00";
                case 6: return "15:00 - 16:00";
                case 7: return "16:00 - 17:00";
                default:  return "Chưa đặt giờ";
            }
        }

        private void Block_or_Unlock_Textbox(bool value)
        {
            nameTxt.Enabled = value;
            dobTxt.Enabled = value;
            genderSl.Enabled = value;
            phoneTxt.Enabled = value;
            emailTxt.Enabled = value;
            addrTxt.Enabled = value;
            sltimecb.Enabled = value;
            apptDate.Enabled = value;

        }

        private void Hide_Show_Buttons(bool value)
        {
            btnCan.Enabled = value;
            btnSav.Enabled = value;
            apptList.Enabled = !value;
        }

        private void Enable_Uneable_Function(bool value)
        {
            btnDel.Enabled = value;
            btnAdd.Enabled = value;
            btnSav.Enabled = value;
            btnMod.Enabled = value;
        }

        private void apptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (apptList.SelectedItems.Count > 0)
            {
                ListViewItem item = apptList.SelectedItems[0];
                currentAppt = (AppointmentDTO)item.Tag;

                int id = id_checked = currentAppt.appointment_id;
                String n = currentAppt.name;
                int gend = currentAppt.gender;
                String bd = currentAppt.birthday;
                String addr = currentAppt.address;
                String ph = currentAppt.phone;
                String eml = currentAppt.email;
                int sltime = currentAppt.slottime;
                nameTxt.Text = n;
                genderSl.Text = (gend == 0 ? "Nữ": "Nam");
                dobTxt.Text = bd;
                phoneTxt.Text = ph;
                addrTxt.Text = addr;
                emailTxt.Text = eml;
                switch (sltime)
                {
                    case 0:
                        sltimecb.Text = "9:00 - 10:00";
                        break;
                    case 1:
                        sltimecb.Text = "10:00 - 11:00";
                        break;
                    case 2:
                        sltimecb.Text = "11:00 - 12:00";
                        break;
                    case 3:
                        sltimecb.Text = "12:00 - 13:00";
                        break;
                    case 4:
                        sltimecb.Text = "13:00 - 14:00";
                        break;
                    case 5:
                        sltimecb.Text = "14:00 - 15:00";
                        break;
                    case 6:
                        sltimecb.Text = "15:00 - 16:00";
                        break;
                    case 7:
                        sltimecb.Text = "16:00 - 17:00";
                        break;
                    default:
                        break;
                }
                flag = 1;
            }
        }

        private void LoadAll()
        {
            List<AppointmentDTO> appointmentlist = apptBus.GetAppointments();

            apptList.Items.Clear();

            foreach (AppointmentDTO appt in appointmentlist)
            {;
                ListViewItem item = new ListViewItem(new String[] { appt.appointment_id.ToString(), appt.name, getGender(appt.gender), appt.birthday, appt.address, appt.phone, appt.email, appt.slotdate, getSlotTime(appt.slottime)});
                item.Tag = appt;
                apptList.Items.Add(item);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                nameTxt.Text = "";
                dobTxt.Text = "";
                phoneTxt.Text = "";
                addrTxt.Text = "";
                emailTxt.Text = "";
                genderSl.SelectedIndex = -1;
                sltimecb.SelectedIndex = -1;

                flag = 0;
            }
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);

            //Choose functionn
            function_check = 1;

            //
            Hide_Show_Buttons(true);
        }

        private void BtnMod_Click(object sender, EventArgs e)
        {
            if (apptList.Items.Count == 0)
            {
                MessageBox.Show("Danh sách khám bệnh đang rỗng!");
                return;
            }
            if (currentAppt == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch khám!");
                return;
            }
            Block_or_Unlock_Textbox(true);
            Enable_Uneable_Function(false);

            //
            function_check = 2;
            Hide_Show_Buttons(true);
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (apptList.Items.Count == 0)
            {
                MessageBox.Show("Rỗng!");
                return;
            }
            if (currentAppt == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch khám!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa lịch khám này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                apptBus.DeleteAppointment(id_checked);
            }
            nameTxt.Text = "";
            dobTxt.Text = "";
            phoneTxt.Text = "";
            addrTxt.Text = "";
            emailTxt.Text = "";
            apptDate.Text = "";
            genderSl.SelectedIndex = -1;
            sltimecb.SelectedIndex = -1;

            LoadAll();
        }

        private void BtnSav_Click(object sender, EventArgs e)
        {
            String n = nameTxt.Text;
            int gend = genderSl.SelectedIndex;
            String bd = dobTxt.DateTime.ToString("yyyy-MM-dd");
            String addr = addrTxt.Text;
            String ph = phoneTxt.Text;
            String eml = emailTxt.Text;
            String sldate = apptDate.DateTime.ToString("yyyy-MM-dd");
            int sltime = sltimecb.SelectedIndex;
            int t = 0; //check our input

            if (
                string.IsNullOrWhiteSpace(nameTxt.Text)
                || string.IsNullOrWhiteSpace(addrTxt.Text)
                || string.IsNullOrWhiteSpace(phoneTxt.Text)
                || string.IsNullOrWhiteSpace(emailTxt.Text)
                || string.IsNullOrWhiteSpace(bd)
                || string.IsNullOrWhiteSpace(sldate)
                || gend == -1 || sltime == -1
                )
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            switch (function_check)
            {
                case 1:
                    currentAppt = apptBus.InsertAppointment(n, gend, bd, addr, ph, eml, sldate, sltime);
                    break;
                case 2:
                    apptBus.DeleteAppointment(id_checked);
                    apptBus.InsertAppointment(n, gend, bd, addr, ph, eml, sldate, sltime);
                    break;
            }

            currentAppt = null;
            nameTxt.Text = "";
            dobTxt.Text = "";
            phoneTxt.Text = "";
            addrTxt.Text = "";
            emailTxt.Text = "";
            apptDate.Text = "";
            genderSl.SelectedIndex = -1;
            sltimecb.SelectedIndex = -1;

            //
            Enable_Uneable_Function(true);
            Hide_Show_Buttons(false);
            Block_or_Unlock_Textbox(false);

            //
            LoadAll();
        }

        private void BtnCan_Click(object sender, EventArgs e)
        {
            Enable_Uneable_Function(true);
            Hide_Show_Buttons(false);
            nameTxt.Text = "";
            dobTxt.Text = "";
            phoneTxt.Text = "";
            addrTxt.Text = "";
            emailTxt.Text = "";
            apptDate.Text = "";
            genderSl.SelectedIndex = -1;
            sltimecb.SelectedIndex = -1;


            Block_or_Unlock_Textbox(false);
        }
    }
}