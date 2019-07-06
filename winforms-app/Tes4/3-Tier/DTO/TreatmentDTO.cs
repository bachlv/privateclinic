using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class TreatmentDTO
    {
        public int treat_id { get; private set; }

        public String name { get; private set; }

        public String symptom { get; private set; }

        public TreatmentDTO(int id, String na, String sym)
        {
            treat_id = id;
            name = na;
            symptom = sym;
        }
        public TreatmentDTO()
        { }

    }
}
