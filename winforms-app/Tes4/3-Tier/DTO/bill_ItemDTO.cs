using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes4._3_Tier.DTO
{
    public class bill_ItemDTO
    {
        public int item_id { get; private set; }

        public int bill_id { get; private set; }

        public String name { get; private set; }

        public String unit { get; private set; }

        public int quantity { get; private set; }
        
        public String usage { get; private set; }

        public float price { get; private set; }
        public bill_ItemDTO(String nameMedicine, String un, int quant,String use)
        {
            
            name = nameMedicine;
            unit = un;
            quantity = quant;
            usage = use;
        }
        public bill_ItemDTO()
        {
        }
    }
}
