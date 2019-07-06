using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class query_medic_patientDTO
    {
        public int patient_id { get; private set; }
        public String patient_name { get; private set; }

        public String date { get; private set; }

        public String Disease { get; private set; }

        public String symptom { get; private set; }

        public String phone { get; private set; }

        public query_medic_patientDTO(int id,String pat_name, String dat, String dis, String sym ,String phon)
        {
            patient_id = id;
            patient_name = pat_name;
            date = dat;
            Disease = dis;
            symptom = sym;
            phone = phon;
        }
        public query_medic_patientDTO()
        {}
    }
}