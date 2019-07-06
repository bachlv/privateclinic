using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Tes4.Patient;
using Tes4.GUI.Management;
using Tes4.GUI.Patient;
using Tes4.GUI.Report;

namespace Tes4
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonForm1()
        {
            InitializeComponent();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Diseases diseases = new Diseases();
            diseases.MdiParent = this;
            diseases.Show();
        }

        private void btnPatient_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatienInfo painfo = new PatienInfo();
            painfo.MdiParent = this;
            painfo.Show();
        }

        private void btnPaSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryPatient painQ = new QueryPatient();
            painQ.MdiParent = this;
            painQ.Show();

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatientRecord patientRecord = new PatientRecord();
            patientRecord.MdiParent = this;
            patientRecord.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            PillManagement pillM = new PillManagement();
            pillM.MdiParent = this;
            pillM.Show();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            MedicalRecord medRe = new MedicalRecord();
            medRe.MdiParent = this;
            medRe.Show();
        }

        private void btnInvoice_ItemClick(object sender, ItemClickEventArgs e)
        {
            Bill BIL = new Bill();
            BIL.MdiParent = this;
            BIL.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

            Medicine_Used BIL = new Medicine_Used();
            BIL.MdiParent = this;
            BIL.Show();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Income income = new Income();
            income.MdiParent = this;
            income.Show();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreatmentCost treatmentCost = new TreatmentCost();
            treatmentCost.ShowDialog();

        }

        private void btnDSKB_ItemClick(object sender, ItemClickEventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.MdiParent = this;
            appointment.Show();
        }

        private void BarButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppointNumber treatmentCost = new AppointNumber();
            treatmentCost.ShowDialog();
        }
    }
}