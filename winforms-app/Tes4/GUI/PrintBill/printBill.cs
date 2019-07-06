using System;
using Tes4._3_Tier.DTO;
using System.Collections.Generic;

namespace Tes4.GUI.PrintBill
{
    public partial class printBill : DevExpress.XtraReports.UI.XtraReport
    {
        public printBill()
        {
           InitializeComponent();
        }

        public void InitData(int patient_id, int bill_id, string name_pa, string add, string dob, string sym, string treat, string gender, float Sum,List<bill_ItemDTO> data,float treat_cost)
        {
            bill_ID.Value = bill_id;
            patienntID.Value = patient_id;
            patientNName.Value = name_pa;
            patientAdd.Value = add;
            patientTreat.Value = treat;
            patienSym.Value = sym;
            patientDOB.Value = dob;
            Total.Value = Sum;
            patientGender.Value = gender;
            patientTreat.Value = treat_cost;
            objectDataSource1.DataSource = data;

        }

    }
}
