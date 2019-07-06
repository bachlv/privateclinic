using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
namespace Tes4._3_Tier.BUS
{
    class MedicalRecord_BUS
    {
        private MedicalRecord_DAL medReDAL;
        public MedicalRecord_BUS()
        {
            medReDAL = new MedicalRecord_DAL();
            medReDAL.InitializeDB();
        }
        public int Insert(int padId, String dis, String sym, String dat)
        {
            return medReDAL.Insert(padId, dis, sym, dat);
        }
        public void Delete(int med_ID)
        {
             medReDAL.Delete(med_ID);
        }
    }
}
