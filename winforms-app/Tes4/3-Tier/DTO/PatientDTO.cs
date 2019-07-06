using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class PatientDTO
    {
        public int patient_id { get; private set; }

        public String name { get; private set; }

        public String birthday { get; private set; }

        public String gender { get; private set; }

        public String addres { get; private set; }

        public String phone { get; private set; }
        public PatientDTO()
        {

        }
        public PatientDTO(int idP, String n, String dob, String ge, String addr, String pnNum)
        {
            patient_id = idP;
            name = n;
            birthday = dob;
            gender = ge;
            addres = addr;
            phone = pnNum;
        }
    }
}
