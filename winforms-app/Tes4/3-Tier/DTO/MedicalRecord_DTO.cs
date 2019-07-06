using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class MedicalRecord_DTO
    {
        public int record_id { get; private set; }

        public int patient_id { get; private set; }

        public String symptom { get; private set; }

        public String disease { get; private set; }

         public String date { get; private set; }

        public MedicalRecord_DTO(int medre_id, int padId, String dis, String sym, String dat)
        {
            record_id = medre_id;
            patient_id = padId;
            symptom = sym;
            disease = dis;
            date = dat;
        }
        public MedicalRecord_DTO()
        {
        }
    }
}
