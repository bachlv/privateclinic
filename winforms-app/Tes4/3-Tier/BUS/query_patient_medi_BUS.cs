using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DAL;
using Tes4._3_Tier.DTO;

namespace Tes4._3_Tier.BUS
{
    class query_patient_medi_BUS
    {
        private query_medicine_patient_DAL query_pa_med;
        public query_patient_medi_BUS()
        {
            query_pa_med = new query_medicine_patient_DAL();
            query_pa_med.InitializeDB();
        }
        public List<query_medic_patientDTO> LoadAll()
        {
            return query_pa_med.LoadAll();
        }
        public List<query_medic_patientDTO> Query(string dieukien)
        {
            return query_pa_med.Query(dieukien);
        }
    }
}
