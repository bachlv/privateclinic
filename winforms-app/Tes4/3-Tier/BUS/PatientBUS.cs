using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
using System.Collections.Generic;
using System;

namespace Tes4._3_Tier.BUS
{
    class PatientBUS
    {
        private PatientDAL patDAL;

        public PatientBUS()
        {
            patDAL = new PatientDAL();
            patDAL.InitializeDB();
        }
        
        public PatientDTO Insert(String n, String yob, String gender, String Addr, String pnNum)
        {
            return patDAL.Insert(n, yob, gender, Addr, pnNum);
        }
        public void Delete(int id)
        {
            patDAL.Delete(id);
        }
        public void Update(int id,String n, String yob, String gender, String Addr, String pnNum)
        {
            patDAL.Update(id,n, yob, gender, Addr, pnNum);
        }
        public List<PatientDTO> GetPatients()
        {
            return patDAL.GetPatients();
        }
        public List<PatientDTO> FastQuery(int searching)
        {
            return patDAL.FastQuery(searching);
        }
        public List<PatientDTO> Query(string dieukien)
        {
            return patDAL.Query(dieukien);
        }
        public List<string> GetPatients_Name()
        {
            return patDAL.GetPatients_Name();
        }
        public int GetPatient_ID(string name)
        {
            return patDAL.GetPatient_ID(name);
        }
    }
}
