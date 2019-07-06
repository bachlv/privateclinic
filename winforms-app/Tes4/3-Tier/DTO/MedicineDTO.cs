using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    class MedicineDTO
    {
        public int medicine_id { get; private set; }

        public String name { get; private set; }

        public String quantity { get; private set; }

        public float price { get; private set; }


        public MedicineDTO(int idP, String n, String qu, float pr)
        {
            medicine_id = idP;
            name = n;
            quantity = qu;
            price = pr;
        }
        public MedicineDTO()
        {
        }
    }
}
