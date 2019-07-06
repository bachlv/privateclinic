using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class AppointmentDTO
    {
        public int appointment_id { get; private set; }

        public String name { get; private set; }

        public int gender { get; private set; }

        public String birthday { get; private set; }

        public String address { get; private set; }

        public String phone { get; private set; }

        public String email { get; private set; }

        public String slotdate { get; private set; }

        public int slottime { get; private set; }

        public AppointmentDTO()
        {

        }
        public AppointmentDTO(int aID, String n, int ge, String dob, String addr, String pnNum, String eml, String sldate, int sltime)
        {
            appointment_id = aID;
            name = n;
            birthday = dob;
            gender = ge;
            address = addr;
            phone = pnNum;
            email = eml;
            slotdate = sldate;
            slottime = sltime;
        }
    }
}
