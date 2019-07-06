using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class billDTO
    {
        public int record_id { get; private set; }

        public int bill_id { get; private set; }

        public float treatment { get; private set; }

        public float total { get; private set; }

        public billDTO(int medre_id, int billid, float treat, float sum)
        {
            record_id = medre_id;
            bill_id = billid;
            treatment = treat;
            total = sum;
        }
        public billDTO()
        {
        }
    }
}
