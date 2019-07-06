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
namespace Tes4.GUI.PrintBill
{
    public partial class Reader : DevExpress.XtraEditors.XtraForm
    {
        public Reader()
        {
            InitializeComponent();
        }
        public void PrintInvoice (int patient_id, int bill_id, string name_pa, string add, string dob, string sym, string treat, string gender, float Sum, List<bill_ItemDTO> data,float treat_cost)
        {
            printBill print = new printBill();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in print.Parameters)
                p.Visible = false;
            print.InitData(patient_id, bill_id, name_pa, add, dob, sym, treat, gender, Sum, data,treat_cost);
            documentViewer1.DocumentSource = print;
            print.CreateDocument();

        }
    }
}